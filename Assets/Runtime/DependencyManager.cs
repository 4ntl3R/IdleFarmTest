using System.Collections.Generic;
using AKhvalov.IdleFarm.Runtime.Controllers;
using AKhvalov.IdleFarm.Runtime.Data;
using AKhvalov.IdleFarm.Runtime.Models;
using AKhvalov.IdleFarm.Runtime.Pool;
using AKhvalov.IdleFarm.Runtime.Views;
using DG.Tweening;
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
        
        
        private GameObjectPool _lootPool;
        private GatherableController _gatherableController;
        private PlayerMovementController _playerMovementController;
        private ResourcesModel _resourcesModel;
        private ResourcesController _resourcesController;
        
        
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
            _gatherableController = new GatherableController(_lootPool, gatherables, gatherableCapacity, animationData.GrowParameters);
            _playerMovementController = new PlayerMovementController(inputJoystickController, playerMovementView);
            _resourcesModel = new ResourcesModel(lootCapacity, lootCost);
            _resourcesController = new ResourcesController(resourcesView, _resourcesModel, _lootPool);
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
        }
    }
}
