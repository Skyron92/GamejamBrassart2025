using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlot : MonoBehaviour
{
    [SerializeField] PlayersManager playersManager;
    [SerializeField] Image image;
    [SerializeField] Sprite disconnectedSprite;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField, Range(1,4)] int slotNumber;

    private void Start() {
        OnPlayerDisconnected();
        TextAnimation();
    }

    public void OnPlayerJoined(SpellCaster spellCaster)
    {
        textMeshPro.text = "";// "Player " + slotNumber;
        image.sprite = spellCaster.spell.playerSlotSprite;
    }

    public void OnPlayerDisconnected() {
        textMeshPro.text = "Player " + slotNumber + "\nPress any\nbutton to join";
        image.sprite = disconnectedSprite;
    }

    public void TextAnimation() {
        float speedAnim = .5f;
        textMeshPro.transform.DOScale(Vector3.one * 1.1f, speedAnim).onComplete += () =>
        textMeshPro.transform.DOScale(Vector3.one , speedAnim).onComplete += () => TextAnimation();
    }
}
