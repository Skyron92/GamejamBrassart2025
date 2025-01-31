using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour {
    private float rotationSpeed = .3f ;
    private float Acceleration => rotationSpeed * .1f;
    [SerializeField] PlayersManager playersManager;
    List<GameObject> players = new List<GameObject>();
    [SerializeField] private Image fade;
    [SerializeField] private Transform bossPosition;

    private void Start()
    {
        StartCoroutine(StartRotation());
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            players.Add(other.gameObject);
            if (players.Count == playersManager.PlayerCount) {
                rotationSpeed = 1f;
                fade.DOFade(1, .3f).onComplete += () => {
                    foreach (var spellCaster in playersManager.players) {
                        spellCaster.transform.position = bossPosition.position;
                    }
                    fade.DOFade(0, .3f);
                };
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            players.Remove(other.gameObject);
        }
    }

    IEnumerator StartRotation() {
        float increment = Acceleration;
        while (true) {
            transform.Rotate(new Vector3(0f, 0f, increment), Space.Self);
            if(increment < Acceleration) increment += Acceleration;
            yield return null;
        }
    }
}