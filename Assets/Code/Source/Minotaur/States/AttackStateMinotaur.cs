using Code.Source.StateMachine;
using UnityEngine;

namespace Code.Source.Minotaur.States
{
    public class AttackStateMinotaur : IState
    {
        public GameObject Victim;

        private readonly MinotaurMachine _minotaurMachine;

        public AttackStateMinotaur(MinotaurMachine minotaurMachine)
        {
            _minotaurMachine = minotaurMachine;
        }

        public float LastAttackTime { get; set; }

        /// <inheritdoc />
        public string GetPrint() => "Attack";

        /// <inheritdoc />
        public void Enter()
        {
        }

        /// <inheritdoc />
        public void Execute()
        {
            if (!AttackUtils.CanAttack(_minotaurMachine.transform, Victim,
                    GameVars.Instance.minotaurRadius) ||
                !_minotaurMachine.CanAttack)
            {
                _minotaurMachine.ChangeState(State.Idle);
            }

            if (!_minotaurMachine.CanAttack) return;
            _minotaurMachine.Attack();
            LastAttackTime = Time.time;
        }

        /// <inheritdoc />
        public void Exit()
        {
        }
    }
}