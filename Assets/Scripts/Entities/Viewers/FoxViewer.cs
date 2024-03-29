﻿using UnityEngine;
using Entities.Functions;
using Entities.Functions.Movers;
using VFX;

namespace Entities.Viewers
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(DeathMaker))]
    public class FoxViewer : Viewer
    {
        [SerializeField] private SimpleVFX _jumpDust;
        [SerializeField] private GameObject _dustSpawn;

        private Mover _mover;
        private DeathMaker _deathMaker;
        
        private static class AnimatorHashes
        {
            public static readonly int SpeedX = Animator.StringToHash("AbsSpeedX");
            public static readonly int SpeedY = Animator.StringToHash("SpeedY");
            public static readonly int TakingDamage = Animator.StringToHash("TakingDamage");
        }
        
        private void OnEnable()
        {
            _mover.OnMoveY += ThrowDust;
            _deathMaker.OnDie += ShowHurt;
        }

        private void OnDisable()
        {
            _mover.OnMoveY -= ThrowDust;
            _deathMaker.OnDie -= ShowHurt;
        }

        private void FixedUpdate()
        {
            var speed = _mover.GetSpeed();
            SetAnimatorSpeeds(speed);
        }
        
        public void StopShowingHurt() => Animator.SetBool(AnimatorHashes.TakingDamage, false);

        protected override void DoAdditionalInitialization()
        {
            _mover = GetComponent<Mover>();
            _deathMaker = GetComponent<DeathMaker>();
        }

        protected override void ShowHurt() => Animator.SetBool(AnimatorHashes.TakingDamage, true);

        private void ThrowDust() => Instantiate(_jumpDust, _dustSpawn.transform.position, Quaternion.identity);

        private void SetAnimatorSpeeds(Vector2 speed)
        {
            Animator.SetFloat(AnimatorHashes.SpeedX, Mathf.Abs(speed.x));
            Animator.SetFloat(AnimatorHashes.SpeedY, speed.y);
        }
    }
}