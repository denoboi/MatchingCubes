using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const float INPUT_LIMIT = 20;
    private const float INPUT_SMOOTHNESS = 15f;

    private float inputX;
    public float InputX { get { return inputX; } }

    private float smoothX;

    private Vector2 lastInputPosition;

    private void Update()
    {
        GetSwerveInput();
    }

    private void GetSwerveInput()
    {
        if (Input.GetMouseButtonDown(0))
            lastInputPosition = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            smoothX = Mathf.Clamp(Input.mousePosition.x - lastInputPosition.x, -INPUT_LIMIT, INPUT_LIMIT);

            lastInputPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
            smoothX = 0;

        inputX = Mathf.Lerp(inputX, smoothX, Time.deltaTime * INPUT_SMOOTHNESS);
    }
}
