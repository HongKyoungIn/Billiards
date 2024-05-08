using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour {
    private float zRotation = 0f;
    public float rotationSpeed = 50f;
    public GameObject whiteBall;
    public Transform launchPos;
    public float ballSpeed = 10f;

    // whiteBall�� ������ �����ϱ� ���� ���� ����
    private int whiteBallCount = 0;

    void Update() {
        zRotation += Input.GetAxisRaw("Vertical") * Time.deltaTime * rotationSpeed;
        zRotation = Mathf.Clamp(zRotation, -60f, 60f); // Mathf.Clamp ������� ����ȭ
        transform.eulerAngles = new Vector3(0, 0, zRotation);

        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject ball = Instantiate(whiteBall, launchPos.position, Quaternion.Euler(Vector3.zero));
            Vector2 startVelocity = new Vector2(ballSpeed * Mathf.Cos(zRotation * Mathf.Deg2Rad), ballSpeed * Mathf.Sin(zRotation * Mathf.Deg2Rad));
            ball.GetComponent<BallStart>().startVelocity = startVelocity;

            // whiteBallCount�� ������Ŵ
            whiteBallCount++;
            Debug.Log(whiteBallCount);
        }
    }

    public int GetBallCount() {
        return whiteBallCount;
    }
}