namespace Assets._Project.Scripts.StateMachineSystem
{
    public interface IStateSwitcher<TState> where TState : IState
    {
        void SwitchState<T>() where T : TState;
    }
}