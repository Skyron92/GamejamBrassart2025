using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Spell/Blood")]
public class BloodSpell : Spell
{
    [SerializeField] private GameObject bloodPlatformPrefab, bloodWallPrefab, bloodWayPrefab;
    [HideInInspector] public GameObject bloodObjectInstance;
    public override void CastSpell(PlayerController playerController) {
        if(!playerController.GetComponent<SpellCaster>().spellIsAvailable) return;
        if(bloodObjectInstance != null) Destroy(bloodObjectInstance);
        bloodObjectInstance = Instantiate(GetBloodPlatformToSpawn(playerController), playerController.transform.position, Quaternion.identity);
        bloodObjectInstance.transform.Rotate(new Vector3(0,playerController.transform.rotation.eulerAngles.y - 90,0));
    }

    private GameObject GetBloodPlatformToSpawn(PlayerController playerController) {
        return !playerController.IsGrounded ? bloodPlatformPrefab : !playerController.isNearToVoid ? bloodWallPrefab : bloodWayPrefab;
    }
}