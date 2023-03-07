using System;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Views
{
    [RequireComponent(typeof(Collider))]
    public class InteractionActorView : MonoBehaviour
    {
        public event Action<InteractionActorView, InteractionReactorView> OnInteraction;

        [SerializeField] 
        private GameObject lootPickTarget;
        
        private InteractionReactorView _reactorView;

        public GameObject LootPickTarget => lootPickTarget;

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent(typeof(InteractionReactorView), out var reactor))
            {
                return;
            }
            
            _reactorView = (InteractionReactorView) reactor;
            OnInteraction?.Invoke(this, _reactorView);
            _reactorView.Interact(this);
        }
    }
}
