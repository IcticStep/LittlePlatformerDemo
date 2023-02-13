using System;
using System.IO;
using Entities.Controls;
using Entities.Functions;
using Entities.System.Savers.Data;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;
using Zenject;

namespace Entities.System.Savers
{
    public class ProjectSaver : ISaver
    {
        private const float SaveInterval = 0.5f;
        private ILevelSwitcher _levelSwitcher;
        private Player _player;
        private SpawnPlacer _playerPlacer;
        
        [Inject]
        private void Construct(ILevelSwitcher levelSwitcher, Player player)
        {
            (_levelSwitcher, _player) = (levelSwitcher, player);
            _playerPlacer = _player.GetComponent<SpawnPlacer>();
            
            Load();
            StartSavingTimer();
        }

        private void StartSavingTimer() =>
            Observable.Timer(TimeSpan.FromSeconds(SaveInterval))
                .Repeat()
                .Subscribe(_ => Save());

        public void Save()
        {
            var data = GetCurrentProjectData();
            WriteSave(data);
        }

        private void WriteSave(ProjectData data)
        {
            Directory.CreateDirectory(GetFolderPath());
            var streamWriter = new StreamWriter(GetSavePath());
            var saveJson = JsonConvert.SerializeObject(data);
            streamWriter.WriteLine(saveJson);
            streamWriter.Close();
        }

        private ProjectData GetCurrentProjectData() =>
            new()
            {
                LevelID = _levelSwitcher.GetCurrentLevel(),
                PlayerPosition = _player.transform.position
            };

        public void Load()
        {
            var file = new FileInfo(GetSavePath());
            if(!file.Exists)
                return;

            var data = ReadSaveData();
            RestoreProjectState(data);
        }
        
        private ProjectData ReadSaveData()
        {
            var streamReader = new StreamReader(GetSavePath());
            var saveJson = streamReader.ReadToEnd();
            streamReader.Close();
            return JsonConvert.DeserializeObject<ProjectData>(saveJson);
        }

        private void RestoreProjectState(ProjectData sceneData)
        {
            _player.transform.position = sceneData.PlayerPosition;

            var currentLevel = _levelSwitcher.GetCurrentLevel();
            if (currentLevel != sceneData.LevelID)
            {
                _playerPlacer.DisableForOneLevelSwitch();
                _levelSwitcher.SwitchLevel(sceneData.LevelID);
            }
        }

        private string GetFolderPath() => Application.persistentDataPath;
        
        private string GetSavePath()
            => GetFolderPath() + @"\" + SaveSettings.GlobalSaveName + SaveSettings.SaveFileExtension;
    }
}