using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour {
    private float zRotation = 0f;
    public float rotationSpeed = 50f;
    public GameObject whiteBall;
    public Transform launchPos;
    public float ballSpeed = 10f;
    public LineRenderer lineRenderer; // LineRenderer 참조

    void Update() {
        zRotation += Input.GetAxisRaw("Vertical") * Time.deltaTime * rotationSpeed;
        zRotation = Mathf.Clamp(zRotation, -60f, 60f); // Mathf.Clamp 사용으로 간략화
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
        float distance = 1f; // 예측 선의 길이, 필요에 따라 조정
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, distance);
        
        lineRenderer.positionCount = 2; // 시작점과 끝점 또는 충돌 지점
        lineRenderer.SetPosition(0, currentPosition);
        
        if (hit.collider != null) {
            // 충돌 지점이 있으면, 그 지점까지 선을 그린다.
            lineRenderer.SetPosition(1, hit.point);
        } else {
            // 충돌 지점이 없으면, 예측된 거리만큼 선을 그린다.
            lineRenderer.SetPosition(1, currentPosition + direction * distance);
        }
    }
}
