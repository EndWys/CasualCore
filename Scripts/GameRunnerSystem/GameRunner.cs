using Assets.CasualCore.Scripts.GameRunnerSystem.GameStateMachine;

namespace Assets.CasualCore.Scripts.GameRunnerSystem
{
    public class GameRunner
    {
        private GameRunnerStateMachine _stateMachine;

        public void StartGame()
        {
            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            _stateMachine = new GameRunnerStateMachine();
            //_stateMachine.SwitchState<>();
        }

        public void Run()
        {
            _stateMachine.Tick();
        }

        public void ExitGame()
        {

        }
    }
}