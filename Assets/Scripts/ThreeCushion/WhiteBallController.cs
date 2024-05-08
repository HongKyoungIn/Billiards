using System.Collections.Generic;
using UnityEngine;

public class WhiteBallController : MonoBehaviour {
    public float moveSpeed = 10f;
    public float rotateSpeed = 100f;
    private float rotation = 0f;
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;

    private HashSet<GameObject> hitWalls = new HashSet<GameObject>(); // 흰색 공이 부딪힌 벽을 추적하는 해시셋
    private HashSet<GameObject> hitBalls = new HashSet<GameObject>(); // 흰색 공이 부딪힌 공을 추적하는 해시셋

    public GameObject redBall; // 빨간 공 오브젝트
    public GameObject yellowBall; // 노란 공 오브젝트

    private ThreeCushionGameManager gameManager;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        // LineRenderer 설정
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.05f; // 2D에서는 선이 너무 두껍지 않게 조절
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        // ThreeCushionGameManager의 인스턴스를 찾아서 참조합니다.
        gameManager = FindObjectOfType<ThreeCushionGameManager>();
    }

    void Update() {
        // 위 아래 방향키로 회전 각도 조정
        rotation += Input.GetAxis("Vertical") * rotateSpeed * Time.deltaTime;

        // LineRenderer를 이용하여 방향 표시
        lineRenderer.SetPosition(0, transform.position);
        Vector3 endPosition = transform.position + Quaternion.Euler(0, 0, rotation) * Vector3.right * 5;
        lineRenderer.SetPosition(1, endPosition);

        // 모든 공이 정지 상태인지 확인
        if (AreAllBallsStopped()) {
            hitWalls.Clear();
            hitBalls.Clear();
            // 스페이스바를 누를 때 방향으로 공을 발사
            if (Input.GetKeyDown(KeyCode.Space)) {
                rb.velocity = Quaternion.Euler(0, 0, rotation) * Vector2.right * moveSpeed;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        GameObject collidedObject = collision.gameObject;

        // 흰색 공이 빨간 공과 노란 공에 부딪혔는지 확인
        if (collidedObject == redBall || collidedObject == yellowBall) {
            // 부딪힌 공을 해시셋에 추가
            hitBalls.Add(collidedObject);
        }

        // 모든 공에 부딪혔는지 확인
        if (HasHitAllBalls()) {
            Debug.Log("흰색 공이 빨간 공과 노란 공에 모두 부딪혔습니다!");
            if(hitWalls.Count == ThreeCushionGameManager.selectedWalls.Count) {
                gameManager.ResetAndSelectNewWalls();
                Debug.Log("점수 획득");
            }
            else {
                hitWalls.Clear();
                hitBalls.Clear();
                Debug.Log("점수 미획득");
            }
        }

        // 충돌한 오브젝트가 "Wall" 태그를 가진 경우
        if (collision.gameObject.CompareTag("Wall")) {
            GameObject hitWall = collision.gameObject;

            // 선택된 벽 중에 충돌한 벽이 있다면 해시셋에 추가
            if (ThreeCushionGameManager.selectedWalls.Contains(hitWall)) {
                hitWalls.Add(hitWall);
            }

            // 모든 선택된 벽에 흰색 공이 부딪혔는지 확인
            if (hitWalls.Count == ThreeCushionGameManager.selectedWalls.Count) {
                Debug.Log("흰색 공이 모든 선택된 벽에 부딪혔습니다!");
                // 필요한 경우 게임 승리 처리 추가
            }
        }
    }

    // 흰색 공이 빨간 공과 노란 공에 모두 부딪혔는지 확인하는 함수
    public bool HasHitAllBalls() {
        // 해시셋에 빨간 공과 노란 공이 모두 포함되어 있는지 확인
        return hitBalls.Contains(redBall) && hitBalls.Contains(yellowBall);
    }

    // 모든 공이 정지 상태인지 확인하는 함수
    bool AreAllBallsStopped() {
        // 모든 공의 Rigidbody2D 컴포넌트를 가져옵니다.
        Rigidbody2D redBallRb = redBall.GetComponent<Rigidbody2D>();
        Rigidbody2D yellowBallRb = yellowBall.GetComponent<Rigidbody2D>();

        // 모든 공의 속도를 확인하고, 모두 정지 상태(속도가 거의 0에 가깝다면)인지 확인합니다.
        bool whiteBallStopped = rb.velocity.sqrMagnitude < 0.01f;
        bool redBallStopped = redBallRb.velocity.sqrMagnitude < 0.01f;
        bool yellowBallStopped = yellowBallRb.velocity.sqrMagnitude < 0.01f;

        // 모든 공이 정지 상태라면 true를 반환합니다.
        return whiteBallStopped && redBallStopped && yellowBallStopped;
    }
}
