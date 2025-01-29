using UnityEngine;

public class EnemyTest : MonoBehaviour, IHitable, IStunnable
{
    [SerializeField, Range(1,10)] private float health = 2;
    float IHitable.Health {
        get => health;
        set => health = value < 0 ? 0 : value;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Spell")) {
            other.gameObject.GetComponentInParent<SpellObject>().Interact(gameObject);
        }
    }

    public void TakeDamage(float damage) {
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