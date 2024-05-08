using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("ColorBall")) {
            // PocketBallGameManager 인스턴스에 접근
            PocketBallGameManager gameManager = FindObjectOfType<PocketBallGameManager>();

            // BallDestroyed 메서드 호출하기 전에 gameManager가 null이 아닌지 확인
            if (gameManager != null) {
                gameManager.BallDestroyed(collision.gameObject);
            }

            // 공을 파괴
            Destroy(collision.gameObject);
        }
    }
}
