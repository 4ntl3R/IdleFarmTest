using System;
using AKhvalov.IdleFarm.Runtime.Data;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Views
{
    [RequireComponent(typeof(Collider))]
    public class InteractionActorView : MonoBehaviour
    {
        public event Action<InteractionActorView, InteractionReactorView> OnInteraction;

        [SerializeField] 
        private GameObject lootPickTarget;

        [SerializeField] 
        private InteractableType interactableType;

        public Vector3 LootPickTargetScale => _lootPickTargetScale;

        private Vector3 _lootPickTargetScale;
        private Collider _collider;
        private InteractionReactorView _reactorView;

        public GameObject LootPickTarget => lootPickTarget;

        private void Awake()
        {
            _lootPickTargetScale = lootPickTarget.transform.localScale;
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent(typeof(InteractionReactorView), out var reactor))
            {
                return;
            }
            
            _reactorView = (InteractionReactorView) reactor;
            if (!interactableType.HasFlag(_reactorView.InteractableType))
            {
                return;
            }
            
            OnInteraction?.Invoke(this, _reactorView);
            _reactorView.Interact(this);
        }

        public void ToggleInteractions(bool state)
        {
            _collider.enabled = state;
        }

        public void EndInteraction(InteractionReactorView reactor)
        {
            reactor.EndInteraction(this);
        }
    }
}
