using System.Collections.Generic;
using AKhvalov.IdleFarm.Runtime.Controllers;
using AKhvalov.IdleFarm.Runtime.Data;
using AKhvalov.IdleFarm.Runtime.Models;
using AKhvalov.IdleFarm.Runtime.Pool;
using AKhvalov.IdleFarm.Runtime.Views;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime
{
    public class DependencyManager : MonoBehaviour
    {
        [SerializeField] 
        private AnimationData animationData;

        [SerializeField] 
        private LevelPreferencesData levelData;

        [SerializeField] 
        private LevelPrefabData prefabData;
        
        [SerializeField] 
        private PlayerMovementView playerMovementView;

        [SerializeField] 
        private InputJoystickController inputJoystickController;

        [SerializeField]
        private GameObject coinTarget;

        [SerializeField] 
        private GameObject deliveryTarget;

        [SerializeField] 
        private List<InteractionReactorView> gatherables;

        [SerializeField] 
        private ResourcesView resourcesView;

        [SerializeField]
        private InteractionActorView interactionActorView;

        [SerializeField] 
        private InteractionActorView gatherHitBox;

        [SerializeField] 
        private PlayerAnimationView playerAnimationView;

        private GameObjectPool _lootPool;
        private GameObjectPool _lootDeliveryPool;
        private GameObjectPool _coinPool;
        
        private GatherableController _gatherableController;
        private PlayerMovementController _playerMovementController;
        private ResourcesController _resourcesController;
        private PlayerInteractionController _playerInteractionController;
        
        private ResourcesModel _resourcesModel;

        
        private void Start()
        {
            CreateClasses();
            ManageInjections();
        }

        private void OnDestroy()
        {
            DeleteSubscriptions();
        }
        
        private void CreateClasses()
        {
            _lootPool = new GameObjectPool(prefabData.LootPrefab);
            _lootDeliveryPool = new GameObjectPool(prefabData.LootDeliveryPrefab);
            _coinPool = new GameObjectPool(prefabData.CoinPrefab);
            
            _resourcesModel = new ResourcesModel(levelData.LootCapacity, levelData.LootCost);
            
            _gatherableController = new GatherableController(_lootPool, gatherables, levelData.GatherableCapacity, 
                animationData);
            _playerMovementController = new PlayerMovementController(inputJoystickController, playerMovementView);
            _resourcesController = new ResourcesController(resourcesView, _resourcesModel, _lootDeliveryPool, 
                interactionActorView, deliveryTarget, animationData.LootDeliverParametersData, coinTarget, _coinPool);
            _playerInteractionController = new PlayerInteractionController(interactionActorView, gatherHitBox, 
                _resourcesModel, playerAnimationView, levelData.HitBoxDuration);
        }

        private void ManageInjections()
        {
            playerMovementView.Inject(levelData.PlayerSpeedMultiplier);
            resourcesView.Inject(levelData.LootCapacity, animationData.UIAnimationParametersData, 
                animationData.LootDeliverParametersData);
            playerAnimationView.Inject(inputJoystickController, animationData.PlayerAnimationParametersData);
        }

        private void DeleteSubscriptions()
        {
            _lootPool.UnsubscribeEvents();
            
            _gatherableController.UnsubscribeEvents();
            _playerMovementController.UnsubscribeEvents();
            _resourcesController.UnsubscribeEvents();
            _playerInteractionController.UnsubscribeEvents();
        }
    }
}
