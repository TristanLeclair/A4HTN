using System;
using Code.Scripts.Managers;
using Code.Source.Minotaur.States;
using Code.Source.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Source.Minotaur
{
    [RequireComponent(typeof(Sight))]
    public class MinotaurMachine : MonoBehaviour
    {
        public GameObject currentActionText;

        public Animator animator;
        public GameObject minotaurAttackPrefab;
        private AttackStateMinotaur _attackStateMinotaur;
        private ChaseStateMinotaur _chasingStateMinotaur;

        private float _cooldown;
        private TextMesh _currentActionText;
        private IdleStateMinotaur _idleStateMinotaur;
        private MovingStateMinotaur _movingStateMinotaur;
        private NavMeshAgent _navMeshAgent;
        private Sight _sight;
        private StateMachine.StateMachine _stateMachine;

        public Vector3 MinotaurPosition => _navMeshAgent.transform.position;
        public Transform MinotaurTransform => _navMeshAgent.transform;

        public bool CanAttack =>
            Time.time > _attackStateMinotaur.LastAttackTime + _cooldown;

        private void Start()
        {
            _cooldown = GameVars.Instance.minotaurCooldown;
            _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            _currentActionText = currentActionText.GetComponent<TextMesh>();

            _idleStateMinotaur =
                new IdleStateMinotaur(this, animator, MinotaurPosition);
            _movingStateMinotaur = new MovingStateMinotaur(this);
            _attackStateMinotaur = new AttackStateMinotaur(this);
            _chasingStateMinotaur = new ChaseStateMinotaur(this);
            _sight = GetComponent<Sight>();


            _stateMachine = new StateMachine.StateMachine();
            ChangeState(_idleStateMinotaur);
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        public void SetDestination(Vector3 direction)
        {
            _navMeshAgent.SetDestination(direction);
        }

        public void ChangeState(IState newState)
        {
            _stateMachine.ChangeState(newState);
            _currentActionText.text = newState.GetPrint();
        }

        public void Hit(GameObject attacker)
        {
            _chasingStateMinotaur.LastKnownPosition =
                attacker.transform.position;
            _chasingStateMinotaur.Target = attacker;
            ChangeState(_chasingStateMinotaur);
        }

        public void ChangeState(State state)
        {
            switch (state)
            {
                case State.Idle:
                    ChangeState(_idleStateMinotaur);
                    break;
                case State.Moving:
                    ChangeState(_movingStateMinotaur);
                    break;
                case State.Chasing:
                    ChangeState(_chasingStateMinotaur);
                    break;
                case State.Attacking:
                    _attackStateMinotaur.Victim = _chasingStateMinotaur.Target;
                    ChangeState(_attackStateMinotaur);
                    break;
            }
        }

        public void Attack()
        {
            Instantiate(minotaurAttackPrefab, MinotaurPosition,
                Quaternion.identity);
        }
    }
}