using System.Collections;
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
    
    // Health
    [HideInInspector] public bool death;
    
    // Move
    [SerializeField] CharacterController characterController;
    [SerializeField, Range(0,100)] float moveSpeed = 1;
    private float MoveSpeed => moveSpeed * Time.deltaTime;
    private Vector3 _movement = Vector3.zero;

    private void Rotate() {
        if(_movement.x == 0) return;
        float rotationAngle = Vector3.Angle(_movement, Vector3.forward);
        if(_movement.x < 0) rotationAngle = -rotationAngle;
        Quaternion rotation = Quaternion.Euler(0, rotationAngle, 0);
        transform.rotation = rotation;
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
    public delegate void SpellChange(SpellCaster caster);
    public event SpellChange onSpellChanged;
    bool _canInteract = true;
    
    public void OnInteract(InputAction.CallbackContext context) {
        if(!context.started || !_canInteract) return;
        StartCoroutine(InteractCooldown());
        onSpellChanged?.Invoke(GetComponent<SpellCaster>());
    }

    private IEnumerator InteractCooldown() {
        float timer = 0;
        _canInteract = false;
        while (timer < 1) {
            timer += Time.fixedDeltaTime;
            yield return null;
        }
        _canInteract = true;
    }
    
    [HideInInspector] public Vector2 direction = Vector2.zero;
    
    public void OnMove(InputAction.CallbackContext context) {
        _movement.x = context.ReadValue<float>();
    }
    
    public void OnAim(InputAction.CallbackContext context) {
        direction = context.ReadValue<Vector2>();
        Debug.Log(direction);
    }

    private void Awake() {
        Physics.IgnoreLayerCollision(7,7);
    }

    private void FixedUpdate() {
        ApplyGravity();
        characterController.Move(_movement * MoveSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Rotate();
    }
}