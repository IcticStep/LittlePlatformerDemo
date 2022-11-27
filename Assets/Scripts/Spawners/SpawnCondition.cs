using System;
using UnityEditor;
using UnityEngine;

namespace Spawners
{
    [Serializable]
    public class SpawnCondition
    {
        public SceneAsset PreviousScene;
        public Transform Spawn;
        public bool Mirror;
    }
}