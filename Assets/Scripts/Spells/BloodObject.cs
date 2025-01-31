using DG.Tweening;
using UnityEngine;

public class BloodObject : SpellObject {
    
    [SerializeField] private Collider collider;
    private void Awake() {
        transform.localScale= new Vector3(0,0,1);
        collider.enabled = false;
        transform.DOScale(Vector3.one, .1f).SetEase(Ease.Flash).onComplete += () => collider.enabled = true;
    }

    public override void Interact(GameObject enemy) {
        Camera.main.DOShakePosition(.5f, 1f, 10);
        var stunable = enemy.GetComponent<IStunnable>();
        if (stunable != null) {
            stunable.Stun();
        }
        transform.DOScale(Vector3.zero, .1f).SetEase(Ease.Flash).onComplete += () => Destroy(gameObject);
    }
}