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
    #endregion

    private Animator animator;
    public Animator Animator { get { return animator == null ? animator = GetComponentInChildren<Animator>() : animator; } }

    public void TriggerAnimation(string id)
    {
        Animator.SetTrigger(id);
    }
}
