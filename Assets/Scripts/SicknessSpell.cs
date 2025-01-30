using UnityEngine;

[CreateAssetMenu(menuName = ("Spell/Sickness"))]
public class SicknessSpell : Spell {
    [SerializeField] GameObject sicknessPrefab;
    public override void CastSpell(PlayerController playerController) {
        /*var dir = playerController.transform.position + playerController.direction;
        dir.Normalize();
        if (Physics.Raycast(playerController.transform.position, dir,
                out RaycastHit hit, 100)) {
            var instance = Instantiate(sicknessPrefab, playerController.transform.position, Quaternion.identity);
            if(instance == null) return;
            var sicknessObject = instance.GetComponent<SicknessObject>();
            if(sicknessObject == null) return;
            sicknessObject.origin = playerController.transform;
            sicknessObject.target = hit.point;
            sicknessObject.Init();
        }*/
    }
}