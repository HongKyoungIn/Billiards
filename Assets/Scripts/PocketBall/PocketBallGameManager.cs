using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PocketBallGameManager : MonoBehaviour {
    public int whiteBallNum = 0; // 하얀 공을 발사한 횟수
    public bool blackBall = true; // 검은 공이 존재하는지 여부
    public List<GameObject> colorBalls = new List<GameObject>();
    public Launch launch;

    // 게임 오버 조건 체크 메서드
    void CheckGameOver() {
        // 모든 ColorBall이 제거되었는지 체크
        if (colorBalls.Count == 0) {
            // 모든 ColorBall이 제거되었으면 게임 오버 처리
            GameOver(true); // 여기서 true는 게임에서 승리했음을 의미할 수 있습니다.
        }
    }

    // 게임 오버 처리 메서드
    void GameOver(bool win) {
        // 게임 멈춤
        Time.timeScale = 0;

        if (win) {
            // 승리 메시지 표시
            Debug.Log("축하합니다! 모든 컬러 볼을 제거했습니다!");
        }
        else {
            // 패배 메시지 표시
            Debug.Log("게임 오버! 다시 도전하세요.");
        }

        // 하얀 공을 발사한 횟수 출력
        Debug.Log("하얀 공을 발사한 횟수: " + launch.GetBallCount());

        // 스페이스바가 눌렸는지 체크
        if (Input.GetKeyDown(KeyCode.Space)) {
            // "3cushion" 씬으로 전환
            SceneManager.LoadScene("ThreeCushion");
        }
    }



    public void BallDestroyed(GameObject ball) {
        if (colorBalls.Contains(ball)) {
            colorBalls.Remove(ball); // 리스트에서 공 오브젝트 제거
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
        // 매 프레임마다 게임 오버 조건을 체크
        CheckGameOver();
    }
}
