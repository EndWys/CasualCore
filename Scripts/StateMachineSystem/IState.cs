namespace Assets._Project.Scripts.StateMachineSystem
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Tick();
    }
}