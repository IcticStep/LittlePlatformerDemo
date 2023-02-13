using Newtonsoft.Json;
using UnityEngine;

namespace Entities.System.Savers.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ProjectData
    {
        [JsonProperty] public int LevelID;
        [JsonProperty] private SerializableVector2 _playerPosition;
        
        public Vector2 PlayerPosition
        {
            get => new(_playerPosition.X, _playerPosition.Y);
            set => _playerPosition = new(value.x, value.y);
        }
    }
}