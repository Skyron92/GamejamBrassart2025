using System;
using UnityEngine;

public class SpellManager : MonoBehaviour {
    public Spell[] spells;
    public SpellSelector[] spellSelector;

    public void Select(int spellIndex, bool selected) {
        spells[spellIndex].taken = !selected;
        spellSelector[spellIndex].Select(selected);
    }

    private void Awake() {
        foreach (Spell spell in spells) spell.taken = false;
    }
}