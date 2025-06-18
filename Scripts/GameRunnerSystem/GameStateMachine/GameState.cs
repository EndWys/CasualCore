using Assets.CasualCore.Scripts.StateMachineSystem;
using System.Threading;

namespace Assets.CasualCore.Scripts.GameRunnerSystem.GameStateMachine
{
    public abstract class GameState : State<GameState>
    {
        protected CancellationToken _gameCancellationToken;

        public GameState(CancellationToken token)
        {
            _gameCancellationToken = token;
        }
    }
}