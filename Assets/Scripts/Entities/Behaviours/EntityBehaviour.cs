using System;
using UnityEngine;

namespace Entities.Behaviours
{
    public abstract class EntityBehaviour : MonoBehaviour
    {
        public event Action OnDie;

        protected void SignalDying() => OnDie?.Invoke();
    }
}