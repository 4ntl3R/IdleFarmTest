using AKhvalov.IdleFarm.Runtime.Models;

namespace AKhvalov.IdleFarm.Runtime.Controllers
{
    public class ResourcesController
    {
        private readonly ResourcesView _view;
        private readonly ResourcesModel _model;
    
        public ResourcesController(ResourcesView view, ResourcesModel model)
        {
            _view = view;
            _model = model;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _model.OnCoinsUpdated += _view.SetCoinValue;
            _model.OnLootUpdated += _view.SetLootValue;
        }

        public void UnsubscribeEvents()
        {
            _model.OnCoinsUpdated -= _view.SetCoinValue;
            _model.OnLootUpdated -= _view.SetLootValue;
        }
    }
}
