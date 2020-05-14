using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFloorTest : MonoBehaviour
{
    public Transform[] floors;
    public Transform[] openFloorPositions;
    public float speed = 2;
    private Vector3[] ogFloorPos;
    private bool active = false;
    private bool opening = false;
    float teaTime = 0.0f;

    private void Start() {
        ogFloorPos = new Vector3[floors.Length];
        int i = 0;
        foreach (Transform t in floors) {
            ogFloorPos[i] = t.position;
            i++;
        }
    }

    private void Update() {
        if (active) {
            if (opening) {
                teaTime += 0.01f * Time.deltaTime;
                if(teaTime >= 1.0) {
                    active = false;
                }
            } else {
                teaTime -= 0.001f;
                if (teaTime <= -0.1f) {
                    active = false;
                }
            }
            int h = 0;
            foreach (Transform t in floors) {
                t.position = Vector3.Lerp(ogFloorPos[h], openFloorPositions[h].position, teaTime);
                h++;
            }
        }
    }

    public void Activate() {
        active = true;
        opening = true;
    }

    public void Deactivate() {
        active = true;
        opening = false;
    }
}
