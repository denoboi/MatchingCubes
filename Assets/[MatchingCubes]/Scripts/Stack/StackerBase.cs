using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class StackerBase : MonoBehaviour, IStacker
{
    [SerializeField] private Transform stackHolder;
    [SerializeField] private Rigidbody playerVisualRigidbody;
    public Rigidbody PlayerVisualRigidbody => playerVisualRigidbody;

    private const float ADDITIONAL_Y_POS = 0.75f;

    public List<IStackable> Stacks { get; set; }

    public Event OnStacked = new Event();

    public virtual void Start()
    {
        Stacks = new List<IStackable>();
    }

    public virtual void RemoveStack(IStackable stack)
    {
        if (Stacks.Contains(stack))
        {
            Stacks.Remove(stack);
            stack.OnUnstacked();
        }
    }

    public virtual void AddStack(IStackable stack)
    {
        if (!Stacks.Contains(stack) && !stack.IsStacked)
        {
            Stacks.Add(stack);
            stack.OnStacked(this);
            stack.transform.SetParent(stackHolder);

            Vector3 targetPlayerPosition = (Vector3.up * ADDITIONAL_Y_POS) * Stacks.Count;
            if (playerVisualRigidbody.transform.localPosition.y < targetPlayerPosition.y)
            {
                playerVisualRigidbody.transform.localPosition = targetPlayerPosition;
                playerVisualRigidbody.AddForce(Vector3.up * 100);
            }

            playerVisualRigidbody.velocity = Vector3.zero;

            int j = 0;
            for (int i = Stacks.Count - 1; i >= 0; i--)
            {
                Transform stackTransform = Stacks[i].transform;

                Vector3 newStackPosition = (Vector3.up * ADDITIONAL_Y_POS) * j;

                if (stackTransform.localPosition.y < newStackPosition.y)
                    stackTransform.localPosition = newStackPosition;

                if (DOTween.IsTweening(stackTransform.gameObject.GetInstanceID()))
                    DOTween.Kill(stackTransform.gameObject.GetInstanceID(), true);
                stackTransform.DOPunchScale((Vector3.right + Vector3.forward) * 0.3f, 0.5f, 1, 1).SetDelay(0.05f * i).OnComplete(()=> stackTransform.localScale = Vector3.one).SetId(stackTransform.gameObject.GetInstanceID());
                j++;
            }

            stack.transform.localPosition = Vector3.zero;
            Events.OnLastStackableChanged.Invoke(GetLastStack());
            OnStacked.Invoke();
        }
    }

    public IStackable GetLastStack()
    {
        return Stacks.Count > 0 ? Stacks[Stacks.Count - 1] : null;
    }
}
