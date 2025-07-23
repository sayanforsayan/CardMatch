using System;
using System.Collections.Generic;

namespace Sayan.CardGame
{
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
