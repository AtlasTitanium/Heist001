using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtBehaviour : MonoBehaviour
{
    [Header("ArtData")]
    public string artName, authorName, description;
    [Range(0, 100)]
    public int value = 1;
    public Sprite inventoryIcon;

    [Header("ItemData")]
    public float throwStrenght = 2;
    public float animationSteps = 50;
    public float animationTime = 0.01f;
    //public GameObject colliderObj;

    private Rigidbody rb;
    private Collider col;
    private Vector3 ogScale;
    private ArtData artData;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        ogScale = transform.localScale;

        artData = new ArtData(artName, authorName);
    }

    public ArtData GetData() {
        return artData;
    }

    public void Pickup(Transform player) {
        Debug.Log("Grabbed art");
        rb.useGravity = false;
        StartCoroutine(MoveToPlayer(player));
    }

    public void Drop(Transform lookDir) {
        Debug.Log("Dropped art");
        transform.position = lookDir.position;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(lookDir.forward * throwStrenght, ForceMode.Impulse);
        StartCoroutine(PlayerDrop());
    }

    IEnumerator MoveToPlayer(Transform player) {
        Vector3 ogPos = transform.position;
        float t = 0;
        for (int i = 0; i < animationSteps; i++) {
            transform.position = Vector3.Lerp(ogPos, player.position, t);
            transform.localScale = Vector3.Lerp(ogScale, Vector3.zero, t);
            //colliderObj.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            t += i / animationSteps;
            if (t >= 0.5f) {
                col.enabled = false;
                //colliderObj.SetActive(false);
            }
            yield return new WaitForSeconds(animationTime);
        }
    }

    IEnumerator PlayerDrop() {
        float t = 0;
        for (int i = 0; i < animationSteps; i++) {
            transform.localScale = Vector3.Lerp(Vector3.zero, ogScale, t);
            //colliderObj.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            t += i / animationSteps;
            if(t >= 0.5f) {
                //colliderObj.SetActive(true);
                col.enabled = true;
            }
            yield return new WaitForSeconds(animationTime);
        }
        
    }
}

public class ArtData {
    public string artName, authorName;

    public ArtData(string artName, string authorName) {
        this.artName = artName;
        this.authorName = authorName;
    }
}

