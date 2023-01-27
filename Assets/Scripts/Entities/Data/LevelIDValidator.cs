using UnityEngine.SceneManagement;

namespace Entities.Data
{
    public class LevelIDValidator
    {
        private const string LevelPrefix = "Level";
        private const string LevelPath = "Assets/Scenes/Levels/";
        private const string LevelFileType = ".unity";

        public bool LevelExists(string shortName)
        {
            var fullPath = LevelPath + shortName + LevelFileType;
            var buildIndex = SceneUtility.GetBuildIndexByScenePath(fullPath);

            return buildIndex != -1;
        }

        public bool LevelExists(int levelID) => LevelExists(LevelPrefix + levelID);
    }
}