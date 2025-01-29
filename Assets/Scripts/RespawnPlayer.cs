using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public Transform respawnPoint; // Point de r�apparition du joueur
    public float respawnDelay = 2f; // D�lai avant le respawn

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
        // D�sactiver temporairement le joueur
        gameObject.SetActive(false);

        // Attendre le d�lai de respawn
        yield return new WaitForSeconds(respawnDelay);

        // Remettre le joueur � sa position initiale ou au point de respawn
        transform.position = respawnPoint != null ? respawnPoint.position : initialPosition;
        transform.rotation = respawnPoint != null ? respawnPoint.rotation : initialRotation;

        // R�activer le joueur
        gameObject.SetActive(true);
    }
}