namespace Assets.CasualCore.Scripts.StateMachineSystem
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Tick();
    }
}