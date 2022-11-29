using Entities.Collectables;
using UnityEngine;

namespace EntitiesFunctions
{
    [RequireComponent(typeof(Collider2D))]
    public class Collector : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.gameObject.TryGetComponent<CollectableItem>(out var collectableItem))
                return;
            
            collectableItem.Collect();
        }
    }
}