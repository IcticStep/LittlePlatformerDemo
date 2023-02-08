using Zenject;
using System.IO;
using Collectables;
using Newtonsoft.Json;
using UnityEngine;

namespace Entities.System.Savers
{
    public class SceneSaver : ISaver
    {
        private ILevelSwitcher _levelSwitcher;
        private CollectablesContainer _collectablesContainer;

        [Inject]
        public void Construct(ILevelSwitcher levelSwitcher, CollectablesContainer collectablesContainer)
        {
            _levelSwitcher = levelSwitcher;
            _collectablesContainer = collectablesContainer;

            Init();
        }

        private void Init()
        {
            _levelSwitcher.OnLevelStart += Load;
            _levelSwitcher.OnLevelSwitch += Save;
        }

        ~SceneSaver()
        {
            _levelSwitcher.OnLevelStart -= Load;
            _levelSwitcher.OnLevelSwitch -= Save;
        }

        public void Save()
        {
            var sceneData = GetCurrentSceneData();
            WriteSceneDataSave(sceneData);
        }

        public void Load()
        {
            var file = new FileInfo(GetLevelSavePath());
            if(!file.Exists)
                return;

            var sceneData = ReadSceneData();
            RestoreSceneState(sceneData);
        }

        private SceneData GetCurrentSceneData() =>
            new()
            {
                Collectables = _collectablesContainer.GetItemStates()
            };

        private SceneData ReadSceneData()
        {
            var streamReader = new StreamReader(GetLevelSavePath());
            var saveJson = streamReader.ReadToEnd();
            streamReader.Close();
            return JsonConvert.DeserializeObject<SceneData>(saveJson);
        }

        private void WriteSceneDataSave(SceneData sceneData)
        {
            Directory.CreateDirectory(GetLevelFolderPath());
            var streamWriter = new StreamWriter(GetLevelSavePath());
            var saveJson = JsonConvert.SerializeObject(sceneData);
            streamWriter.WriteLine(saveJson);
            streamWriter.Close();
        }

        private void RestoreSceneState(SceneData sceneData) =>
            _collectablesContainer.SetItemStates(sceneData.Collectables);

        private string GetLevelFolderPath() =>
            Application.persistentDataPath + SaveSettings.SceneSaveRelativePath;

        private string GetLevelSavePath()
        {
            var currentLevel = _levelSwitcher.GetCurrentLevel();
            var folder = GetLevelFolderPath();
            return folder + SaveSettings.SceneSaveName + currentLevel + SaveSettings.SaveFileExtension;
        }
    }
}