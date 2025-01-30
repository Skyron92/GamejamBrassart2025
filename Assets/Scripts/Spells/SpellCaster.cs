using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellCaster : MonoBehaviour
{
    public Spell spell;
    [SerializeField] private PlayerController player;
    [HideInInspector] public bool spellIsAvailable = true;

    private void Awake() {
        spellIsAvailable = true;
    }

    public void OnAction(InputAction.CallbackContext context) {
        if(!context.performed) return;
        spell?.CastSpell(player);
    }
    
    protected IEnumerator StartCooldownTimer() {
        spellIsAvailable = false;
        float timer = 0;
        while (timer < spell.castCooldown) {
            timer += Time.fixedDeltaTime;
            yield return null;
        }
        spellIsAvailable = true;
    }
}