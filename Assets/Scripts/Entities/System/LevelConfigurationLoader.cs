using System.Collections.Generic;
using Entities.System.Data;
using UnityEngine;
using Zenject;

namespace Entities.System
{
    public class LevelConfigurationLoader : MonoBehaviour
    {
        public IReadOnlyList<EdgeSettings> LevelSettings { get; private set; }
        
        [Inject]
        public void Construct(List<EdgeSettings> levelSettings) => LevelSettings = levelSettings;
    }
}