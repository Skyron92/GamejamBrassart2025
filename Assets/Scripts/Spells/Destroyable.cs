using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Destroyable : MonoBehaviour {
    
    [SerializeField] Renderer renderer;
    Material _material;
    [SerializeField] private Color animationColor;
    [SerializeField, Range(1,10)] private float dissolveSpeed = 2;
    [SerializeField] private EnemyTest enemyOwner;

    private void Start() {
        _material = renderer.material;
        if(enemyOwner != null) enemyOwner.Invincible = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Spell")) {
            var spell = other.gameObject.GetComponent<SpellObject>(); 
            spell?.Interact(gameObject);
        }
    }

    public void Destroy() {
        float speedAnimation = .1f;
        Color color = _material.color;
        StartCoroutine(DissolveAnimation(0));
        _material.DOColor(animationColor, speedAnimation).onComplete += () => {
            _material.DOColor(color, speedAnimation).onComplete += () => _material.DOColor(animationColor, speedAnimation).onComplete += () => {
                _material.DOColor(color, speedAnimation);
            };
        };
    }

    public IEnumerator DissolveAnimation(float target) {
        float value = _material.GetFloat("_Dissolve");
        while (value > target) {
            value -= Time.deltaTime * dissolveSpeed;
            _material.SetFloat("_Dissolve", value);
            yield return null;
        }
        if (enemyOwner != null) {
            var hitable = (IHitable)enemyOwner;
            if(hitable != null) hitable.Invincible = false;
        }
        Destroy(gameObject);
    }
}