using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages a character controlled by a player
/// </summary>
public class PlayerController : MonoBehaviour
{
    // Device management
    public delegate void DeviceStatusChanged(object sender, PlayerInput playerInput);
    public event DeviceStatusChanged OnDeviceRemoved;
    
    public void OnDeviceLost(PlayerInput playerInput) {
        OnDeviceRemoved?.Invoke(this, playerInput);
    }
    
    // Move
    [SerializeField] CharacterController characterController;
    [SerializeField, Range(0,10)] float moveSpeed = 1;
    private float MoveSpeed => moveSpeed * Time.deltaTime;
    private Vector3 _movement = Vector3.zero;
    
    public void OnMove(InputAction.CallbackContext context) {
        _movement.x = context.ReadValue<float>();
    }
    
    // Jump
    [SerializeField, Range(0,.5f)] float jumpForce = .2f;
    bool _isJumping;
    private float _gravity = -9.81f;
    public bool IsGrounded => characterController.isGrounded;
    
    public void OnJump(InputAction.CallbackContext context) {
        if(IsGrounded && context.performed) _isJumping = true;
    }
    
    private void ApplyGravity() {
        if(IsGrounded && _movement.y < 0) _movement.y = -1f;
        if (_isJumping) {
            _movement.y = jumpForce * -2 * _gravity;
            _isJumping = false;
        }
        _movement.y += _gravity * Time.fixedDeltaTime;
    }
    
    // Spell relative variables
    [HideInInspector] public bool isNearToVoid;
    
    private void FixedUpdate() {
        ApplyGravity();
        characterController.Move(_movement * MoveSpeed);
    }
}