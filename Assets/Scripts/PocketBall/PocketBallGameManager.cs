using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PocketBallGameManager : MonoBehaviour {
    public int whiteBallNum = 0; // �Ͼ� ���� �߻��� Ƚ��
    public bool blackBall = true; // ���� ���� �����ϴ��� ����
    public List<GameObject> colorBalls = new List<GameObject>();
    public Launch launch;

    // ���� ���� ���� üũ �޼���
    void CheckGameOver() {
        // ��� ColorBall�� ���ŵǾ����� üũ
        if (colorBalls.Count == 0) {
            // ��� ColorBall�� ���ŵǾ����� ���� ���� ó��
            GameOver(true); // ���⼭ true�� ���ӿ��� �¸������� �ǹ��� �� �ֽ��ϴ�.
        }
    }

    // ���� ���� ó�� �޼���
    void GameOver(bool win) {
        // ���� ����
        Time.timeScale = 0;

        if (win) {
            // �¸� �޽��� ǥ��
            Debug.Log("�����մϴ�! ��� �÷� ���� �����߽��ϴ�!");
        }
        else {
            // �й� �޽��� ǥ��
            Debug.Log("���� ����! �ٽ� �����ϼ���.");
        }

        // �Ͼ� ���� �߻��� Ƚ�� ���
        Debug.Log("�Ͼ� ���� �߻��� Ƚ��: " + launch.GetBallCount());

        // �����̽��ٰ� ���ȴ��� üũ
        if (Input.GetKeyDown(KeyCode.Space)) {
            // "3cushion" ������ ��ȯ
            SceneManager.LoadScene("ThreeCushion");
        }
    }



    public void BallDestroyed(GameObject ball) {
        if (colorBalls.Contains(ball)) {
            colorBalls.Remove(ball); // ����Ʈ���� �� ������Ʈ ����
        }
    }

    // Start is called before the first frame update
    void Start() {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ColorBall");
        foreach (GameObject ball in balls) {
            colorBalls.Add(ball);
        }
    }

    // Update is called once per frame
    void Update() {
        // �� �����Ӹ��� ���� ���� ������ üũ
        CheckGameOver();
    }
}
