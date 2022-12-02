using System;
using UnityEngine;

namespace Collectables
{
    public abstract class CollectableItem : MonoBehaviour
    {
        public bool Collected { get; private set; }
        public event Action OnCollected;

        public void Collect()
        {
            if(Collected)
                return;

            Collected = true;
            OnCollected?.Invoke();
        }
    }
}
