using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.Random
{
    public interface IRandomService
    {
        bool Happend(int percentChance);
        int GetBetween(int from, int to);
        T GetRandomListElement<T>(List<T> list);
        Vector3 GetRandomVectorBetween(Vector3 from, Vector3 to);
    }
}