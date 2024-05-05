using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour {
    // Start is called before the first frame update

    private float zRotation = 0f;
    public float rotationSpeed = 50f;
    public GameObject whiteBall;
    public Transform launchPos;
    public float ballSpeed = 10f;

    // Update is called once per frame
    void Update() {
        zRotation += Input.GetAxisRaw("Vertical") * Time.deltaTime * rotationSpeed;
        if(zRotation > 60f) {
            zRotation = 60f;
        }
        if(zRotation < -60f) {
            zRotation = -60f;
        }
        transform.eulerAngles = new Vector3(0, 0, zRotation);

        if(Input.GetKeyDown(KeyCode.Space)) {
            GameObject ball = Instantiate(whiteBall, launchPos.position, Quaternion.Euler(Vector3.zero));
            ball.GetComponent<BallStart>().startVelocity = new Vector2(ballSpeed * Mathf.Cos(zRotation * Mathf.Deg2Rad), Mathf.Sin(zRotation * Mathf.Deg2Rad));

        }
    }
}
