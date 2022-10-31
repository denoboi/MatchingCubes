using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JumpBoost : BoostBase
{
    [SerializeField] private Transform jumpPoint;
    [SerializeField] private Ease jumpEase;

    public override void Use(Transform user)
    {
        if (user.TryGetComponent(out Player player))
        {
            Rigidbody rigidbody = null;
            IStackable stack = player.Stacker.GetLastStack();
            rigidbody = stack.transform.GetComponent<Rigidbody>();

            if (rigidbody == null)
                rigidbody = player.Stacker.PlayerVisualRigidbody;

            if (rigidbody == null) return;

            rigidbody.isKinematic = true;
            player.IsJumping = true;
            player.PlayerAnimator.TriggerAnimation(PlayerAnimator.JUMP_ID);
            player.transform.DOJump(jumpPoint.position, 5, 1, boostDuration).SetEase(jumpEase)
                .OnComplete(()=> 
                { 
                    player.IsJumping = false; 
                    rigidbody.isKinematic = false;
                    player.PlayerAnimator.TriggerAnimation(PlayerAnimator.LAND_ID);
                });

            JumpMotion(player.Stacker.Stacks, player.Stacker.PlayerVisualRigidbody);
        }
    }

    private void JumpMotion(List<IStackable> stacks, Rigidbody player)
    {
        var rigidbodies = stacks.Select(r => r.transform.GetComponent<Rigidbody>()).Reverse().ToList();

        float forceMultiplier = 15f;
        float force = 45f;
        Vector3 defaultGravity = Physics.gravity;

        foreach (var r in rigidbodies)
        {
            r.AddForce(Vector3.up * force);
            force += forceMultiplier;
        }

        player.AddForce(Vector3.up * force);
        Physics.gravity = Vector3.zero;
        DOTween.To(() => Physics.gravity, x => Physics.gravity = x, defaultGravity, boostDuration).SetEase(Ease.InSine);
    }
}
