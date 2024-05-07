using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour {
    private float zRotation = 0f;
    public float rotationSpeed = 50f;
    public GameObject whiteBall;
    public Transform launchPos;
    public float ballSpeed = 10f;
    public LineRenderer lineRenderer; // LineRenderer ����

    void Update() {
        zRotation += Input.GetAxisRaw("Vertical") * Time.deltaTime * rotationSpeed;
        zRotation = Mathf.Clamp(zRotation, -60f, 60f); // Mathf.Clamp ������� ����ȭ
        transform.eulerAngles = new Vector3(0, 0, zRotation);

        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject ball = Instantiate(whiteBall, launchPos.position, Quaternion.Euler(Vector3.zero));
            Vector2 startVelocity = new Vector2(ballSpeed * Mathf.Cos(zRotation * Mathf.Deg2Rad), ballSpeed * Mathf.Sin(zRotation * Mathf.Deg2Rad));
            ball.GetComponent<BallStart>().startVelocity = startVelocity;
            PredictPath(startVelocity);
        }
    }

    void PredictPath(Vector2 startVelocity) {
        Vector2 currentPosition = launchPos.position;
        Vector2 direction = startVelocity.normalized;
        float distance = 1f; // ���� ���� ����, �ʿ信 ���� ����
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, distance);
        
        lineRenderer.positionCount = 2; // �������� ���� �Ǵ� �浹 ����
        lineRenderer.SetPosition(0, currentPosition);
        
        if (hit.collider != null) {
            // �浹 ������ ������, �� �������� ���� �׸���.
            lineRenderer.SetPosition(1, hit.point);
        } else {
            // �浹 ������ ������, ������ �Ÿ���ŭ ���� �׸���.
            lineRenderer.SetPosition(1, currentPosition + direction * distance);
        }
    }
}
