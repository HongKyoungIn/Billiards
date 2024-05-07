using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[] walls; // 벽 오브젝트 배열

    // Start is called before the first frame update
    void Start() {
        SelectRandomWalls();
    }

    void SelectRandomWalls() {
        // 모든 "Wall" 태그가 붙은 오브젝트를 찾습니다.
        walls = GameObject.FindGameObjectsWithTag("Wall");

        // 선택된 벽을 저장할 리스트
        List<GameObject> selectedWalls = new List<GameObject>();

        // 임의의 3개 벽을 선택합니다.
        while (selectedWalls.Count < 3) {
            GameObject randomWall = walls[Random.Range(0, walls.Length)];
            if (!selectedWalls.Contains(randomWall)) {
                selectedWalls.Add(randomWall);
            }
        }

        // 선택된 벽 오브젝트에 대한 처리
        foreach (GameObject wall in selectedWalls) {
            Debug.Log("Selected Wall: " + wall.name);
        }
    }
}
