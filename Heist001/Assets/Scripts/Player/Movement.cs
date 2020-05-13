using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    float moveSpeed;
    Rigidbody rb;
    Vector3 dir;
    bool jumping, onGround;
    Vector3 velocity;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        dir = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        dir = dir.normalized;

        jumping |= Input.GetKeyDown(KeyCode.Space);

        moveSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift)) {
            moveSpeed += speed;
        }
    }

    private void FixedUpdate() {
        
        rb.position += dir * Time.deltaTime * moveSpeed;

        if (jumping) {
            jumping = false;
            Jump();
        }

        rb.velocity += velocity;
        velocity = Vector3.zero;

        onGround = false;
    }

    private void Jump() {
        if (onGround) {
            velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
        }
    }

    private void OnCollisionStay(Collision collision) {
        onGround = true;
    }
}
