using UnityEngine;

public class Destroyable : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spell")) {
            other.gameObject.GetComponent<SpellObject>().Interact(gameObject);
        }
    }

    public void Destroy() {
        Destroy(gameObject);
    }
}