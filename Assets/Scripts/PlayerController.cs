using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages a character controlled by a player
/// </summary>
public class PlayerController : MonoBehaviour, IHitable
{
    // Device management
    public delegate void DeviceStatusChanged(object sender, PlayerInput playerInput);
    public event DeviceStatusChanged OnDeviceRemoved;
    public int indexPlayer;
    
    public void OnDeviceLost(PlayerInput playerInput) {
        OnDeviceRemoved?.Invoke(this, playerInput);
    }
    
    // Health
    [HideInInspector] public bool death;
    
    [SerializeField] Animator animator;
    private string _speedParameter = "Speed";
    private string _jumpParameter = "Jump";
    private string _castParameter = "CastSpell";
    private string _LandParameter = "Landing";
    
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
        if(death) return;
        if (IsGrounded && context.performed) {
            _isJumping = true;
            animator.SetTrigger(_jumpParameter);
        }
    }
    
    private void ApplyGravity() {
        if(IsGrounded && _movement.y < 0) _movement.y = -1f;
        if (_isJumping) {
            _movement.y = jumpForce * -2 * _gravity;
            _isJumping = false;
        }
        _movement.y += _gravity * Time.fixedDeltaTime;
        animator.SetBool(_LandParameter, !IsGrounded && _movement.y < 0);
    }
    
    // Spell relative variables
    [HideInInspector] public bool isNearToVoid;
    public delegate void SpellChange(SpellCaster caster);
    public event SpellChange onSpellChanged;
    bool _canInteract = true;
    
    public void OnInteract(InputAction.CallbackContext context) {
        if(death) return;
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
    
    [HideInInspector] public Vector3 direction = Vector3.zero;
    private float _health;

    public void OnMove(InputAction.CallbackContext context) {
        if(death) return;
        _movement.x = context.ReadValue<float>();
        animator.SetFloat(_speedParameter, context.canceled? 0 : Mathf.Abs(_movement.x));
    }
    
    public void OnAim(InputAction.CallbackContext context) {
        if(death) return;
        direction = context.ReadValue<Vector2>();
    }

    private void Awake() {
        Physics.IgnoreLayerCollision(7,7);
        _material = renderer.material;
    }

    public bool isTp;
    private void FixedUpdate() {
        if(death) return;
        if (!isTp)
        {
            characterController.Move(_movement * MoveSpeed);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        else isTp = false;
        ApplyGravity();
        Rotate();
    }

    public void CastAnimation() {
        if(death) return;
        animator.SetTrigger(_castParameter);
        Invoke("DisableCastAnimation", .5f);
    }

    public void DisableCastAnimation() {
        if(death) return;
        animator.ResetTrigger(_castParameter);
    }
    
    [SerializeField] private Renderer renderer;
    private Material _material;
    [SerializeField] private GameObject visualGO, flowerPrefab, flowerInstance;
    [SerializeField] private SpriteRenderer respawnVFX;

    float IHitable.Health {
        get => _health;
        set => _health = value;
    }

    public delegate void DeadEvent();
    public event DeadEvent OnDead;
    public event DeadEvent OnRespawn;

    public bool Invincible { get; set; }
    public void TakeDamage(float damage) {
        if(death) return;
        float speedAnimation = .1f;
        var color = _material.color;
        _material.DOColor(Color.red, speedAnimation).onComplete += () => {
            _material.DOColor(color, speedAnimation).onComplete += () => _material.DOColor(Color.red, speedAnimation).onComplete += () => {
                _material.DOColor(color, speedAnimation).onComplete += () => Die();
            };
        };
    }

    public void Die() {
        death = true;
        visualGO.SetActive(false);
        flowerInstance = Instantiate(flowerPrefab, transform.position, Quaternion.identity);
        flowerInstance.GetComponentInChildren<Flower>().playerController = this;
        var pm = FindFirstObjectByType<PlayersManager>();
        pm.ChangePlayerDead();
    }

    public void Respawn(PlayerController playerController, GameObject flower) {
        playerController.respawnVFX.DOFade(1, .1f);
        playerController.respawnVFX.transform.DOScale(Vector3.zero, .5f).onComplete += () => {
            CancelRespawn(playerController);
            playerController.visualGO.SetActive(true);
            playerController.death = false;
            Destroy(flower);
            playerController.OnRespawn?.Invoke();
        };
        var pm = FindFirstObjectByType<PlayersManager>();
        pm.ChangePlayerDead();
    }

    public void CancelRespawn(PlayerController playerController) {
        playerController.respawnVFX.color = new Color(respawnVFX.color.r, respawnVFX.color.g, respawnVFX.color.b, 0f);
        playerController.respawnVFX.transform.localScale = Vector3.one;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Flower")) {
            var flower = other.gameObject.GetComponent<Flower>();
            if(flower.playerController == this) return;
            if (flower.playerController.death) {
                Respawn(flower.playerController, other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            var flower = other.gameObject.GetComponent<Flower>();
            if (flower.playerController.death) CancelRespawn(flower.playerController);
        }
    }

    private void OnEnable() {
        _movement = Vector3.zero;
    }
}