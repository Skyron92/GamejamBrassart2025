using UnityEngine;

[CreateAssetMenu(menuName = "Spell/Gaz")]
public class GazSpell : Spell
{
    [SerializeField] private GameObject gazPrefab;
    public override void CastSpell(PlayerController playerController) {
        if(!playerController.GetComponent<SpellCaster>().spellIsAvailable) return;
        Instantiate(gazPrefab, playerController.transform.position, Quaternion.identity);
    }
}