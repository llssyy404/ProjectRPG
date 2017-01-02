using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // wayPoint값 받아와서 4개 위치 설정
    // wayPoint값으로 Enemy 위치, Patrol위치 설정
    // Enemy 인스턴싱
    // Use this for initialization
    void Awake()
    {
        GameObject playSceneUIPrefab = Resources.Load("Prefabs/PlaySceneUI") as GameObject;
        GameObject playScene = GameObject.Instantiate(playSceneUIPrefab) as GameObject;

        GameObject Map01Prefab = Resources.Load("Prefabs/Map01") as GameObject;
        GameObject Map01 = GameObject.Instantiate(Map01Prefab) as GameObject;
        Debug.Log("UI and Map Loading Complete");
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
