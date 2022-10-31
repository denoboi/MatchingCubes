using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStacker : StackerBase
{
    private void OnEnable()
    {
        OnStacked.AddListener(CheckMatches);
    }

    private void OnDisable()
    {
        OnStacked.RemoveListener(CheckMatches);
    }

    public void CheckMatches()
    {
        StartCoroutine(CheckMatchesCo());
    }

    private IEnumerator CheckMatchesCo()
    {
        yield return new WaitForSeconds(0.25f);

        for (int i = Stacks.Count - 1; i >= 0; i--)
        {
            IStackable currentStack = Stacks[i];
            IStackable previousStack = i + 1 >= Stacks.Count ? null : Stacks[i + 1];
            IStackable nextStack = i - 1 < 0 ? null : Stacks[i - 1];

            if (previousStack == null || nextStack == null) continue;

            if (nextStack.StackType != previousStack.StackType) continue;

            if (nextStack.StackType == currentStack.StackType)
            {
                currentStack.Dispose();
                previousStack.Dispose();
                nextStack.Dispose();

                CheckMatches();
                Events.OnLastStackableChanged.Invoke(GetLastStack());
                break;
            }
        }
    }
}
