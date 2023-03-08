
namespace AKhvalov.IdleFarm.Runtime.Controllers
{
    public class PlayerMovementController
    {
        private InputJoystickController _joystickController;
        private PlayerMovementView _playerMovementView;

        public PlayerMovementController(InputJoystickController joystick, PlayerMovementView player)
        {
            _joystickController = joystick;
            _playerMovementView = player;
            _joystickController.OnJoystickDrag += _playerMovementView.ChangeVelocity;
        }

        public void UnsubscribeEvents()
        {
            _joystickController.OnJoystickDrag -= _playerMovementView.ChangeVelocity;
        }
    }
}
