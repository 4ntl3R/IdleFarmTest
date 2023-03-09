using AKhvalov.IdleFarm.Runtime.Models;
using AKhvalov.IdleFarm.Runtime.Pool;

namespace AKhvalov.IdleFarm.Runtime.Controllers
{
    public class ResourcesController
    {
        private readonly ResourcesView _view;
        private readonly ResourcesModel _model;
        private readonly GameObjectPool _lootPool;
    
        public ResourcesController(ResourcesView view, ResourcesModel model, GameObjectPool lootPool)
        {
            _view = view;
            _model = model;
            _lootPool = lootPool;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _model.OnCoinsUpdated += _view.SetCoinValue;
            _model.OnLootUpdated += _view.SetLootValue;
            _lootPool.OnObjectDeactivated += _model.LootIncrease;
        }

        public void UnsubscribeEvents()
        {
            _model.OnCoinsUpdated -= _view.SetCoinValue;
            _model.OnLootUpdated -= _view.SetLootValue;
            _lootPool.OnObjectDeactivated -= _model.LootIncrease;
        }
    }
}
