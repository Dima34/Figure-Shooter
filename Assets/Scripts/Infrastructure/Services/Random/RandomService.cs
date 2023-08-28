using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.Random
{
    class RandomService : IRandomService
    {
        public bool Happend(int percentChance)
        {
            int rangeResult = UnityEngine.Random.Range(0, 100);
            return rangeResult < percentChance;
        }

        public int GetBetween(int from, int to) =>
            UnityEngine.Random.Range(from, to);
        
        public T GetRandomListElement<T>(List<T> list)
        {
            int randomIndex = GetBetween(0, list.Count);
            return list[randomIndex];
        }
        
        public Vector3 GetRandomVectorBetween(Vector3 from, Vector3 to) =>
            new(UnityEngine.Random.Range(from.x, to.x), UnityEngine.Random.Range(from.y, to.y), UnityEngine.Random.Range(from.z, to.z));
    }
}