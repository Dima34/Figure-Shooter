using Infrastructure.Factories;
using Zenject;

namespace Infrastructure.StateMachines.GameStateMachine.States
{
    public class GameLoopState : IGameState
    {
        public GameLoopState(IGameFactory gameFactory, IGameStateMachine gameStateMachine)
        {
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
        {
        }
    }
}