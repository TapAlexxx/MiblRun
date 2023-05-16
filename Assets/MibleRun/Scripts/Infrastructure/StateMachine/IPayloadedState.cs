namespace Scripts.Infrastructure.StateMachine
{
    public interface IPayloadedState<TPayload> : IExitable
    {
        void Enter(TPayload payload);
    }
}