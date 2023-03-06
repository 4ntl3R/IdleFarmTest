using AKhvalov.IdleFarm.Runtime.Controllers;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime
{
    public class DependencyManager : MonoBehaviour
    {
        [SerializeField] 
        private PlayerMovementController playerMovementController;

        [SerializeField] 
        private InputJoystickController inputJoystickController;

        [SerializeField] 
        private float playerSpeedMultiplier = 10f;
        
        
        private void Start()
        {
            ManageInjections();
            AddSubscriptions();
        }

        private void OnDestroy()
        {
            DeleteSubscriptions();
        }

        private void ManageInjections()
        {
            playerMovementController.Inject(playerSpeedMultiplier);
        }

        private void AddSubscriptions()
        {
            inputJoystickController.OnJoystickDrag += playerMovementController.ChangeVelocity;
        }

        private void DeleteSubscriptions()
        {
            inputJoystickController.OnJoystickDrag -= playerMovementController.ChangeVelocity;
        }
    }
}
