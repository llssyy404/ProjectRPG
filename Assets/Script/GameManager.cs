using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{

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
    
    public PlaySceneUI playsceneUI { get; private set; }

    private List<Vector3> _wayPoint;

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);

        //map load
        SceneManager.LoadScene("Map01", LoadSceneMode.Additive);
        
        // UI load
        GameObject playSceneUIPrefab = Resources.Load("Prefabs/UI/PlaySceneUI") as GameObject;
        GameObject playScene = GameObject.Instantiate(playSceneUIPrefab) as GameObject;
        playsceneUI = playScene.GetComponent<PlaySceneUI>();
     
    }
  

	void Start () 
    {
        //player load
        if (null != Player)
        {
            GameObject PlayerObject = GameObject.Instantiate(Player) as GameObject;
            PlayerObject.transform.position = new Vector3(0, 0, 0);
            PlayerObject.AddComponent<ObjectUI>();
            _player = PlayerObject.GetComponent<Player>();
        }

        //enemy load
        if (null != Enemy)
        {
            _enemyStateList = new List<EnumyState>();
            _wayPoint = new List<Vector3>();

            _wayPoint.Add(new Vector3(-127.0f, 1.6f, -132.0f));
            _wayPoint.Add(new Vector3(-127.0f, 1.6f, -227.0f));
            _wayPoint.Add(new Vector3(-127.0f, 1.6f, -418.0f));
            _wayPoint.Add(new Vector3(-288.0f, 1.6f, -420.0f));
            _wayPoint.Add(new Vector3(-452.0f, 1.6f, -418.0f));
            _wayPoint.Add(new Vector3(-452.0f, 1.6f, -277.0f));
            _wayPoint.Add(new Vector3(-452.0f, 1.6f, -132.0f));
            _wayPoint.Add(new Vector3(-614.0f, 1.6f, -132.0f));

            for (int i = 0; i < _wayPoint.Count; ++i)
            {
                GameObject EnemyObject = GameObject.Instantiate(Enemy) as GameObject;
                EnemyObject.transform.position = _wayPoint[i];
                EnemyObject.gameObject.name = "Enemy_" + i;
                EnemyObject.AddComponent<ObjectUI>();
                var enumyState = EnemyObject.GetComponent<EnumyState>();
                _enemyStateList.Add(enumyState);
            }
        }


        SoundManager.GetInstance().PlayLoopBgm("Bgm");
        SoundManager.GetInstance().PlayOneshotClip("sword");
        ResourceManager.GetInstance().MakeParticle(new Vector3(0, 0, 0), "Effect_02", 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void GetJoystickVector(Vector3 pos)
    {

    }

    public void SetJoystickVector(Vector3 pos)
    {

    }
    

}
