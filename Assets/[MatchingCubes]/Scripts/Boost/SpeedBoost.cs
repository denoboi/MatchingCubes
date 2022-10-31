using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : BoostBase
{
    public override void Use(Transform user)
    {
        StartCoroutine(UseCo(user));
    }

    private IEnumerator UseCo(Transform user)
    {
        if (user.TryGetComponent(out Player player))
        {
            player.IsBoosted = true;
            player.PlayerMovement.SetSpeedBoost(true);
            yield return new WaitForSeconds(boostDuration);
            player.IsBoosted = false;
            player.PlayerMovement.SetSpeedBoost(false);
        }
    }
}
