using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public Transform respawnPoint; // Point de réapparition du joueur
    public float respawnDelay = 2f; // Délai avant le respawn

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Sauvegarde la position et rotation initiales
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        // Désactiver temporairement le joueur
        gameObject.SetActive(false);

        // Attendre le délai de respawn
        yield return new WaitForSeconds(respawnDelay);

        // Remettre le joueur à sa position initiale ou au point de respawn
        transform.position = respawnPoint != null ? respawnPoint.position : initialPosition;
        transform.rotation = respawnPoint != null ? respawnPoint.rotation : initialRotation;

        // Réactiver le joueur
        gameObject.SetActive(true);
    }
}