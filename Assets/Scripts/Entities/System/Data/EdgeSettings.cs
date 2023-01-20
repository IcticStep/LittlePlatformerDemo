using System;
using Entities.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entities.System.Data
{
    [Serializable]
    public struct EdgeSettings
    {
        [SerializeField] public Edge Edge;
        [SerializeField] public EdgeAction Action;
        [Tooltip("Used if Action is set to Kill")]
        [SerializeField] public int GoalSceneIndex;

        public override string ToString() 
            => $"On Edge {Edge} should {Action}. GoalSceneIndex: {GoalSceneIndex}.";
    }
}