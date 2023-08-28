using UnityEngine;

namespace Infrastructure
{
    public static class DataExtensions
    {
        public static Vector3 AsUnityVector(this Vector3Data vectorData) => 
            new Vector3(vectorData.X, vectorData.Y, vectorData.Z);
        
    }
}