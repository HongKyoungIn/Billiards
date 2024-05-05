using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallConflict : MonoBehaviour {
    (Vector2, Vector2) calculateBall2BallCollision(Vector2 v1, Vector2 v2, Vector2 c1, Vector2 c2, float e = 1f) {
        Vector2 basisX = (c2 - c1).normalized;
        Vector2 basisY = Vector2.Perpendicular(basisX);

        float sin1, sin2, cos1, cos2;

        if (v1.magnitude < 0.0001f) {
            sin1 = 0;
            cos1 = 1;
        }
        else {
            float dotProduct = Vector2.Dot(v1, basisX);
            cos1 = dotProduct / v1.magnitude;
            Vector3 cross = Vector3.Cross(v1, basisX);
            float crossMagnitude = cross.magnitude / v1.magnitude;
            sin1 = cross.z > 0 ? Mathf.Min(crossMagnitude, 1f) : -Mathf.Min(crossMagnitude, 1f); // NaN 방지
        }

        if (v2.magnitude < 0.0001f) {
            sin2 = 0;
            cos2 = 1;
        }
        else {
            float dotProduct = Vector2.Dot(v2, basisX);
            cos2 = dotProduct / v2.magnitude;
            Vector3 cross = Vector3.Cross(v2, basisX);
            float crossMagnitude = cross.magnitude / v2.magnitude;
            sin2 = cross.z > 0 ? Mathf.Min(crossMagnitude, 1f) : -Mathf.Min(crossMagnitude, 1f); // NaN 방지
        }

        Vector2 u1, u2;
        u1 = ((1 - e) * v1.magnitude * cos1 + (1 * e) * v2.magnitude * cos2) / 2 * basisX - v1.magnitude * sin1 * basisY;
        u2 = ((1 + e) * v1.magnitude * cos1 + (1 - e) * v2.magnitude * cos2) / 2 * basisX - v2.magnitude * sin2 * basisY;

        return (u1, u2);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ball")) {
            Rigidbody2D ball1RB = gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D ball2RB = collision.gameObject.GetComponent<Rigidbody2D>();

            Vector2 v1 = gameObject.GetComponent<BallStart>().velocity;
            Vector2 v2 = collision.gameObject.GetComponent<BallStart>().velocity;

            (ball1RB.velocity, ball2RB.velocity) = calculateBall2BallCollision(v1, v2, ball1RB.position, ball2RB.position);
        }
    }
}
