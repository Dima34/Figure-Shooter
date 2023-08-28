using System;
using StaticData;
using UnityEngine;

namespace Infrastructure
{
    public static class JsonHelper
    {
        public static T[] ListFromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }

        public static T LoadFromJson<T>(string path)
        {
            string json = LoadJsonFromResouce(path);
            return JsonUtility.FromJson<T>(json);
        }
        
        private static string LoadJsonFromResouce(string jsonPath) =>
            Resources.Load<TextAsset>(jsonPath).text;

        public static T[] LoadListFromJson<T>(string jsonResourcePath)
        {
            string json = LoadJsonFromResouce(jsonResourcePath);
            return ListFromJson<T>(json);
        }
    }
}