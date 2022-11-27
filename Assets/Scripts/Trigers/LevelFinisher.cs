using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class LevelFinisher : MonoBehaviour
{
    [SerializeField] private Collider2D _interactorForFinish;
    [SerializeField] private SceneAsset _nextScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetInstanceID() == _interactorForFinish.GetInstanceID())
            SceneManager.LoadScene(_nextScene.name);
    }
}
