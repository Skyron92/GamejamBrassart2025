using System;
using UnityEngine;

public class SpellManager : MonoBehaviour {
    public Spell[] spells;

    private void Awake()
    {
        foreach (Spell spell in spells) spell.taken = false;
    }
}