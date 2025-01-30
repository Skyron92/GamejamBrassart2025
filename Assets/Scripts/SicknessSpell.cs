using UnityEngine;

[CreateAssetMenu(menuName = ("Spell/Sickness"))]
public class SicknessSpell : Spell
{
    public override void CastSpell(PlayerController playerController)
    { 
        Debug.Log("ronce de fouuu !");
    }
}