using System;

namespace Entities.System
{
    public class LevelIndexer
    {
        private const string LevelNamePrefix = "Level";
        private const string MainMenuName = "MainMenu";
        public string GetLevelNameByIndex(int index) 
            => LevelNamePrefix + Convert.ToString(index);

        public int GetLevelIndexByName(string shortName)
        {
            if (shortName == MainMenuName) 
                return 0;
            
            var stringedIndex = shortName.Replace(LevelNamePrefix, "");
            return int.Parse(stringedIndex);
        }
    }
}