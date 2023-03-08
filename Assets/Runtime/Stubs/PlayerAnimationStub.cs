using AKhvalov.IdleFarm.Runtime.Extensions;
using AKhvalov.IdleFarm.Runtime.Views;
using DG.Tweening;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Stubs
{
    public class PlayerAnimationStub : MonoBehaviour
    {
        [SerializeField] 
        private InteractionActorView actor;

        private void Awake()
        {
            actor = GetComponent<InteractionActorView>();
            actor.OnInteraction += DelayedInteractionEnd;
        }

        private void OnDestroy()
        {
            actor.OnInteraction -= DelayedInteractionEnd;
        }

        private void DelayedInteractionEnd(InteractionActorView actor, InteractionReactorView reactor)
        {
            DOTweenExtension.SequenceDelay(0.25f, ()=>reactor.EndInteraction(actor));
        }

        public void Test()
        {
            Debug.Log(DOTween.TotalActiveSequences());
        }
    }
}
