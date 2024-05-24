using System.Collections.Generic;
using System.Linq;
using Code.Source.Minotaur;
using Code.Source.Player;
using UnityEngine;

namespace Code.Scripts.Managers
{
    public class WorldState : MonoBehaviour
    {
        private static WorldState _instance;

        public GameObject treasure;
        public bool isTreasureGrabbed;
        public GameObject[] escapePortals;
        public MinotaurMachine minotaurMachine;

        private GameObject[] _players;
        private IEnumerable<Player> _playerScripts;

        public static WorldState Instance => _instance
            ? _instance
            : _instance = FindObjectOfType<WorldState>();

        public GameObject Minotaur => minotaurMachine.gameObject;

        private void Start()
        {
            minotaurMachine = FindObjectOfType<MinotaurMachine>();
            _players = GameObject.FindGameObjectsWithTag("AI");
            _playerScripts = _players.Select(p => p.GetComponent<Player>());
        }

        public IEnumerable<GameObject> GetPlayers()
        {
            return _players;
        }

        public void GrabTreasure()
        {
            isTreasureGrabbed = true;
            treasure.SetActive(false);
        }

        public void DropTreasure(Vector3 position)
        {
            treasure.transform.position = position;
            treasure.SetActive(true);
        }

        public static void WinGame()
        {
            // pause game and show win screen
            Time.timeScale = 0;
            Debug.Log("Players win!");
        }

        public void PlayerDead(Player player)
        {
            _playerScripts = _playerScripts.Where(p => p != player);
            if (_playerScripts.Any()) return;
            // pause game and show lose screen
            Time.timeScale = 0;
            Debug.Log("Players lose!");
        }
    }
}