using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // Détection des collisions avec un autre Collider
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision détectée avec : " + collision.gameObject.name);
        Destroy(collision.gameObject);
    }

    // Détection des déclenchements si l'un des objets a un Collider en mode Trigger
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger détecté avec : " + other.gameObject.name);
    }
}