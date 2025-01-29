using System;
using UnityEngine;

public class GazObject : SpellObject
{
    private void Awake()
    {
        Debug.Log(transform.eulerAngles.y);
    }

    [SerializeField, Range(1, 10)] private float damage;
    public override void Interact(GameObject enemy) {
        var hitable = enemy.GetComponent<IHitable>();
        hitable.TakeDamage(damage);
    }
}