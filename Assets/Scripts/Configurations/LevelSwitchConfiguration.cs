using System.Collections.Generic;
using Entities.System.Data;
using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(fileName = "LevelSwitch", menuName = "ScriptableObjects/LevelSwitchConfiguration", order = 1)]
    public class LevelSwitchConfiguration : ScriptableObject
    {
        public List<EdgeSettings> EdgeSettings;
    }
}