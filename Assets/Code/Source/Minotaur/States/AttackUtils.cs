using UnityEngine;

namespace Code.Source.Minotaur.States
{
    public static class AttackUtils
    {
        public static bool TargetInRange(Transform attacker, GameObject victim,
            float range)
        {
            return Vector3.Distance(attacker.position,
                victim.transform.position) < range;
        }

        public static bool CanAttack(Transform attacker, GameObject victim,
            float range)
        {
            return TargetInRange(attacker, victim, range) &&
                   Sight.TestSight(attacker, victim, 360f);
        }
    }
}