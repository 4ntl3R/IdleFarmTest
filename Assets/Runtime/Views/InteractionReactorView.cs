using System;
using AKhvalov.IdleFarm.Runtime.Data;
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
        private InteractableType interactableType;

        [SerializeField] 
        private Renderer _visualisation;

        private Collider _collider;

        public InteractableType InteractableType => interactableType;
        
        public Renderer Visualisation => _visualisation;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            
            #if UNITY_EDITOR
            
                Assert.IsTrue(_collider.isTrigger, string.Format(ColliderAssertionString, gameObject.name));
            
            #endif
        }

        public void Interact(InteractionActorView actorView)
        {
            _collider.enabled = false;
            OnInteraction?.Invoke(actorView, this);
        }

        public void Activate()
        {
            _collider.enabled = true;
        }

        public void EndInteraction(InteractionActorView actorView)
        {
            OnInteractionEnd?.Invoke(actorView, this);
        }
    }
}
