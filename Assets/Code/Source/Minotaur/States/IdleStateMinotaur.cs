using Code.Source.StateMachine;
using UnityEngine;

namespace Code.Source.Minotaur.States
{
    public class IdleStateMinotaur : IState
    {
        private readonly MinotaurMachine _minotaurMachine;
        private readonly Animator _animator;
        private readonly Vector3 _idlePosition;

        private static readonly int IsIdle = Animator.StringToHash("IsIdle");

        public IdleStateMinotaur(MinotaurMachine minotaurMachine, Animator animator, Vector3 idlePosition)
        {
            _minotaurMachine = minotaurMachine;
            _animator = animator;
            _idlePosition = idlePosition;
        }
        
        /// <inheritdoc />
        public void Enter()
        {
            _animator.SetBool(IsIdle, true);
        }


        /// <inheritdoc />
        public void Execute()
        {
            // If far away from idle position, walk back to it
            if (Vector3.Distance(_minotaurMachine.MinotaurPosition, _idlePosition) > 1f)
            {
                _minotaurMachine.ChangeState(new WalkBackStateMinotaur(_minotaurMachine, _idlePosition));
            }
        }

        /// <inheritdoc />
        public void Exit()
        {
            _animator.SetBool(IsIdle, false);
        }

        /// <inheritdoc />
        public string GetPrint() => "Idle";
    }
}