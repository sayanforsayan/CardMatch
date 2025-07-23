using System;
using System.Collections.Generic;

namespace Sayan.CardGame
{
    /// <summary>
    /// Json Properties where level name and position store
    /// </summary>

    [Serializable]
    public class LevelData
    {
        public string levelName;
        public List<PositionData> positions;
    }

    [Serializable]
    public class PositionData
    {
        public float x;
        public float y;
    }

}
