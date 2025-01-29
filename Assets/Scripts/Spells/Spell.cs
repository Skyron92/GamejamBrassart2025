using UnityEngine;

public abstract class Spell : ScriptableObject
{

    [Range(0, 10f)] public float castCooldown = 1;
    
    public abstract void CastSpell(PlayerController playerController);
}