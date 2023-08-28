using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Infrastructure.Constants;
using Infrastructure.Services.SceneLoad;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<string, LevelStaticData> _levelsData;
        private ISceneLoadService _sceneLoadService;
        private PlayerStaticData _playerData;
        private List<Color> _bulletColors;
        private List<string> _buildBlockVariantPaths;
        private List<Color> _buildBlockColors;
        private List<string> _figureVatiantPaths;

        [Inject]
        public void Construct(ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;

            LoadData();
        }

        private void LoadData()
        {
            LoadLevelData();
            LoadPlayerData();
            LoadBulletColors();
            LoadBuildBlockVariantPaths();
            LoadBuildBlockColors();
            LoadFigureVariants();
        }

        private void LoadLevelData()
        {
            LevelStaticData[] levelsDataList = JsonHelper.LoadListFromJson<LevelStaticData>(ResourcePaths.LEVELS_STATICDATA);
            _levelsData = levelsDataList.ToDictionary(x => x.LevelName, x => x);
        }

        private void LoadPlayerData() =>
            _playerData = JsonHelper.LoadFromJson<PlayerStaticData>(ResourcePaths.PLAYER_STATICDATA);

        private void LoadBulletColors()
        {
            string[] colorsList = JsonHelper.LoadListFromJson<string>(ResourcePaths.BULLET_COLORS);
            _bulletColors = TryParseColors(colorsList);
        }

        private void LoadBuildBlockColors()
        {
            string[] colorsList = JsonHelper.LoadListFromJson<string>(ResourcePaths.BUILD_BLOCK_COLORS);
            _buildBlockColors = TryParseColors(colorsList);
        }

        private List<Color> TryParseColors(string[] colorsList)
        {
            List<Color> colors = new List<Color>();

            foreach (string colorString in colorsList)
            {
                if (ColorUtility.TryParseHtmlString(colorString, out Color color))
                {
                    colors.Add(color);
                }
                else
                    throw new Exception($"Wrong color {colorString}");
            }

            return colors;
        }

        private void LoadBuildBlockVariantPaths() =>
            _buildBlockVariantPaths = JsonHelper.LoadListFromJson<string>(ResourcePaths.BUILD_BLOCK_VARIANTS).ToList();

        private void LoadFigureVariants() =>
            _figureVatiantPaths = JsonHelper.LoadListFromJson<string>(ResourcePaths.FIGURE_VARIANTS).ToList();

        public LevelStaticData GetCurrentLevelData()
        {
            string currentLevel = _sceneLoadService.GetCurrentLevel();
            return _levelsData[currentLevel];
        }

        public PlayerStaticData GetPlayerData() =>
            _playerData;

        public List<Color> GetBulletColors() =>
            _bulletColors;

        public List<string> GetBuildBlockVariantPaths() =>
            _buildBlockVariantPaths;

        public List<Color> GetBlockColors() =>
            _buildBlockColors;

        public List<string> GetFigureVatiantPaths() =>
            _figureVatiantPaths;
    }

    [System.Serializable]
    class LevelDataListWrapper
    {
        public List<LevelStaticData> levelDataList;

        public LevelDataListWrapper(List<LevelStaticData> list)
        {
            levelDataList = list;
        }
    }
}