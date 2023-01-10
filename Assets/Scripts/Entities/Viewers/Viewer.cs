using UnityEngine;

namespace Entities.Viewers
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Viewer : MonoBehaviour
    {
        protected Animator Animator;
        protected SpriteRenderer SpriteRenderer;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            DoAdditionalInitialization();
        }

        /// <summary>
        /// Runs in Awake of Viewer.
        /// </summary>
        protected virtual void DoAdditionalInitialization() { }

        protected virtual void ShowHurt() { }
    }
}