using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float feverModeDuration;
    [SerializeField] private float feverModeActivationThreshold;

    public bool IsControlable { get; private set; }
    public bool IsDead { get; private set; }
    public bool IsGrounded { get; private set; }
    public bool IsBoosted { get; set; }
    public bool IsJumping { get; set; }

    private int matchCount;
    private Coroutine feverRoutine;

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
        Stacker.OnSuccessfulMatch.AddListener(CheckFeverMode);
    }

    private void OnDisable()
    {
        LevelSystem.Instance.OnLevelStarted.RemoveListener(EnableControls);
        LevelSystem.Instance.OnLevelFinished.RemoveListener(DisableControls);
        Stacker.OnSuccessfulMatch.RemoveListener(CheckFeverMode);
    }

    private void Update()
    {
        if (!IsControlable) return;

        if (!IsJumping)
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

    private void CheckFeverMode()
    {
        if (IsBoosted) return;

        if (feverRoutine != null)
            StopCoroutine(feverRoutine);

        feverRoutine = StartCoroutine(CheckFeverModeCo());
    }

    private IEnumerator CheckFeverModeCo()
    {
        matchCount++;
        if (matchCount >= 3)
        {
            matchCount = 0;
            StartCoroutine(ActivateFeverMode());
        }
        yield return new WaitForSeconds(feverModeActivationThreshold);
        matchCount = 0;
    }

    private IEnumerator ActivateFeverMode()
    {
        IsBoosted = true;
        PlayerMovement.SetSpeedBoost(true);
        Events.OnSpeedBoostChanged.Invoke(true);
        yield return new WaitForSeconds(feverModeDuration);
        IsBoosted = false;
        PlayerMovement.SetSpeedBoost(false);
        Events.OnSpeedBoostChanged.Invoke(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IStackable stackable))
        {
            Stacker.AddStack(stackable);
        }

        if (other.TryGetComponent(out IGate gate))
        {
            gate.OnInteracted(Stacker);
            Events.OnLastStackableChanged.Invoke(Stacker.GetLastStack());
            Stacker.CheckMatches();
        }

        if (other.TryGetComponent(out EndPlatform platform))
        {
            GameManager.Instance.CompleteLevel(true);
            transform.DOLookAt(Vector3.back, 0.5f);
            PlayerAnimator.TriggerAnimation(PlayerAnimator.DANCE_ID);
        }

        if (other.TryGetComponent(out IBoost boost))
        {
            boost.Use(transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IObstacle obstacle))
        {
            if (IsBoosted)
            {
                obstacle.Dispose();
                return;
            }

            if (Stacker.Stacks.Count <= 0 && !obstacle.IsInteracted)
                Die();
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
