using UnityEngine;
using DG.Tweening;

namespace Entities.Collectables
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Coin : CollectableItem
    {
        [Header("On collected")]
        [SerializeField] private float _flyingUpDistance = 1;
        [SerializeField] private float _flyingUpTime = 1;
        
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override void Collect() => FlyToDestroy();

        private void FlyToDestroy() =>
            DOTween.Sequence()
                .Append(transform.DOMoveY(transform.position.y + _flyingUpDistance, _flyingUpTime)
                    .SetEase(Ease.InCubic))
                .Insert(0, _spriteRenderer.DOFade(0, _flyingUpTime))
                .onKill += () => Destroy(gameObject);
    }
}