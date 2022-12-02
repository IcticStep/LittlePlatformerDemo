using Entities.Behaviours;
using UnityEngine;

namespace Entities.Viewers
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Behaviour))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Viewer : MonoBehaviour
    {
        protected Animator Animator;
        protected EntityBehaviour Behaviour;
        protected SpriteRenderer SpriteRenderer;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Behaviour = GetComponent<EntityBehaviour>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            DoAdditionalInitialization();
        }

        /// <summary>
        /// Runs in Awake of Viewer.
        /// </summary>
        protected virtual void DoAdditionalInitialization() { }
        
        protected abstract void ShowHurt();
    }
}