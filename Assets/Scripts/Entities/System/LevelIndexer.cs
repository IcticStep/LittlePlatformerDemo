using System;

namespace Entities.System
{
    public class LevelIndexer
    {
        private const string LevelNamePrefix = "Level";
        public string GetLevelNameByIndex(int index) => LevelNamePrefix + Convert.ToString(index);

        public int GetLevelIndexByName(string shortName)
        {
            var stringedIndex = shortName.Replace(LevelNamePrefix, "");
            return int.Parse(stringedIndex);
        }
    }
}