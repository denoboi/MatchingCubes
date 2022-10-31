using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    #region Animation IDS
    public const string RUN_ID = "Run";
    public const string JUMP_ID = "Jump";
    public const string FALL_ID = "Fall";
    public const string LAND_ID = "Land";
    public const string STACK_ID = "StackCount";
    public const string DANCE_ID = "Dance";
    #endregion

    private Animator animator;
    public Animator Animator { get { return animator == null ? animator = GetComponentInChildren<Animator>() : animator; } }

    private IStacker stacker;
    public IStacker Stacker { get { return stacker == null ? stacker = GetComponent<IStacker>() : stacker; } }

    public void TriggerAnimation(string id)
    {
        Animator.SetTrigger(id);
    }

    private void Update()
    {
        UpdateAnimatorValues();
    }

    private void UpdateAnimatorValues()
    {
        Animator.SetFloat(STACK_ID, Stacker.Stacks.Count);
    }
}
