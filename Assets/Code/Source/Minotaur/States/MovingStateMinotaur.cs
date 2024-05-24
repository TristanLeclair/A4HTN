using Code.Source.StateMachine;
using UnityEngine;

namespace Code.Source.Minotaur.States
{
    public class MovingStateMinotaur : IState
    {
        private MinotaurMachine _minotaurMachine; 
        
        public MovingStateMinotaur(MinotaurMachine minotaurMachine)
        {
            _minotaurMachine = minotaurMachine;
        }
        
        /// <inheritdoc />
        public void Enter()
        {
        }

        /// <inheritdoc />
        public void Execute()
        {
        }

        /// <inheritdoc />
        public void Exit()
        {
        }

        /// <inheritdoc />
        public string GetPrint() => "Moving";
    }
}