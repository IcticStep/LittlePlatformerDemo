using Entities.Behaviours;
using UnityEngine;

namespace Entities.Viewers
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(Animator))]
    public class EagleViewer : MonoBehaviour
    {
        private static readonly int _fallingAnimatorVar = Animator.StringToHash("Hurting");
        
        private Animator _animator;
        private Enemy _enemy;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _enemy = GetComponent<Enemy>();
        }

        private void OnEnable() => _enemy.OnDie += ShowHurt;
        private void OnDisable() => _enemy.OnDie -= ShowHurt;
        private void ShowHurt() => _animator.SetBool(_fallingAnimatorVar, true);
    }
}