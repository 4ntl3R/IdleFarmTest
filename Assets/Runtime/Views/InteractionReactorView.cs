using System;
using AKhvalov.IdleFarm.Runtime.Data;
using AKhvalov.IdleFarm.Runtime.Data.Enums;
using UnityEngine;
using UnityEngine.Assertions;

namespace AKhvalov.IdleFarm.Runtime.Views
{
    [RequireComponent(typeof(Collider))]
    public class InteractionReactorView : MonoBehaviour
    {
        private const string ColliderAssertionString = "Collider attached to reactor {0} should be trigger";
        
        public event Action<InteractionActorView, InteractionReactorView> OnInteraction;
        public event Action<InteractionActorView, InteractionReactorView> OnInteractionEnd;

        [SerializeField] 
        private bool isToggleable = true;

        [SerializeField] 
        private ToggleType toggleType = ToggleType.OnInteractionStart;

        [SerializeField] 
        private InteractableType interactableType;

        [SerializeField] 
        private Renderer visualisation;

        private Collider _collider;

        public InteractableType InteractableType => interactableType;
        
        public Renderer Visualisation => visualisation;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            
            #if UNITY_EDITOR
            
                Assert.IsTrue(_collider.isTrigger, string.Format(ColliderAssertionString, gameObject.name));
            
            #endif
        }

        public void Interact(InteractionActorView actorView)
        {
            if (isToggleable & toggleType == ToggleType.OnInteractionStart)
            {
                _collider.enabled = false;
            }
            OnInteraction?.Invoke(actorView, this);
        }

        public void Activate()
        {
            if (isToggleable)
            {
                _collider.enabled = true;
            }
        }

        public void EndInteraction(InteractionActorView actorView)
        {
            if (isToggleable & toggleType == ToggleType.OnInteractionEnd)
            {
                _collider.enabled = false;
            }
            OnInteractionEnd?.Invoke(actorView, this);
        }
    }
}
