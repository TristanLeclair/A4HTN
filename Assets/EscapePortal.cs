using Code.Scripts.Managers;
using Code.Source.Player;
using UnityEngine;

public class EscapePortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // if is players layer and player is holding treasure
        if (other.gameObject.layer != LayerMask.NameToLayer("Players")) return;

        if (other.GetComponent<Player>().isHoldingTreasure)
            WorldState.WinGame();
    }
}