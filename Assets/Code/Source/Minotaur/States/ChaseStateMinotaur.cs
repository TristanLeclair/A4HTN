using Code.Source.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Source.Minotaur.States
{
    public class ChaseStateMinotaur : IState
    {
        public GameObject Target { get; set; }
        public Vector3 LastKnownPosition { get; set; }

        private readonly MinotaurMachine _minotaurMachine;

        public ChaseStateMinotaur(MinotaurMachine minotaurMachine)
        {
            _minotaurMachine = minotaurMachine;
        }

        /// <inheritdoc />
        public string GetPrint() => "Chasing";

        /// <inheritdoc />
        public void Enter()
        {
            _minotaurMachine.SetDestination(LastKnownPosition);
        }

        /// <inheritdoc />
        public void Execute()
        {
            if (AttackUtils.CanAttack(_minotaurMachine.MinotaurTransform,
                    Target, GameVars.Instance.minotaurRadius * 2))
            {
                Debug.Log("Reached");
                _minotaurMachine.ChangeState(State.Attacking);
            }

            var currentPos = _minotaurMachine.MinotaurPosition;
            if (Vector3.Distance(currentPos,
                    LastKnownPosition) > GameVars.Instance.minotaurRadius * 2)
            {
                return;
            }

            _minotaurMachine.ChangeState(State.Idle);
        }

        /// <inheritdoc />
        public void Exit()
        {
        }
    }
}