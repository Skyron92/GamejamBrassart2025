using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    // D�tection des collisions avec un autre Collider
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detecte avec : " + collision.gameObject.name);
    }

    // D�tection des d�clenchements si l'un des objets a un Collider en mode Trigger
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detecte avec : " + other.gameObject.name);
        other.gameObject.SetActive(false);
    }
}