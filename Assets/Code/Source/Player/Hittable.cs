using Code.Scripts.Managers;
using UnityEngine;

namespace Code.Source.Player
{
    public class Hittable : MonoBehaviour
    {
        public int health;

        private Renderer _renderer;
        private readonly Color _hurtColor = Color.red;
        private readonly Color _normalColor = Color.green;
        private Player _player;

        private void Awake()
        {
            // Set color to normal color
            _renderer = GetComponent<Renderer>();
            _player = GetComponent<Player>();

            SetColor(_normalColor);
        }

        public void Hit()
        {
            // TODO: Drop treasure if holding treasure
            if (_player.isHoldingTreasure)
            {
                _player.DropTreasure();
                WorldState.Instance.isTreasureGrabbed = false;
            }

            _player.timeSinceLastHit = Time.time;

            health--;
            // change color for .5 seconds
            SetColor(_hurtColor);
            Invoke(nameof(ResetColor), .5f);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void ResetColor() => SetColor(_normalColor);
        private void SetColor(Color c) => _renderer.material.color = c;
    }
}