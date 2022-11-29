using System;
using UnityEngine;

namespace Entities.Behaviours
{
    public abstract class Enemy : MonoBehaviour
    {
        public event Action OnDie;

        protected void SignalDying() => OnDie?.Invoke();
    }
}