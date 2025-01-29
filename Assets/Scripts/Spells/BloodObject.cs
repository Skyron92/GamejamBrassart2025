using DG.Tweening;
using UnityEngine;

public class BloodObject : MonoBehaviour {
    
    [SerializeField] private Collider collider;
    private void Awake() {
        transform.localScale= new Vector3(0,0,1);
        collider.enabled = false;
        transform.DOScale(Vector3.one, .1f).SetEase(Ease.Flash).onComplete += () => collider.enabled = true;
    }
}