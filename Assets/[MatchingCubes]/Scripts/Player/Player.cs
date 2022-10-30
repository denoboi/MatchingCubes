using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    public bool IsControlable { get; private set; }
    public bool IsDead { get; set; }
    public bool IsGrounded { get; set; }

    #region Getters
    private PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement { get { return playerMovement == null ? playerMovement = GetComponent<PlayerMovement>() : playerMovement; } }

    private PlayerInput playerInput;
    public PlayerInput PlayerInput { get { return playerInput == null ? playerInput = GetComponent<PlayerInput>() : playerInput; } }

    private PlayerAnimator playerAnimator;
    public PlayerAnimator PlayerAnimator { get { return playerAnimator == null ? playerAnimator = GetComponent<PlayerAnimator>() : playerAnimator; } }

    private PlayerStacker stacker;
    public PlayerStacker Stacker { get { return stacker == null ? stacker = GetComponent<PlayerStacker>() : stacker; } }

    private TrailController trail;
    public TrailController Trail { get { return trail == null ? trail = GetComponent<TrailController>() : trail; } }
    #endregion

    private void OnEnable()
    {
        LevelSystem.Instance.OnLevelStarted.AddListener(EnableControls);
        LevelSystem.Instance.OnLevelFinished.AddListener(DisableControls);
    }

    private void OnDisable()
    {
        LevelSystem.Instance.OnLevelStarted.RemoveListener(EnableControls);
        LevelSystem.Instance.OnLevelFinished.RemoveListener(DisableControls);
    }

    private void Update()
    {
        if (!IsControlable) return;

        PlayerMovement.Move(Vector3.forward);
        PlayerMovement.Swerve(PlayerInput.InputX);
        Trail.ToggleTrail(IsGrounded);
    }

    private void FixedUpdate()
    {
        CheckIfIsGrounded();
    }

    private void EnableControls()
    {
        IsControlable = true;
        IsDead = false;
        PlayerAnimator.TriggerAnimation(PlayerAnimator.RUN_ID);
    }

    private void DisableControls()
    {
        IsControlable = false;
    }

    private void Die()
    {
        if (IsDead) return;

        IsDead = true;
        GameManager.Instance.CompleteLevel(false);
        PlayerAnimator.TriggerAnimation(PlayerAnimator.FALL_ID);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IStackable stackable))
        {
            Stacker.AddStack(stackable);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IObstacle obstacle))
        {
            if (Stacker.Stacks.Count <= 0 && !obstacle.IsInteracted)
                Die();
            else if (Stacker.Stacks.Count > 0)
            {
                obstacle.OnInteracted(Stacker.Stacks[Stacker.Stacks.Count - 1]);
                Events.OnLastStackableChanged.Invoke(Stacker.GetLastStack());
            }
        }
    }

    private void CheckIfIsGrounded()
    {
        IStackable stack = Stacker.GetLastStack();

        if (stack == null) 
            IsGrounded = false;
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(stack.transform.position + Vector3.up, Vector3.down, out hit, 1.05f, groundLayer))
                IsGrounded = true;
            else
                IsGrounded = false;
        }
    }
}
