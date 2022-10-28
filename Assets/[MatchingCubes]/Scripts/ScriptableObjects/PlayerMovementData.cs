using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Scriptable Objects/Player/MovementData")]
public class PlayerMovementData : ScriptableObject
{
    public float BaseMovementSpeed = 5f;
    public float BoostedMovementSpeed = 7.5f;

    public float BaseSwerveSpeed = 1f;
    public float ClampValueX = 1.5f;
}
