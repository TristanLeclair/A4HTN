using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Source
{
    public class GameVars : MonoBehaviour
    {
        private static GameVars _instance;

        public static GameVars Instance => _instance
            ? _instance
            : _instance = FindObjectOfType<GameVars>();

        public int playerHitPoints = 5; // N
        public int playerCooldown = 1;
        public int playerRadius = 1;
        public int minotaurRadius = 1; // d
        public int minotaurCooldown = 1;
    }
}