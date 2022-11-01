using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    private Player player;
    public Player Player { get { return player == null ? player = GetComponentInParent<Player>() : player; } }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IObstacle obstacle))
        {
            if (Player.IsBoosted)
            {
                obstacle.Dispose();
                return;
            }

            if (Player.Stacker.Stacks.Count <= 0 && !obstacle.IsInteracted)
                Player.Die();
        }
    }
}
