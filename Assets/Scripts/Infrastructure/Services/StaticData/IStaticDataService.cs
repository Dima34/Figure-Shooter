using System.Collections.Generic;
using StaticData;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        LevelStaticData GetCurrentLevelData();
        PlayerStaticData GetPlayerData();
        List<Color> GetBulletColors();
        List<string> GetBuildBlockVariantPaths();
        List<Color> GetBlockColors();
        List<string> GetFigureVatiantPaths();
    }
}