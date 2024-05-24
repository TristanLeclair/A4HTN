using UnityEngine;

namespace Code.Source.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        public GameObject playerPrefab;

        public PlayerType playerType;

        private void Awake()
        {
            // Spawn player
            var playerObject = Instantiate(playerPrefab, transform.position,
                Quaternion.identity);
            playerObject.GetComponent<Player>().playerType = playerType;
        }
    }
}