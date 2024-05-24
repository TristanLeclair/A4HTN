using UnityEditor;

namespace Code.Source.StateMachine
{
    public interface IState : IPrintable
    {
        void Enter();
        void Execute();
        void Exit();
    }

    public interface IPrintable
    {
        string GetPrint();
    }
    
    public enum State
    {
        Idle,
        Moving,
        Chasing,
        Attacking
    }
}