using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AKhvalov.IdleFarm.Runtime.Controllers
{
    public class InputJoystickController : InitiatableMonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<Vector2> OnJoystickDrag;
        
        [SerializeField]
        private RectTransform radialConstraint;
        
        [SerializeField] 
        private RectTransform innerJoystick;

        private RectTransform _outerJoystick;

        private Vector2 _dragPosition = Vector2.zero;
        private Vector2 _dragNormalized = Vector2.zero;
        
        private float _maxDragMagnitude = 1f;

        public void OnPointerDown(PointerEventData eventData)
        { 
            Initiate();
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (_outerJoystick, eventData.position, eventData.pressEventCamera, out _dragPosition);

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
        }

        protected override void Initiate()
        {
            base.Initiate();
            _outerJoystick = GetComponent<RectTransform>();
            _maxDragMagnitude = radialConstraint.localPosition.magnitude;
        }
    }
}
