using Unity.Mathematics;
using UnityEngine;

namespace Movers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Animal : MonoBehaviour, IMovable
    {
        [SerializeField] private float _speed = 5;
        
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            
        }

        public void MoveHorizontally(float ratio)
        {
            //_rigidbody2D.AddForce();
            throw new System.NotImplementedException();
        }

        public void MoveVertically(float ratio)
        {
            throw new System.NotImplementedException();
        }

        public float GetSpeed()
        {
            throw new System.NotImplementedException();
        }
    }
}