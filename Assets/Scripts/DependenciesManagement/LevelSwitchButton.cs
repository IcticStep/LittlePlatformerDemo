using System;
using System.Linq;
using Entities.System;
using Entities.System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace DependenciesManagement
{
    [RequireComponent(typeof(Button))]
    public class LevelSwitchButton : MonoBehaviour
    {
        [Inject] private LevelSwitcher _levelSwitcher;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ChangeScene);
        }

        private void ChangeScene()
        {
            // _levelSwitcher.SwitchLevel(default);
            SceneManager.LoadScene(1);
        }
    }
}
