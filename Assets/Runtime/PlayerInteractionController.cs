using AKhvalov.IdleFarm.Runtime.Data;
using AKhvalov.IdleFarm.Runtime.Extensions;
using AKhvalov.IdleFarm.Runtime.Models;
using AKhvalov.IdleFarm.Runtime.Views;
using DG.Tweening;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Controllers
{
    public class PlayerInteractionController
    {
        private readonly float _hitBoxDuration;
        private readonly InteractionActorView _player;
        private readonly InteractionActorView _gatherHitBox;
        private readonly PlayerAnimationView _playerAnimationView;
        private readonly ResourcesModel _resourcesModel;

        public PlayerInteractionController(InteractionActorView player, InteractionActorView gatherHitBox,
            ResourcesModel resourcesModel, PlayerAnimationView animationView, float hitBoxDuration)
        {
            _player = player;
            _gatherHitBox = gatherHitBox;
            _hitBoxDuration = hitBoxDuration;
            _resourcesModel = resourcesModel;
            _playerAnimationView = animationView;
            SubscribeEvents();
        }
        

        public void UnsubscribeEvents()
        {
            _player.OnInteraction -= InteractionResolve;
            _playerAnimationView.OnGather -= GatherResolve;
            _gatherHitBox.OnInteraction -= HitBoxResolve;
        }

        private void SubscribeEvents()
        {
            _player.OnInteraction += InteractionResolve;
            _playerAnimationView.OnGather += GatherResolve;
            _gatherHitBox.OnInteraction += HitBoxResolve;
        }

        private void InteractionResolve(InteractionActorView actor, InteractionReactorView reactor)
        {
            switch (reactor.InteractableType)
            {
                case InteractableType.Gather:
                    _playerAnimationView.StartGatherAnimation();
                    return;
                case InteractableType.Loot:
                {
                    if (!_resourcesModel.IsLootCapacityFilled)
                    {
                        reactor.EndInteraction(actor);
                        _resourcesModel.LootIncrease();
                    }

                    return;
                }
                case InteractableType.Deliver:
                {
                    _resourcesModel.ConvertLootToCoins();
                    return;
                }
            }
        }

        private void ToggleHitBox(bool newState)
        {
            _gatherHitBox.ToggleInteractions(newState);
        }

        private void GatherResolve()
        {
            ToggleHitBox(true);
            DOTweenExtension.SequenceDelay(_hitBoxDuration, () => ToggleHitBox(false));
        }

        private void HitBoxResolve(InteractionActorView actor, InteractionReactorView reactor)
        {
            reactor.EndInteraction(actor);
        }
    }
}
