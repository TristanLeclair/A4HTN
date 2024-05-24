using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Source
{
    public class Sight : MonoBehaviour
    {
        public float viewAngle;

        public Tuple<bool, GameObject> FindClosestVisibleTarget(
            IEnumerable<GameObject> targets)
        {
            GameObject closest = null;
            var found = false;
            foreach (var player in targets)
            {
                if (!TestConeOfSight(transform, player, viewAngle)) continue;
                if (!TestLineOfSight(transform, player)) continue;
                closest = player;
                found = true;
            }

            return new Tuple<bool, GameObject>(found, closest);
        }

        public static bool TestSight(Transform viewer, GameObject target,
            float viewAngle) => TestConeOfSight(viewer, target, viewAngle) &&
                                TestLineOfSight(viewer, target);

        private static bool TestConeOfSight(Transform viewer, GameObject target,
            float viewAngle)
        {
            var targetDir = target.transform.position - viewer.position;
            var angle = Vector3.Angle(targetDir, viewer.forward);

            return angle < viewAngle;
        }

        private static bool TestLineOfSight(Transform viewer, GameObject target)
        {
            var targetDir = target.transform.position - viewer.position;
            if (!Physics.Raycast(viewer.position, targetDir, out var hit))
                return false;

            return hit.transform.gameObject == target;
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.T)) return;
            
            var canSee = TestSight(transform, GameObject.FindWithTag("AI"),
                360f);
            Debug.Log($"Can see: {canSee}");
        }
    }
}