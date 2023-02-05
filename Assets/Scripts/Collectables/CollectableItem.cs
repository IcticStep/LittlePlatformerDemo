using System;
using UnityEngine;

namespace Collectables
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class CollectableItem : MonoBehaviour
    {
        public bool Collected { get; private set; }
        public event Action OnCollected;

        private Collider2D _collider;

        private void Awake() => _collider = GetComponent<Collider2D>();

        public void Collect()
        {
            if(Collected)
                return;

            Collected = true;
            _collider.enabled = false;
            OnCollected?.Invoke();
        }
    }
}
