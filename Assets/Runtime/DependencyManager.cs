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
        private List<InteractionReactorView> _gatherables;

        [SerializeField] 
        private AnimationData _animationData;

        private GameObjectPool _lootPool;

        private GatherableController _gatherableController;
        
        
        private void Start()
        {
            CreateClasses();
            ManageInjections();
            AddSubscriptions();
        }

        private void OnDestroy()
        {
            DeleteSubscriptions();
        }
        
        private void CreateClasses()
        {
            _lootPool = new GameObjectPool(lootPrefab);
            _gatherableController = new GatherableController(_lootPool, _gatherables, 1, _animationData.GrowParameters);
        }

        private void ManageInjections()
        {
            playerMovementView.Inject(playerSpeedMultiplier);
        }

        private void AddSubscriptions()
        {
            inputJoystickController.OnJoystickDrag += playerMovementView.ChangeVelocity;
        }

        private void DeleteSubscriptions()
        {
            inputJoystickController.OnJoystickDrag -= playerMovementView.ChangeVelocity;
            _lootPool.UnsubscribeEvents();
            _gatherableController.UnsubscribeEvents();
        }
    }
}
