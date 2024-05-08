using System.Collections.Generic;
using UnityEngine;

public class ThreeCushionGameManager : MonoBehaviour {
    public GameObject[] walls; // 벽 오브젝트 배열
    public static List<GameObject> selectedWalls = new List<GameObject>(); // 수정: 클래스 레벨의 static 리스트 사용

    void Start() {
        Time.timeScale = 1;
        SelectRandomWalls();
    }

    // 선택된 벽을 초기화하고 새로운 벽을 선택하는 함수
    public void ResetAndSelectNewWalls() {
        // 선택된 벽 초기화
        foreach (GameObject wall in selectedWalls) {
            // 벽의 색상을 기본 색상(원래 색상)으로 되돌립니다.
            Renderer wallRenderer = wall.GetComponent<Renderer>();
            if (wallRenderer != null) {
                wallRenderer.material.color = Color.white; // 원래 색상으로 변경
            }
        }
        selectedWalls.Clear(); // 선택된 벽 리스트를 비웁니다.

        // 새로운 랜덤 벽을 선택합니다.
        SelectRandomWalls();
    }

    void SelectRandomWalls() {
        walls = GameObject.FindGameObjectsWithTag("Wall");
        selectedWalls.Clear(); // 수정: 기존에 선택된 벽을 클리어

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
                wallRenderer.material.color = Color.blue; // 수정: 주석과 일치하도록 색상을 빨간색에서 파란색으로 변경
            }
        }
    }
}
