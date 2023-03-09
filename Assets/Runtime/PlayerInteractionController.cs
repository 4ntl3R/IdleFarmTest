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
        private readonly InteractionActorView _actor;
        private readonly ResourcesModel _resourcesModel;

        public PlayerInteractionController(InteractionActorView actor, ResourcesModel resourcesModel)
        {
            _actor = actor;
            _resourcesModel = resourcesModel;
            _actor.OnInteraction += InteractionResolver;
        }

        public void UnsubscribeEvents()
        {
            _actor.OnInteraction -= InteractionResolver;
        }

        private void InteractionResolver(InteractionActorView actor, InteractionReactorView reactor)
        {
            switch (reactor.InteractableType)
            {
                case InteractableType.Gather:
                    DOTweenExtension.SequenceDelay(0.25f, ()=>reactor.EndInteraction(actor));
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
    }
}
