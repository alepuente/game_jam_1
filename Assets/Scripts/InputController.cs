using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.LeftShift;

    public static InputController Instance { get; private set; }

    [Header("Movement Settings")]
    public float speed = 5.0f;
    // Jump 
    public float jumpForce = 10.0f;
    public float jumpCooldown = 1f;
    public float airMultiplier = 1f;
  
    // Dash 
    public float dashCooldown = 1f;  
    public float dashDistance = 5.0f;
    


    [Header("Camera Settings")]
    public float mouseSensitivity = 100f;
    public float distanceFromTarget = 2f;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float rotationSmoothTime = 0.12f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
