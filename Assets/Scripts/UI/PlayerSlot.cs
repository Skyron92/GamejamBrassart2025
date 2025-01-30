using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlot : MonoBehaviour
{
    [SerializeField] PlayersManager playersManager;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField, Range(1,4)] int slotNumber;

    private void Start()
    {
        OnPlayerDisconnected();
    }

    public void OnPlayerJoined(SpellCaster spellCaster) {
        textMeshPro.text = "Player " + slotNumber;
        image.color = spellCaster.spell.spellColor;
    }

    public void OnPlayerDisconnected() {
        textMeshPro.text = "Player " + slotNumber + "\nPress any\nbutton to join";
        image.color = Color.grey;
    }
}
