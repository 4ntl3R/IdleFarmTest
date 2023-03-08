using System.Collections.Generic;
using AKhvalov.IdleFarm.Runtime.Controllers;
using AKhvalov.IdleFarm.Runtime.Data;
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
        private List<InteractionReactorView> _gatherables;

        [SerializeField] 
        private AnimationData _animationData;

        private GameObjectPool _lootPool;

        private GatherableController _gatherableController;

        private PlayerMovementController _playerMovementController;
        
        
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
            _gatherableController = new GatherableController(_lootPool, _gatherables, gatherableCapacity, _animationData.GrowParameters);
            _playerMovementController = new PlayerMovementController(inputJoystickController, playerMovementView);
        }

        private void ManageInjections()
        {
            playerMovementView.Inject(playerSpeedMultiplier);
        }

        private void DeleteSubscriptions()
        {
            _lootPool.UnsubscribeEvents();
            _gatherableController.UnsubscribeEvents();
            _playerMovementController.UnsubscribeEvents();
        }
    }
}
