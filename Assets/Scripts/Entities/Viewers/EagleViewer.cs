using UnityEngine;

namespace Entities.Viewers
{
    
    public class EagleViewer : Viewer
    {
        private static readonly int _fallingAnimatorVar = Animator.StringToHash("Hurting");
        
        private void OnEnable() => DeathMaker.OnDie += ShowHurt;
        private void OnDisable() => DeathMaker.OnDie -= ShowHurt;
        
        protected override void ShowHurt() => Animator.SetBool(_fallingAnimatorVar, true);
    }
}