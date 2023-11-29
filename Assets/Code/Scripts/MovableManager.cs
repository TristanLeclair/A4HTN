using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Code.Scripts
{
    public class MovableManager : MonoBehaviour
    {
        readonly List<NavMeshAgent> _navMeshAgents = new();
        // Start is called before the first frame update
        
        private Camera _camera;
        private void Awake()
        {
            Assert.IsNotNull(Camera.main, "Camera.main != null");
            _camera = Camera.main;
        }
        private void Start()
        {
            var gameObjects = GameObject.FindGameObjectsWithTag("AI");
            foreach (var a in gameObjects)
            {
                _navMeshAgents.Add(a.GetComponent<NavMeshAgent>());
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
        
            if (!Physics.Raycast(
                    _camera.ScreenPointToRay(Input.mousePosition),
                    out var hit, 100)) return;
            foreach (var a in _navMeshAgents)
            {
                a.SetDestination(hit.point);
            }
        }
    }
}
