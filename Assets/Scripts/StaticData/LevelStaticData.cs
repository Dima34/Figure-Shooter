using System;
using Infrastructure;
using UnityEngine;

namespace StaticData
{
    [Serializable]  
    public class LevelStaticData
    {
        public string LevelName;
        public Vector3Data PlayerStartPosition;
        public Vector3Data WorldSpawnPosition;
    }
}