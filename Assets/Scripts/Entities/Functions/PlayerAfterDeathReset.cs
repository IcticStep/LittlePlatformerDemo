using System;
using System.Collections.Generic;
using System.Linq;
using Entities.System;
using Entities.Viewers;
using UnityEngine;
using Zenject;

namespace Entities.Functions
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(DeathMaker))]
    [RequireComponent(typeof(FoxViewer))]
    public class PlayerAfterDeathReset : MonoBehaviour
    {
        private LevelSwitcher _levelSwitcher;
        private Rigidbody2D _rigidbody;
        private DeathMaker _deathMaker;
        private FoxViewer _foxViewer;

        private RigidbodyConstraints2D _constraintsCached;
        private float _gravityScaleCached;
        private List<Collider2D> _attachedColliders;

        [Inject]
        public void Construct(LevelSwitcher levelSwitcher) => _levelSwitcher = levelSwitcher;

        private void Awake()
        {
            GetComponents();
            CacheStartValues();

            _levelSwitcher.OnLevelStart += DoReset;
        }

        private void GetComponents()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _deathMaker = GetComponent<DeathMaker>();
            _foxViewer = GetComponent<FoxViewer>();
            _attachedColliders = GetComponentsInChildren<Collider2D>().ToList();
        }

        private void CacheStartValues()
        {
            _constraintsCached = _rigidbody.constraints;
            _gravityScaleCached = _rigidbody.gravityScale;
        }

        private void DoReset()
        {
            DoRigidbodyReset();
            EnableCollisions();
            
            _foxViewer.StopShowingHurt();
        }

        private void DoRigidbodyReset()
        {
            _rigidbody.constraints = _constraintsCached;
            _rigidbody.gravityScale = _gravityScaleCached;
            _rigidbody.velocity = Vector2.zero;
        }

        private void EnableCollisions() => _attachedColliders.ForEach(collider => collider.enabled = true);
    }
}