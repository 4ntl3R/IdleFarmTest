using AKhvalov.IdleFarm.Runtime.Controllers;
using AKhvalov.IdleFarm.Runtime.Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace AKhvalov.IdleFarm.Runtime
{
    public class DependencyManager : MonoBehaviour
    {
        [SerializeField] 
        private PlayerMovementView playerMovementView;

        [SerializeField] 
        private InputJoystickController inputJoystickController;
        
        private GameObjectPool lootPool;

        [SerializeField] 
        private GameObject lootPrefab;

        [SerializeField] 
        private float playerSpeedMultiplier = 10f;
        
        
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
            lootPool = new GameObjectPool(lootPrefab);
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
            lootPool.UnsubscribeEvents();
        }
    }
}
