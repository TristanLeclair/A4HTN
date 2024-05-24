using System;
using UnityEngine;

namespace Code.Source.Player
{
    public class PlayerFactory : MonoBehaviour
    {
        private static PlayerFactory _instance;

        public GameObject MeleePlayerPrefab;
        public GameObject RangedPlayerPrefab;

        public static PlayerFactory Instance => _instance
            ? _instance
            : _instance = FindObjectOfType<PlayerFactory>();

        public GameObject CreatePlayer(PlayerType playerType)
        {
            return playerType switch
            {
                PlayerType.Melee => MeleePlayerPrefab,
                PlayerType.Ranged => RangedPlayerPrefab,
                _ => throw new ArgumentOutOfRangeException(
                    nameof(playerType), playerType, "Invalid player type")
            };
        }
    }
}