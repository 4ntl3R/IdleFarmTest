using System.Collections.Generic;
using AKhvalov.IdleFarm.Runtime.Data;
using AKhvalov.IdleFarm.Runtime.Extensions;
using AKhvalov.IdleFarm.Runtime.Models;
using AKhvalov.IdleFarm.Runtime.Pool;
using AKhvalov.IdleFarm.Runtime.Views;
using DG.Tweening;

namespace AKhvalov.IdleFarm.Runtime.Controllers
{
    public class GatherableController
    {
        private GameObjectPool _lootPool;
        private readonly Dictionary<InteractionReactorView, GatherableModel> _viewModelDictionary;
        private readonly Dictionary<GatherableModel, InteractionReactorView> _modelViewDictionary;
        private GrowAnimationParametersData _animationParameters;
        private float _gatheringDuration;

        public GatherableController(GameObjectPool lootPool, List<InteractionReactorView> gatherables, 
            int gatherableCapacity, AnimationData animationData)
        {
            _lootPool = lootPool;
            _animationParameters = animationData.GrowParameters;
            _gatheringDuration = animationData.PlayerAnimationParametersData.GatheringDuration;
            
            _viewModelDictionary = new Dictionary<InteractionReactorView, GatherableModel>();
            _modelViewDictionary = new Dictionary<GatherableModel, InteractionReactorView>();
            
            foreach (var view in gatherables)
            {
                var model = new GatherableModel(gatherableCapacity);
                _modelViewDictionary.Add(model, view);
                _viewModelDictionary.Add(view, model);
                Subscribe(model, view);
            }
        }

        public void UnsubscribeEvents()
        {
            foreach (var viewModelPair in _viewModelDictionary)
            {
                viewModelPair.Key.OnInteractionEnd -= Gather;
                viewModelPair.Value.OnEmpty -= ResetGatherable;
                viewModelPair.Value.OnContaining -= ActivateGatherable;
                viewModelPair.Value.OnEmpty -= ResetVisualizations; 
                viewModelPair.Value.OnContaining -= SwitchVisualizations;
            }
        }

        private void Subscribe(GatherableModel model, InteractionReactorView view)
        {
            view.OnInteractionEnd += Gather;
            model.OnEmpty += ResetGatherable;
            model.OnEmpty += ResetVisualizations; 
            model.OnContaining += DelayedActivateGatherable;
            model.OnContaining += SwitchVisualizations;
        }

        private void Gather(InteractionActorView actor, InteractionReactorView reactorView)
        {
            var model = _viewModelDictionary[reactorView];
            model.Use();
            _lootPool.GenerateObject(new PoolableActivationData(reactorView.transform.position));
        }

        private void ResetGatherable(GatherableModel sender)
        {
            var view = _modelViewDictionary[sender];
            Sequence growSequence = view.gameObject.Grow(view.Visualisation.material, 
                () => ActivateGatherable(sender), 
                _animationParameters);

            growSequence.Play();
        }

        private void ActivateGatherable(GatherableModel sender)
        {
            _modelViewDictionary[sender].Activate();
        }

        private void DelayedActivateGatherable(GatherableModel sender)
        {
            DOTweenExtension.SequenceDelay(_gatheringDuration, (() => ActivateGatherable(sender)));
        }

        private void SwitchVisualizations(GatherableModel sender)
        {
            _modelViewDictionary[sender].GetComponent<VisualizationSwitchView>().SwitchVisualization();
        }
        
        private void ResetVisualizations(GatherableModel sender)
        {
            _modelViewDictionary[sender].GetComponent<VisualizationSwitchView>().ResetVisualization();
        }
    }
}
