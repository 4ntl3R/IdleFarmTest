using System;
using AKhvalov.IdleFarm.Runtime.Data;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

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
            if (isToggleable)
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
            OnInteractionEnd?.Invoke(actorView, this);
        }
    }
}
