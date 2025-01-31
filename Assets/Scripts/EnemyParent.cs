using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    // D�tection des d�clenchements si l'un des objets a un Collider en mode Trigger
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(1);
        }
    }
}