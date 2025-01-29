using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    // D�tection des collisions avec un autre Collider
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision d�tect�e avec : " + collision.gameObject.name);
        Destroy(collision.gameObject);
    }

    // D�tection des d�clenchements si l'un des objets a un Collider en mode Trigger
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger d�tect� avec : " + other.gameObject.name);
    }
}