using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpForce = 8f;
    public float gravity = -9.81f;
    
    [Header("Camera")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;
    
    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private SurvivalData survivalData;
    
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        // Mouse look
        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity;
        float mouseY = Mouse.current.delta.y.ReadValue() * mouseSensitivity;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
        
        // Movement
        Vector2 move = Keyboard.current.wasdComposite.ReadValue<Vector2>();
        Vector3 moveDirection = transform.right * move.x + transform.forward * move.y;
        moveDirection = moveDirection.normalized;
        
        bool isRunning = Keyboard.current.leftShiftKey.isPressed && survivalData.stamina > 10;
        float speed = isRunning ? runSpeed : walkSpeed;
        
        controller.Move(moveDirection * speed * Time.deltaTime);
        
        // Gravity
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;
        else
            velocity.y += gravity * Time.deltaTime;
            
        controller.Move(velocity * Time.deltaTime);
    }
}