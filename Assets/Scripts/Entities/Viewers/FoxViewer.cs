using UnityEngine;
using System;
using Entities.Functions.Movers;
using VFX;

namespace Entities.Viewers
{
    [RequireComponent(typeof(Mover))]
    public class FoxViewer : Viewer
    {
        [SerializeField] private SimpleVFX _jumpDust;
        [SerializeField] private GameObject _dustSpawn;
        [SerializeField] private float _minSpeedToFlip = 0.1f;
        
        private Mover _mover;
        
        private static class AnimatorHashes
        {
            public static readonly int SpeedX = Animator.StringToHash("AbsSpeedX");
            public static readonly int SpeedY = Animator.StringToHash("SpeedY");
        }
        
        private void OnEnable() => _mover.OnMoveY += ThrowDust;
        private void OnDisable() => _mover.OnMoveY -= ThrowDust;
        private void FixedUpdate() => UpdateView();

        protected override void DoAdditionalInitialization() => _mover = GetComponent<Mover>();
        
        protected override void ShowHurt()
        {
            throw new NotImplementedException();
        }

        private void SetMirrored(bool mirrored) => SpriteRenderer.flipX = mirrored;
        private void ThrowDust() => Instantiate(_jumpDust, _dustSpawn.transform.position, Quaternion.identity);

        private void UpdateView()
        {
            var speed = _mover.GetSpeed();
            
            SetAnimatorSpeeds(speed);
            FlipSprite(speed);
        }

        private void FlipSprite(Vector2 speed)
        {
            if (speed.x < -_minSpeedToFlip)
                SetMirrored(true);
            else if (speed.x > _minSpeedToFlip)
                SetMirrored(false);
        }

        private void SetAnimatorSpeeds(Vector2 speed)
        {
            Animator.SetFloat(AnimatorHashes.SpeedX, Mathf.Abs(speed.x));
            Animator.SetFloat(AnimatorHashes.SpeedY, speed.y);
        }
    }
}