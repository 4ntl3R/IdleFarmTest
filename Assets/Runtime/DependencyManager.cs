using AKhvalov.IdleFarm.Runtime.Controllers;
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
            playerMovementView.Inject(playerSpeedMultiplier);
        }

        private void AddSubscriptions()
        {
            inputJoystickController.OnJoystickDrag += playerMovementView.ChangeVelocity;
        }

        private void DeleteSubscriptions()
        {
            inputJoystickController.OnJoystickDrag -= playerMovementView.ChangeVelocity;
        }
    }
}
