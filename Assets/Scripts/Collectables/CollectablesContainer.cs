using System.Linq;
using UnityEngine;

namespace Collectables
{
    public class CollectablesContainer : MonoBehaviour
    {
        private CollectableItem[] _items;

        private void Awake()
        {
            _items = GetComponentsInChildren<CollectableItem>();
        }

        public bool[] GetItemStates()
        {
            return _items.Select(item => item.Collected).ToArray();
        }

        public void SetItemStates(bool[] states)
        {
            for (var i = 0; i < _items.Length; i++)
            {
                var current = _items[i];
                current.Collected = states[i];
                current.gameObject.SetActive(!current.Collected);
            }
        }
    }
}