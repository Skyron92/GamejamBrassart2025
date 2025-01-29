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
        Camera.main.DOShakePosition(.2f, .5f, 3);
        var stunable = enemy.GetComponent<IStunnable>();
        if (stunable != null) {
            stunable.Stun();
        }
    }
}