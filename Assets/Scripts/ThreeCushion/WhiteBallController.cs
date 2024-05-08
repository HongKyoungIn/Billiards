using System.Collections.Generic;
using UnityEngine;

public class WhiteBallController : MonoBehaviour {
    public float moveSpeed = 10f;
    public float rotateSpeed = 100f;
    private float rotation = 0f;
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;

    private HashSet<GameObject> hitWalls = new HashSet<GameObject>(); // ��� ���� �ε��� ���� �����ϴ� �ؽü�
    private HashSet<GameObject> hitBalls = new HashSet<GameObject>(); // ��� ���� �ε��� ���� �����ϴ� �ؽü�

    public GameObject redBall; // ���� �� ������Ʈ
    public GameObject yellowBall; // ��� �� ������Ʈ

    private ThreeCushionGameManager gameManager;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        // LineRenderer ����
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.05f; // 2D������ ���� �ʹ� �β��� �ʰ� ����
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        // ThreeCushionGameManager�� �ν��Ͻ��� ã�Ƽ� �����մϴ�.
        gameManager = FindObjectOfType<ThreeCushionGameManager>();
    }

    void Update() {
        // �� �Ʒ� ����Ű�� ȸ�� ���� ����
        rotation += Input.GetAxis("Vertical") * rotateSpeed * Time.deltaTime;

        // LineRenderer�� �̿��Ͽ� ���� ǥ��
        lineRenderer.SetPosition(0, transform.position);
        Vector3 endPosition = transform.position + Quaternion.Euler(0, 0, rotation) * Vector3.right * 5;
        lineRenderer.SetPosition(1, endPosition);

        // ��� ���� ���� �������� Ȯ��
        if (AreAllBallsStopped()) {
            hitWalls.Clear();
            hitBalls.Clear();
            // �����̽��ٸ� ���� �� �������� ���� �߻�
            if (Input.GetKeyDown(KeyCode.Space)) {
                rb.velocity = Quaternion.Euler(0, 0, rotation) * Vector2.right * moveSpeed;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        GameObject collidedObject = collision.gameObject;

        // ��� ���� ���� ���� ��� ���� �ε������� Ȯ��
        if (collidedObject == redBall || collidedObject == yellowBall) {
            // �ε��� ���� �ؽü¿� �߰�
            hitBalls.Add(collidedObject);
        }

        // ��� ���� �ε������� Ȯ��
        if (HasHitAllBalls()) {
            Debug.Log("��� ���� ���� ���� ��� ���� ��� �ε������ϴ�!");
            if(hitWalls.Count == ThreeCushionGameManager.selectedWalls.Count) {
                gameManager.ResetAndSelectNewWalls();
                Debug.Log("���� ȹ��");
            }
            else {
                hitWalls.Clear();
                hitBalls.Clear();
                Debug.Log("���� ��ȹ��");
            }
        }

        // �浹�� ������Ʈ�� "Wall" �±׸� ���� ���
        if (collision.gameObject.CompareTag("Wall")) {
            GameObject hitWall = collision.gameObject;

            // ���õ� �� �߿� �浹�� ���� �ִٸ� �ؽü¿� �߰�
            if (ThreeCushionGameManager.selectedWalls.Contains(hitWall)) {
                hitWalls.Add(hitWall);
            }

            // ��� ���õ� ���� ��� ���� �ε������� Ȯ��
            if (hitWalls.Count == ThreeCushionGameManager.selectedWalls.Count) {
                Debug.Log("��� ���� ��� ���õ� ���� �ε������ϴ�!");
                // �ʿ��� ��� ���� �¸� ó�� �߰�
            }
        }
    }

    // ��� ���� ���� ���� ��� ���� ��� �ε������� Ȯ���ϴ� �Լ�
    public bool HasHitAllBalls() {
        // �ؽü¿� ���� ���� ��� ���� ��� ���ԵǾ� �ִ��� Ȯ��
        return hitBalls.Contains(redBall) && hitBalls.Contains(yellowBall);
    }

    // ��� ���� ���� �������� Ȯ���ϴ� �Լ�
    bool AreAllBallsStopped() {
        // ��� ���� Rigidbody2D ������Ʈ�� �����ɴϴ�.
        Rigidbody2D redBallRb = redBall.GetComponent<Rigidbody2D>();
        Rigidbody2D yellowBallRb = yellowBall.GetComponent<Rigidbody2D>();

        // ��� ���� �ӵ��� Ȯ���ϰ�, ��� ���� ����(�ӵ��� ���� 0�� �����ٸ�)���� Ȯ���մϴ�.
        bool whiteBallStopped = rb.velocity.sqrMagnitude < 0.01f;
        bool redBallStopped = redBallRb.velocity.sqrMagnitude < 0.01f;
        bool yellowBallStopped = yellowBallRb.velocity.sqrMagnitude < 0.01f;

        // ��� ���� ���� ���¶�� true�� ��ȯ�մϴ�.
        return whiteBallStopped && redBallStopped && yellowBallStopped;
    }
}
