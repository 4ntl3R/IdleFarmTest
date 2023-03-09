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
        private PlayerMovementView playerMovementView;

        [SerializeField] 
        private InputJoystickController inputJoystickController;

        [SerializeField] 
        private GameObject lootPrefab;

        [SerializeField] 
        private float playerSpeedMultiplier = 10f;

        [SerializeField] 
        private int gatherableCapacity = 1;

        [SerializeField] 
        private int lootCapacity = 40;

        [SerializeField] 
        private int lootCost = 1;

        [SerializeField] 
        private List<InteractionReactorView> gatherables;

        [SerializeField] 
        private AnimationData animationData;

        [SerializeField] 
        private ResourcesView resourcesView;

        [SerializeField]
        private InteractionActorView interactionActorView;

        private GameObjectPool _lootPool;
        
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
            _lootPool = new GameObjectPool(lootPrefab);
            
            _resourcesModel = new ResourcesModel(lootCapacity, lootCost);
            
            _gatherableController = new GatherableController(_lootPool, gatherables, gatherableCapacity, animationData.GrowParameters);
            _playerMovementController = new PlayerMovementController(inputJoystickController, playerMovementView);
            _resourcesController = new ResourcesController(resourcesView, _resourcesModel, _lootPool);
            _playerInteractionController = new PlayerInteractionController(interactionActorView, _resourcesModel);
        }

        private void ManageInjections()
        {
            playerMovementView.Inject(playerSpeedMultiplier);
            resourcesView.Inject(lootCapacity);
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
