using UnityEngine;

public class GazObject : SpellObject
{
    [SerializeField, Range(1, 10)] private float damage;
    public override void Interact(GameObject enemy) {
        var hitable = enemy.GetComponent<IHitable>();
        if(hitable.Invincible) return;
        hitable.TakeDamage(damage);
    }
}