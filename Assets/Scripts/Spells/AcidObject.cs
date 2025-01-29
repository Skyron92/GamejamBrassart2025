using UnityEngine;

public class AcidObject : SpellObject {
    public override void Interact(GameObject enemy) {
        var destroyable = enemy.GetComponent<Destroyable>(); 
        destroyable?.Destroy();
    }
}