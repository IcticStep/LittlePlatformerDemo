using System;
using UnityEngine;

namespace Collectables
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class CollectableItem : MonoBehaviour
    {
        public bool Collected { get; set; }
        public event Action OnCollected;

        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            Init();
        }

        public void Collect()
        {
            if(Collected)
                return;

            Collected = true;
            _collider.enabled = false;
            OnCollected?.Invoke();
        }

        protected virtual void Init()
        {
            
        }
    }
}