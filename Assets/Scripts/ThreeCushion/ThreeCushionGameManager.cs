using System.Collections.Generic;
using UnityEngine;

public class ThreeCushionGameManager : MonoBehaviour {
    public GameObject[] walls; // �� ������Ʈ �迭
    public static List<GameObject> selectedWalls = new List<GameObject>(); // ����: Ŭ���� ������ static ����Ʈ ���

    void Start() {
        Time.timeScale = 1;
        SelectRandomWalls();
    }

    // ���õ� ���� �ʱ�ȭ�ϰ� ���ο� ���� �����ϴ� �Լ�
    public void ResetAndSelectNewWalls() {
        // ���õ� �� �ʱ�ȭ
        foreach (GameObject wall in selectedWalls) {
            // ���� ������ �⺻ ����(���� ����)���� �ǵ����ϴ�.
            Renderer wallRenderer = wall.GetComponent<Renderer>();
            if (wallRenderer != null) {
                wallRenderer.material.color = Color.white; // ���� �������� ����
            }
        }
        selectedWalls.Clear(); // ���õ� �� ����Ʈ�� ���ϴ�.

        // ���ο� ���� ���� �����մϴ�.
        SelectRandomWalls();
    }

    void SelectRandomWalls() {
        walls = GameObject.FindGameObjectsWithTag("Wall");
        selectedWalls.Clear(); // ����: ������ ���õ� ���� Ŭ����

        while (selectedWalls.Count < 3) {
            GameObject randomWall = walls[Random.Range(0, walls.Length)];
            if (!selectedWalls.Contains(randomWall)) {
                selectedWalls.Add(randomWall);
            }
        }

        foreach (GameObject wall in selectedWalls) {
            //Debug.Log("Selected Wall: " + wall.name);
            Renderer wallRenderer = wall.GetComponent<Renderer>();
            if (wallRenderer != null) {
                wallRenderer.material.color = Color.blue; // ����: �ּ��� ��ġ�ϵ��� ������ ���������� �Ķ������� ����
            }
        }
    }
}
