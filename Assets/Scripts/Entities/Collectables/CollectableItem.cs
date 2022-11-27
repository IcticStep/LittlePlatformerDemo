using System;
using UnityEngine;

namespace Entities.Collectables
{
    public abstract class CollectableItem : MonoBehaviour
    {
        public abstract void Collect();
    }
}
