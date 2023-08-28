using Infrastructure.Factories;
using Infrastructure.Factories.UI;
using Infrastructure.Services.LevelBuilder;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.StateMachines.GameStateMachine.States
{
    public class InitializeState : IGameState
    {
        private IGameStateMachine _stateMachine;
        private LevelStaticData _levelData;
        private ILevelBuilder _levelBuilder;
        private IUIFactory _uiFactory;

        public InitializeState(IGameStateMachine stateMachine, ILevelBuilder levelBuilder, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _levelBuilder = levelBuilder;
            _uiFactory = uiFactory;
        }
        
        public void Enter()
        {
            BuildLevel();
            CreateHUD();
            GoToGameIdleState();
        }

        private void BuildLevel() =>
            _levelBuilder.BuildLevel();

        private void CreateHUD() =>
            _uiFactory.CreateHUD();

        private void GoToGameIdleState() =>
            _stateMachine.EnterState<GameLoopState>();

        public void Exit()
        {
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, InitializeState>
        {
        }
    }
}