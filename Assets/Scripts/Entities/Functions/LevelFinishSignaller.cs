using Entities.System;
using UnityEngine;
using Zenject;

namespace Entities.Functions
{
    [RequireComponent(typeof(OffCameraDetector))]
    public class LevelFinishSignaller : MonoBehaviour
    {
        private LevelSwitcher _levelFinisher;
        private OffCameraDetector _detector;
        
        [Inject]
        public void Construct(LevelSwitcher levelFinisher) => _levelFinisher = levelFinisher;

        public void Awake()
        {
            _detector = GetComponent<OffCameraDetector>();
            _detector.OnEdgeLeft += _levelFinisher.FinishLevel;
        }

        public void OnDestroy() => _detector.OnEdgeLeft += _levelFinisher.FinishLevel;
    }
}