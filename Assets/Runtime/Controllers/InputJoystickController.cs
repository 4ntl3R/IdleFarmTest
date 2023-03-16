using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AKhvalov.IdleFarm.Runtime.Controllers
{
    public class InputJoystickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<Vector2> OnJoystickDrag;
        
        [SerializeField]
        private RectTransform radialConstraint;
        
        [SerializeField] 
        private RectTransform innerJoystick;

        [SerializeField] 
        private RectTransform joystick;

        private RectTransform _touchZone;

        private Vector2 _touchPosition = Vector2.zero;
        private Vector2 _dragPosition = Vector2.zero;
        private Vector2 _dragNormalized = Vector2.zero;
        
        private float _maxDragMagnitude = 1f;
        
        void Awake()
        {
            _touchZone = GetComponent<RectTransform>();
            _maxDragMagnitude = radialConstraint.localPosition.magnitude;
            joystick.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (_touchZone, eventData.position, eventData.pressEventCamera, out _touchPosition);
            joystick.localPosition = _touchPosition;
            joystick.gameObject.SetActive(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (joystick, eventData.position, eventData.pressEventCamera, out _dragPosition);

            _dragNormalized = _dragPosition.normalized;
            
            _dragPosition = _dragPosition.magnitude > _maxDragMagnitude
                ? _dragNormalized * _maxDragMagnitude
                : _dragPosition;
            innerJoystick.localPosition = _dragPosition;
            
            OnJoystickDrag?.Invoke(_dragPosition/_maxDragMagnitude);
        }

        public void OnPointerUp(PointerEventData eventData) 
        { 
            OnJoystickDrag?.Invoke(Vector2.zero);
            innerJoystick.localPosition = Vector3.zero;
            joystick.gameObject.SetActive(false);
        }
    }
}
