using System;
using UnityEngine;

public class ButtonHelper : MonoBehaviour
{
    [SerializeField] private SpellCaster caster;
    [SerializeField] private GameObject actionTooltip;
    [SerializeField] private ButtonHiglighting buttonHiglighting;

    private void Start() { 
        buttonHiglighting = FindFirstObjectByType<ButtonHiglighting>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            if (other.gameObject.GetComponentInParent<RollingEnemy>().Invincible) {
                if(caster.spell.id != 0) buttonHiglighting.Highlight();
                else actionTooltip.SetActive(true);
            }
            else if(caster.spell.id != 2) buttonHiglighting.Highlight();
                else actionTooltip.SetActive(true);
        }
    }
}
