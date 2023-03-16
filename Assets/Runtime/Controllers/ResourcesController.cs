using AKhvalov.IdleFarm.Runtime.Data;
using AKhvalov.IdleFarm.Runtime.Extensions;
using AKhvalov.IdleFarm.Runtime.Models;
using AKhvalov.IdleFarm.Runtime.Pool;
using AKhvalov.IdleFarm.Runtime.Views;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Controllers
{
    public class ResourcesController
    {
        private readonly ResourcesView _view;
        private readonly ResourcesModel _model;
        private readonly GameObject _deliverySource;
        private readonly GameObject _deliveryTarget;
        private readonly GameObject _coinTarget;
        private readonly GameObjectPool _deliveryPool;
        private readonly GameObjectPool _coinPool;
        private readonly LootDeliverParametersData _data;
        private readonly Camera _camera;

        private int _deliveryCounter = 0;
        private bool _isDelivering = false;

        public ResourcesController(ResourcesView view, ResourcesModel model, GameObjectPool deliveryLootPool,
            InteractionActorView deliverySource, GameObject deliveryTarget,
            LootDeliverParametersData deliverParametersData,
            GameObject coinTarget, GameObjectPool coinPool)
        {
            _view = view;
            _model = model;
            _deliveryPool = deliveryLootPool;
            _deliverySource = deliverySource.LootPickTarget;
            _deliveryTarget = deliveryTarget;
            _data = deliverParametersData;
            _coinTarget = coinTarget;
            _coinPool = coinPool;
            _camera = Camera.main;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _model.OnCoinsUpdated += _view.SetCoinValue;
            _model.OnLootUpdated += _view.SetLootValue;
            _model.OnLootCleared += DeliveryHandle;
            _coinPool.OnObjectDeactivated += _view.AnimateCoinReceiver;
        }

        public void UnsubscribeEvents()
        {
            _model.OnCoinsUpdated -= _view.SetCoinValue;
            _model.OnLootUpdated -= _view.SetLootValue;
            _model.OnLootCleared -= DeliveryHandle;
            _coinPool.OnObjectDeactivated -= _view.AnimateCoinReceiver;
        }

        private void DeliveryHandle(int lootAmount)
        {
            _deliveryCounter += lootAmount;
            
            if (_isDelivering)
            {
                return;
            }

            _isDelivering = true;
            AnimateLoot();
        }

        private void AnimateLoot()
        {
            _deliveryCounter--;
            _deliveryPool.GenerateObject(CreateDeliveryData());
            _coinPool.GenerateObject(CreateCoinData());
            
            if (_deliveryCounter > 0)
            {
                DOTweenExtension.SequenceDelay(_data.TimeInterval, AnimateLoot);
                return;
            }

            _isDelivering = false;
        }

        private PoolableActivationData CreateDeliveryData()
        {
            return new PoolableActivationData(_deliverySource.transform.position, _deliveryTarget.transform);
        }

        private PoolableActivationData CreateCoinData()
        {
            var sourcePosition = _camera.WorldToScreenPoint(_deliveryTarget.transform.position);
            return new PoolableActivationData(sourcePosition, _coinTarget.transform);
        }
    }
}
