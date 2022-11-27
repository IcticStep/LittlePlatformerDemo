using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Triggers
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelFinisher : MonoBehaviour
    {
        [SerializeField] private Collider2D _trigger;
        [SerializeField] private SceneAsset _nextScene;
        
        public static string PreviousSceneName { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetInstanceID() != _trigger.GetInstanceID())
                return;

            PreviousSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(_nextScene.name);
        }
    }
}