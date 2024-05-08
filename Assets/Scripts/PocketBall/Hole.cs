using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("ColorBall")) {
            // PocketBallGameManager �ν��Ͻ��� ����
            PocketBallGameManager gameManager = FindObjectOfType<PocketBallGameManager>();

            // BallDestroyed �޼��� ȣ���ϱ� ���� gameManager�� null�� �ƴ��� Ȯ��
            if (gameManager != null) {
                gameManager.BallDestroyed(collision.gameObject);
            }

            // ���� �ı�
            Destroy(collision.gameObject);
        }
    }
}
