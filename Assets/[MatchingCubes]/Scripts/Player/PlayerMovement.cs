using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementData movementData;

    private bool isBoosted;

    public void Move(Vector3 direction)
    {
        transform.Translate(direction * (isBoosted ? movementData.BoostedMovementSpeed : movementData.BaseMovementSpeed) * Time.deltaTime);
    }

    public void Swerve(float deltaX)
    {
        Vector3 dir = new Vector3(deltaX, 0, 0);
        transform.Translate(dir * movementData.BaseSwerveSpeed * Time.deltaTime);
        HandleClamping();
    }

    private void HandleClamping()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -movementData.ClampValueX, movementData.ClampValueX);
        transform.position = pos;
    }
}
