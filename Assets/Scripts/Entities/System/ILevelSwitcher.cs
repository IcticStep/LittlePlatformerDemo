using System;

namespace Entities.System
{
    public interface ILevelSwitcher
    {
        public event Action OnLevelStart;
        public event Action OnLevelSwitch;
        public event Action OnLevelRestart;

        public int GetPreviousLevel();
        public int GetCurrentLevel();
        
        /// <summary>
        /// Loads new level in current stream.
        /// </summary>
        /// <param name="levelID">ID specified in level name(NOT buildIndex).</param>
        public void SwitchLevel(int levelID);
        /// <summary>
        /// Reload current active scene.
        /// </summary>
        public void RestartLevel();
    }
}