using Collectables;
using DG.Tweening;
using Entities.System;
using UnityEngine;
using Zenject;

namespace Entities.Viewers
{
    [RequireComponent(typeof(CollectableItem))]
    public class CoinViewer : Viewer
    {
        [Header("On collected")]
        [SerializeField] private float _flyingUpDistance = 2;
        [SerializeField] private float _flyingUpTime = 0.4f;

        private CollectableItem _collectableItem;
        
        private void OnEnable() => _collectableItem.OnCollected += FlyToFade;
        private void OnDisable() => _collectableItem.OnCollected -= FlyToFade;

        protected override void DoAdditionalInitialization() => _collectableItem = GetComponent<CollectableItem>();

        private void FlyToFade() =>
            DOTween.Sequence()
                .Append(transform.DOMoveY(transform.position.y + _flyingUpDistance, _flyingUpTime)
                    .SetEase(Ease.InCubic))
                .Insert(0, SpriteRenderer.DOFade(0, _flyingUpTime));
    }
}