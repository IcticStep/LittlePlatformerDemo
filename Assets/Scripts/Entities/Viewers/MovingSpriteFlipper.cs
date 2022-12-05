using System;
using Entities.Functions.Movers;
using UnityEngine;

namespace Entities.Viewers
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class MovingSpriteFlipper : MonoBehaviour
    {
        [SerializeField] private float _minSpeedToFlip = 0.1f;
        [SerializeField] private bool _invertFlipping;

        private SpriteRenderer _spriteRenderer;
        private Mover _mover;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            var speed = _mover.GetSpeed();
            FlipSprite(speed);
        }

        private void FlipSprite(Vector2 speed)
        {
            var movingLeft = speed.x < -_minSpeedToFlip;
            var movingRight = speed.x > _minSpeedToFlip;
            
            if (movingLeft)
                SetMirrored(true);
            else if (movingRight)
                SetMirrored(false);
        }
        
        private void SetMirrored(bool mirrored) => _spriteRenderer.flipX = _invertFlipping ? !mirrored : mirrored;
    }
}