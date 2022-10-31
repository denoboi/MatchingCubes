using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLava : ObstacleBase
{
    [SerializeField] private float interactionResetDuration = 0.25f;

    public override void OnInteracted(IObstacleTarget target)
    {
        ActivateAfterInteraction(target);
        base.OnInteracted(target);
    }

    private void ActivateAfterInteraction(IObstacleTarget target)
    {
        if (IsInteracted) return;

        if (target.transform.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
            target.transform.DOMoveY(-1f, 0.25f);
        }

        StartCoroutine(ActivateAfterInteractionCo());
    }

    private IEnumerator ActivateAfterInteractionCo()
    {
        yield return new WaitForSeconds(interactionResetDuration);
        IsInteracted = false;
    }
}
