﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtBehaviour : MonoBehaviour
{
    [Header("ArtData")]
    public string artName, authorName, date;
    [Range(1, 100)]
    public int value = 1;
    public Sprite inventoryIcon;

    [Header("ItemData")]
    public float throwStrenght = 2;
    public float animationSteps = 50;
    public float animationTime = 0.01f;

    private Rigidbody rb;
    private Collider col;
    private Vector3 ogScale;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        ogScale = transform.localScale;
    }

    public void Pickup(Transform player) {
        Debug.Log("Grabbed art");
        rb.useGravity = false;
        col.enabled = false;
        StartCoroutine(MoveToPlayer(player));
    }

    public void Drop(Transform lookDir) {
        Debug.Log("Dropped art");
        transform.position = lookDir.position;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(lookDir.forward * throwStrenght, ForceMode.Impulse);
        col.enabled = true;
        StartCoroutine(PlayerDrop());
    }

    IEnumerator MoveToPlayer(Transform player) {
        Vector3 ogPos = transform.position;
        float t = 0;
        for (int i = 0; i < animationSteps; i++) {
            transform.position = Vector3.Lerp(ogPos, player.position, t);
            transform.localScale = Vector3.Lerp(ogScale, Vector3.zero, t);
            t += i / animationSteps;
            yield return new WaitForSeconds(animationTime);
        }
    }

    IEnumerator PlayerDrop() {
        float t = 0;
        for (int i = 0; i < animationSteps; i++) {
            transform.localScale = Vector3.Lerp(Vector3.zero, ogScale, t);
            t += i / animationSteps;
            yield return new WaitForSeconds(animationTime);
        }
    }
}