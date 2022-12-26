using Entities.Functions;
using UnityEngine;

namespace Entities.Viewers
{
    [RequireComponent(typeof(DeathMaker))]
    public class EagleViewer : Viewer
    {
        private static readonly int _fallingAnimatorVar = Animator.StringToHash("Hurting");

        private DeathMaker _deathMaker;
        
        private void OnEnable() => _deathMaker.OnDie += ShowHurt;
        private void OnDisable() => _deathMaker.OnDie -= ShowHurt;

        protected override void DoAdditionalInitialization() => _deathMaker = GetComponent<DeathMaker>();
        protected override void ShowHurt() => Animator.SetBool(_fallingAnimatorVar, true);
    }
}