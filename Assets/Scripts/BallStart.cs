using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStart : MonoBehaviour {
    Rigidbody2D rb;
    public Vector2 startVelocity = new Vector2(10f, 10f);
    public Vector2 velocity;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = startVelocity;
    }

    // Update is called once per frame
    void Update() {
        velocity = rb.velocity;
    }
}
