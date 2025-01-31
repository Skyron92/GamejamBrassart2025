using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHiglighting : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Color defaultColor;
    
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Start()
    {
        ScaleAnimation();
    }

    private void OnEnable() {
        if(!spriteRenderer) return;
        float animSpeed = .2f;
        spriteRenderer.DOColor(Color.white,animSpeed).onComplete+= () => spriteRenderer.DOColor(defaultColor,animSpeed).onComplete += () => 
            spriteRenderer.DOColor(Color.white,animSpeed).onComplete+= () => spriteRenderer.DOColor(defaultColor,animSpeed);
        transform.DOScale(Vector3.one * .3f, animSpeed).onComplete += () =>
            transform.DOScale(Vector3.one * .4f, animSpeed).onComplete += () =>
                transform.DOScale(Vector3.one * .3f, animSpeed).onComplete +=
                    () => transform.DOScale(Vector3.one * .4f, animSpeed);
    }

    [ContextMenu("Test")]
    public void Highlight() {
        float animSpeed = .2f;
        image.DOColor(Color.white,animSpeed).onComplete+= () => image.DOColor(defaultColor,animSpeed).onComplete += () =>  image.DOColor(Color.white,animSpeed).onComplete+= () => image.DOColor(defaultColor,animSpeed);
        transform.DOScale(Vector3.one * 1.5f, animSpeed).onComplete += () =>
            transform.DOScale(Vector3.one, animSpeed).onComplete += () =>
                transform.DOScale(Vector3.one * 1.5f, animSpeed).onComplete +=
                    () => transform.DOScale(Vector3.one, animSpeed);
    }

    public void ScaleAnimation() {
        float speedAnim = .5f;
        transform.DOScale(Vector3.one * 1.1f, speedAnim).onComplete += () =>
            transform.DOScale(Vector3.one , speedAnim).onComplete += () => ScaleAnimation();
    }
}
