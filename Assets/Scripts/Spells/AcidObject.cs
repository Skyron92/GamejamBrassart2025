using System.Collections;
using UnityEngine;

public class AcidObject : SpellObject {
    
    [SerializeField] Renderer renderer;
    Material _material;
    
    private void Awake() {
        _material = renderer.material;
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }

    public override void Interact(GameObject enemy) {
        var destroyable = enemy.GetComponent<Destroyable>(); 
        destroyable?.Destroy();
    }
    
    public IEnumerator DissolveAnimation(float target) {
        float value = _material.GetFloat("_Dissolve");
        while (value > target) {
            value -= Time.deltaTime;
            _material.SetFloat("_Dissolve", value);
            yield return null;
        }
        Destroy(gameObject);
    }
}