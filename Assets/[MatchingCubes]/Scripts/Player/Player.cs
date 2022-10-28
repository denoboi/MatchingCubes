using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsControlable { get; private set; }

    #region Getters
    private PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement { get { return playerMovement == null ? playerMovement = GetComponent<PlayerMovement>() : playerMovement; } }

    private PlayerInput playerInput;
    public PlayerInput PlayerInput { get { return playerInput == null ? playerInput = GetComponent<PlayerInput>() : playerInput; } }

    private PlayerAnimator playerAnimator;
    public PlayerAnimator PlayerAnimator { get { return playerAnimator == null ? playerAnimator = GetComponent<PlayerAnimator>() : playerAnimator; } }
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
    }

    private void EnableControls()
    {
        IsControlable = true;
        PlayerAnimator.TriggerAnimation(PlayerAnimator.RUN_ID);
    }

    private void DisableControls()
    {
        IsControlable = false;
    }
}
