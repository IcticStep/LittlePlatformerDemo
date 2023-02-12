using System;
using System.Linq;
using UnityEngine;

namespace Collectables
{
    public class CollectablesContainer : MonoBehaviour
    {
        public event Action OnAnyCollected;
        private CollectableItem[] _items;

        private void Awake()
        {
            _items = GetComponentsInChildren<CollectableItem>();

            foreach (var item in _items)
                item.OnCollected += SignalCollected;
        }

        private void OnDestroy()
        {
            foreach (var item in _items)
                item.OnCollected -= SignalCollected;
        }
        
        public bool[] GetItemStates() => _items.Select(item => item.Collected).ToArray();

        public void SetItemStates(bool[] states)
        {
            for (var i = 0; i < _items.Length; i++)
            {
                var current = _items[i];
                current.Collected = states[i];
                current.gameObject.SetActive(!current.Collected);
            }
        }

        private void SignalCollected() => OnAnyCollected?.Invoke();
    }
}