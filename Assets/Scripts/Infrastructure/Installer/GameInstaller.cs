using Infrastructure.Factories;
using Infrastructure.Factories.UI;
using Infrastructure.Services.CoroutineRunner;
using Infrastructure.Services.Input;
using Infrastructure.Services.LevelBuilder;
using Infrastructure.StateMachines.GameStateMachine;
using Infrastructure.StateMachines.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installer
{
    public class GameInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindSelfAsInitializable();
            BindCoroutineRunner();
            BindInputService();
            BindGameFactory();
            BindLevelBuilder();
            BindUIFactory();
            BindStateMachineStatesFactory();
            BindGameStateMachine();
        }

        private void BindCoroutineRunner() =>
            Container
                .BindInterfacesAndSelfTo<CoroutineRunnerService>()
                .FromNewComponentOn(gameObject)
                .AsSingle();

        private void BindSelfAsInitializable() =>
            Container
                .BindInterfacesAndSelfTo<GameInstaller>()
                .FromInstance(this)
                .AsSingle();

        private void BindInputService()
        {
            Container
                .Bind<IInputService>()
                .To<InputService>()
                .AsSingle();
        }

        private void BindUIFactory() =>
            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();

        private void BindGameFactory() =>
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();

        private void BindLevelBuilder() =>
            Container
                .Bind<ILevelBuilder>()
                .To<LevelBuilder>()
                .AsSingle();

        private void BindStateMachineStatesFactory()
        {
            BindInitializeStateFactory();
            BindGameLoopStateFactory();
        }

        private void BindInitializeStateFactory() =>
            Container
                .BindFactory<IGameStateMachine,InitializeState, InitializeState.Factory>();
        
        private void BindGameLoopStateFactory() =>
            Container
                .BindFactory<IGameStateMachine,GameLoopState, GameLoopState.Factory>();

        private void BindGameStateMachine() =>
            Container
                .BindInterfacesAndSelfTo<GameStateMachine>()
                .AsSingle();

        public void Initialize() =>
            GetStateMachineAndEnterInitialState();

        private void GetStateMachineAndEnterInitialState()
        {
            GameStateMachine gameStateMachine = Container.Resolve<GameStateMachine>();
            gameStateMachine.EnterState<InitializeState>();
        }
    }
}