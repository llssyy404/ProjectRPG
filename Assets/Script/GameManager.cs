﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // wayPoint값 받아와서 4개 위치 설정
    // wayPoint값으로 Enemy 위치, Patrol위치 설정
    // Enemy 인스턴싱


    public GameObject Player;
    public GameObject Enemy;

    private Player _player;
    public Player GetPlayer()
    {
        return _player;
    }

    private List<EnumyState> _enemyStateList;

    private static GameManager _instance;
    public static GameManager GetInstance()
    {          
        return _instance;
    }

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);

        //map load
        SceneManager.LoadScene("Map01", LoadSceneMode.Additive);

        // UI load
        GameObject playSceneUIPrefab = Resources.Load("Prefabs/UI/PlaySceneUI") as GameObject;
        GameObject playScene = GameObject.Instantiate(playSceneUIPrefab) as GameObject;

        //player load
        if (null != Player)
        {
            GameObject PlayerObject = GameObject.Instantiate(Player) as GameObject;
            PlayerObject.transform.position = new Vector3(0, 0, 0);
            _player = PlayerObject.GetComponent<Player>();            
        }

        //if(null != Enemy)
        //{
        //    _enemyStateList = new List<EnumyState>();

        //    for (int i=0;i<8;++i)
        //    {
        //        GameObject EnemyObject = GameObject.Instantiate(Enemy) as GameObject;
        //        EnemyObject.transform.position = new Vector3(i * 20, 0 , i*20);
        //        EnemyObject.gameObject.name = "Enemy_" + i;
        //        var enumyState = EnemyObject.GetComponent<EnumyState>();
        //        _enemyStateList.Add(enumyState);
        //    }
        //}

        SoundManager.GetInstance().PlayLoopBgm("Bgm");
        SoundManager.GetInstance().PlayOneshotClip("sword");
        ResourceManager.GetInstance().MakeParticle(new Vector3(0, 0, 0), "Effect_02",2.0f);
    }
  

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
