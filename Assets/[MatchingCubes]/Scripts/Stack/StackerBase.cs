using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StackerBase : MonoBehaviour, IStacker
{
    [SerializeField] private Transform stackHolder;
    [SerializeField] private Rigidbody playerVisualRigidbody;

    public List<IStackable> Stacks { get; set; }

    public virtual void Start()
    {
        Stacks = new List<IStackable>();
    }

    public virtual void RemoveStack(IStackable stack)
    {
        if (Stacks.Contains(stack))
            Stacks.Remove(stack);
    }

    public virtual void AddStack(IStackable stack)
    {
        if (!Stacks.Contains(stack))
        {
            Stacks.Add(stack);
            stack.transform.SetParent(stackHolder);

            for (int i = Stacks.Count - 1; i >= 0; i--)
            {
                Stacks[i].transform.position += Vector3.up;
                Stacks[i].transform.DOPunchScale((Vector3.right + Vector3.forward) * 0.3f, 0.5f, 1, 1).SetDelay(0.05f * i);
            }

            playerVisualRigidbody.transform.localPosition = Vector3.up * Stacks.Count;
            playerVisualRigidbody.AddForce(Vector3.up * 100);
            stack.transform.localPosition = Vector3.zero;
        }
    }
}
