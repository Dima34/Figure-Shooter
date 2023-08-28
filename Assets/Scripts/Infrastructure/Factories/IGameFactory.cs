using Infrastructure.Logic;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory
    {
        GameObject CreatePlayerAndAssingPlayerModules();
        GameObject CreateMap();
        Bullet CreateBullet();
        BuildBlock CreateRandBuildBlockWithRandColor();
        FigureBuilderBase CreateRandomFigure();
        GameObject Player { get; }
        PlayerViewMovement PlayerViewMovement { get; }
        SmoothlyLookAt PlayerSmoothlyLookAt { get; }
    }
}