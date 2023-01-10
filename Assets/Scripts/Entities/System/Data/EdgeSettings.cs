using System;
using Entities.Data;
using UnityEditor;
using UnityEngine;

namespace Entities.System.Data
{
    [Serializable]
    public struct EdgeSettings
    {
        public Edge Edge;
        public EdgeAction Action;
        [Tooltip("Used if Action is set to Kill")]
        public SceneAsset GoalScene;
    }
}