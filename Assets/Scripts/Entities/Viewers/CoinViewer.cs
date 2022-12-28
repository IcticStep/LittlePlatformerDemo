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

        private LevelSwitcher _levelSwitcher;
        private CollectableItem _collectableItem;
        private Sequence _flyingToDestroy;
        
        [Inject]
        private void Construct(LevelSwitcher levelSwitcher) => _levelSwitcher = levelSwitcher;
        
        private void OnEnable()
        {
            _collectableItem.OnCollected += FlyToDestroy;
            _levelSwitcher.OnLevelSwitch += StopFlyingToDestroy;
            _levelSwitcher.OnLevelRestart += StopFlyingToDestroy;
        }

        private void OnDisable()
        {
            _collectableItem.OnCollected -= FlyToDestroy;
            _levelSwitcher.OnLevelSwitch -= StopFlyingToDestroy;
            _levelSwitcher.OnLevelRestart -= StopFlyingToDestroy;
        }

        protected override void DoAdditionalInitialization() => _collectableItem = GetComponent<CollectableItem>();

        private void FlyToDestroy()
        {
            _flyingToDestroy = DOTween.Sequence()
                .Append(transform.DOMoveY(transform.position.y + _flyingUpDistance, _flyingUpTime)
                    .SetEase(Ease.InCubic))
                .Insert(0, SpriteRenderer.DOFade(0, _flyingUpTime));
        }

        private void StopFlyingToDestroy()
        {
            if(_flyingToDestroy is null || !_flyingToDestroy.active)
                return;
            
            _flyingToDestroy.Kill();
        }
    }
}