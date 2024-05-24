using Code.Scripts.Managers;
using Code.Source.Minotaur.States;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Source.Player
{
    [RequireComponent(typeof(Hittable))]
    public class Player : MonoBehaviour
    {
        public PlayerType playerType;
        public bool isHoldingTreasure;
        public GameObject holdingTreasureIndicator;
        public float timeSinceLastHit = Time.time;
        private Hittable _hittable;
        private bool _isRunningWithTreasure;
        private NavMeshAgent _navMeshAgent;
        private GameObject _target;

        private float _timeSinceLastAttack;

        private void Awake()
        {
            holdingTreasureIndicator.SetActive(false);
            var playerHitPoints = GameVars.Instance.playerHitPoints;
            var hitPoints = playerType == PlayerType.Melee
                ? playerHitPoints
                : playerHitPoints / 2;

            _hittable = GetComponent<Hittable>();
            SetHp(hitPoints);

            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _target = WorldState.Instance.treasure;
            if (playerType == PlayerType.Melee)
                _navMeshAgent.SetDestination(WorldState.Instance.treasure
                    .transform.position);
            else
                _navMeshAgent.SetDestination(WorldState.Instance.Minotaur
                    .transform.position);
        }

        private void Update()
        {
            if (_isRunningWithTreasure) return;

            if (playerType == PlayerType.Melee)
            {
                if (WorldState.Instance.isTreasureGrabbed)
                {
                    _target = WorldState.Instance.Minotaur;
                    _navMeshAgent.SetDestination(_target.transform.position);
                }
                else if (!AttackUtils.CanAttack(transform, _target,
                             GameVars.Instance.playerRadius) || !AttackReady())
                {
                    _navMeshAgent.SetDestination(_target.transform.position);
                }
                else
                {
                    AttackMinotaur();
                }
            }
            else
            {
                var canAttack = AttackUtils.CanAttack(transform, _target,
                    float.PositiveInfinity);

                if (!canAttack ||
                    !AttackReady()) return;
                _navMeshAgent.SetDestination(transform.position);
                AttackMinotaur();
            }
        }

        private void OnDestroy()
        {
            WorldState.Instance.PlayerDead(this);
        }

        private void FindGoal()
        {
            var closestPortal = WorldState.Instance.escapePortals[0];
            var shortestDistance = float.MaxValue;
            foreach (var portal in WorldState.Instance.escapePortals)
            {
                _navMeshAgent.SetDestination(portal.transform.position);

                if (!(_navMeshAgent.remainingDistance <
                      shortestDistance)) continue;

                shortestDistance = _navMeshAgent.remainingDistance;
                closestPortal = portal;
            }

            _navMeshAgent.SetDestination(closestPortal.transform
                .position);
            _isRunningWithTreasure = true;
        }

        private void AttackMinotaur()
        {
            WorldState.Instance.minotaurMachine.Hit(gameObject);
            _timeSinceLastAttack = Time.time;
        }

        private bool AttackReady()
        {
            var cooldownReady = Time.time - _timeSinceLastAttack >
                                GameVars.Instance.playerCooldown;
            var isNotStunned = Time.time - timeSinceLastHit > 1f;
            return cooldownReady &&
                   isNotStunned;
        }

        private void SetHp(int hp)
        {
            _hittable.health = hp;
        }

        public void DropTreasure()
        {
            holdingTreasureIndicator.SetActive(false);
            WorldState.Instance.DropTreasure(transform.position);
        }

        public void GrabTreasure()
        {
            FindGoal();
            isHoldingTreasure = true;
            holdingTreasureIndicator.SetActive(true);
            WorldState.Instance.isTreasureGrabbed = true;
        }
    }
}