using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Spell/Blood")]
public class BloodSpell : Spell
{
    [SerializeField] private GameObject bloodPlatformPrefab, bloodWallPrefab, bloodWayPrefab;
    private GameObject _bloodObjectInstance;
    public override void CastSpell(PlayerController playerController) {
        if(!playerController.GetComponent<SpellCaster>().spellIsAvailable) return;
        if(_bloodObjectInstance != null) Destroy(_bloodObjectInstance);
        _bloodObjectInstance = Instantiate(GetBloodPlatformToSpawn(playerController), playerController.transform.position, Quaternion.identity);
    }

    private GameObject GetBloodPlatformToSpawn(PlayerController playerController) {
        
        return !playerController.IsGrounded ? bloodPlatformPrefab : !playerController.isNearToVoid ? bloodWallPrefab : bloodWayPrefab;
    }
}