using UnityEngine;
using UnityEngine.UI;

public class SpellSelector : MonoBehaviour
{
    [SerializeField] private Color selectedColor, normalColor;
    [SerializeField] private Image image;

    public void Select(bool selected) {
        image.color = selected ? selectedColor : normalColor;
    }
}