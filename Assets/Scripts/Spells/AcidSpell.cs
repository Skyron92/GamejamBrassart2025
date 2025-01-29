using UnityEngine;

[CreateAssetMenu(menuName = "Spell/Acid")]
public class AcidSpell : Spell {
    [SerializeField] GameObject acidPrefab;
    [SerializeField, Range(1, 10)] private float speedProjection = 1; 
    public override void CastSpell(PlayerController playerController) {
        if(!playerController.GetComponent<SpellCaster>().spellIsAvailable) return;
        var acidInstance = Instantiate(acidPrefab, playerController.transform.position + playerController.transform.right * 1.5f, Quaternion.identity);
        acidInstance.GetComponent<Rigidbody>().AddForce(playerController.transform.right * speedProjection, ForceMode.Impulse);
    }
}