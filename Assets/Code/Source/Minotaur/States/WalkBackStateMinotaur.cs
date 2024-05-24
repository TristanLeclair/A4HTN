using Code.Source.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Source.Minotaur.States
{
    public class WalkBackStateMinotaur : IState
    {
        private readonly MinotaurMachine _minotaurMachine;
        private readonly Vector3 _positionToWalkBackTo;

        public WalkBackStateMinotaur(MinotaurMachine minotaurMachine,
            Vector3 positionToWalkBackTo)
        {
            _minotaurMachine = minotaurMachine;
            _positionToWalkBackTo = positionToWalkBackTo;
        }

        /// <inheritdoc />
        public string GetPrint() => "Walking back";

        /// <inheritdoc />
        public void Enter()
        {
            _minotaurMachine.SetDestination(_positionToWalkBackTo);
        }

        /// <inheritdoc />
        public void Execute()
        {
            if (Vector3.Distance(_minotaurMachine.MinotaurPosition,
                    _positionToWalkBackTo) < 0.5f)
            {
                _minotaurMachine.ChangeState(State.Idle);
            }
        }

        /// <inheritdoc />
        public void Exit()
        {
        }
    }
}