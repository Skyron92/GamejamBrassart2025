using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHiglighting : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Color defaultColor;

    [ContextMenu("Test")]
    public void Highlight() {
        float animSpeed = .2f;
        image.DOColor(Color.white,animSpeed).onComplete+= () => image.DOColor(defaultColor,animSpeed).onComplete += () =>  image.DOColor(Color.white,animSpeed).onComplete+= () => image.DOColor(defaultColor,animSpeed);
    }
}
