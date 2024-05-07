using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[] walls; // �� ������Ʈ �迭

    // Start is called before the first frame update
    void Start() {
        SelectRandomWalls();
    }

    void SelectRandomWalls() {
        // ��� "Wall" �±װ� ���� ������Ʈ�� ã���ϴ�.
        walls = GameObject.FindGameObjectsWithTag("Wall");

        // ���õ� ���� ������ ����Ʈ
        List<GameObject> selectedWalls = new List<GameObject>();

        // ������ 3�� ���� �����մϴ�.
        while (selectedWalls.Count < 3) {
            GameObject randomWall = walls[Random.Range(0, walls.Length)];
            if (!selectedWalls.Contains(randomWall)) {
                selectedWalls.Add(randomWall);
            }
        }

        // ���õ� �� ������Ʈ�� ���� ó��
        foreach (GameObject wall in selectedWalls) {
            Debug.Log("Selected Wall: " + wall.name);
        }
    }
}
