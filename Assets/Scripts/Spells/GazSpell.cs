using UnityEngine;

[CreateAssetMenu(menuName = "Spell/Gaz")]
public class GazSpell : Spell
{
    [SerializeField] private GameObject gazPrefab;
    public override void CastSpell(PlayerController playerController) {
        if(!playerController.GetComponent<SpellCaster>().spellIsAvailable) return;
        var instance = Instantiate(gazPrefab, playerController.transform.position, Quaternion.identity);
        instance.transform.Rotate(new Vector3(0,playerController.transform.rotation.eulerAngles.y - 90,0));
    }
}