using DG.Tweening;
using UnityEngine;

public class EnemyTest : MonoBehaviour, IHitable, IStunnable
{
    [SerializeField, Range(1,10)] private float health = 2;
    float IHitable.Health {
        get => health;
        set => health = value < 0 ? 0 : value;
    }
    
     [SerializeField] Renderer renderer;
     private Material _material;
     private void Awake() {
         _material = renderer.material;
     }

     private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Spell")) {
            other.gameObject.GetComponentInParent<SpellObject>().Interact(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        float speedAnimation = .1f;
        var color = _material.color;
        _material.DOColor(Color.red, speedAnimation).onComplete += () => {
            _material.DOColor(color, speedAnimation).onComplete += () => _material.DOColor(Color.red, speedAnimation).onComplete += () => {
                _material.DOColor(color, speedAnimation);
            };
        };
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }

    public void Stun()
    {
        Debug.Log("Stunned");
    }
}