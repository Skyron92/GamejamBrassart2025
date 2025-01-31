using UnityEngine;
using UnityEngine.Serialization;

public abstract class Spell : ScriptableObject
{

    [Range(0, 10f)] public float castCooldown = 1;
    public Sprite playerSlotSprite;
    [HideInInspector] public bool taken = false;
    
    public abstract void CastSpell(PlayerController playerController);
}