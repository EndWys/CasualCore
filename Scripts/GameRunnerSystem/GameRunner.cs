using Assets.CasualCore.Scripts.GameRunnerSystem.GameStateMachine;
using System.Threading;

namespace Assets.CasualCore.Scripts.GameRunnerSystem
{
    public class GameRunner
    {
        private GameRunnerStateMachine _stateMachine;

        private CancellationTokenSource _runnerCancellationTokenSource;

        public void StartGame()
        {
            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            _runnerCancellationTokenSource = new CancellationTokenSource();

            _stateMachine = new GameRunnerStateMachine();

            _stateMachine.Register(new GameStartState(_runnerCancellationTokenSource.Token));

            _stateMachine.SwitchState<GameStartState>();
        }

        public void Run()
        {
            _stateMachine.Tick();
        }

        public void ExitGame()
        {
            _runnerCancellationTokenSource?.Cancel();
            _runnerCancellationTokenSource?.Dispose();
        }
    }
}