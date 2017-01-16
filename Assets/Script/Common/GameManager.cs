using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{

    // wayPoint값 받아와서 4개 위치 설정
    // wayPoint값으로 Enemy 위치, Patrol위치 설정
    // Enemy 인스턴싱
    public enum state { Title, Play };
    public state GameState { get; private set; }

    private CameraMgr _cameraMgr;

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

    private static bool _initailize = false;
    public static bool Initialized()
    {
        if (false == _initailize)
        {
            SceneManager.LoadScene("TestPlayScene");
            return false;
        }
        else
            return true;
    }


    public PlaySceneUI playsceneUI { get; private set; }

    private List<Vector3> _wayPoint;

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);        
        GameState = state.Title;
        _initailize = true;

        LoadMap();
    }
  
	void Start () 
    {
        
        InitUI();
        InitPlayer();
        InitEnemy();
        InitCamera();

        SoundManager.GetInstance().PlayLoopBgm("Bgm");
        SoundManager.GetInstance().PlayOneshotClip("sword");
        ResourceManager.GetInstance().MakeParticle(new Vector3(0, 0, 0), "Effect_02", 2.0f);
	}
    
    private void LoadMap()
    {
        //map load
        SceneManager.LoadScene("Map01", LoadSceneMode.Additive);
    }

    private void InitUI()
    {
        // UI load
        GameObject playSceneUIPrefab = Resources.Load("Prefabs/UI/PlaySceneUI") as GameObject;
        GameObject playScene = GameObject.Instantiate(playSceneUIPrefab) as GameObject;
        playsceneUI = playScene.GetComponent<PlaySceneUI>();
    }

    private void InitPlayer()
    {
        //player load
        if (null != Player)
        {
            GameObject PlayerObject = GameObject.Instantiate(Player) as GameObject;
            PlayerObject.transform.position = new Vector3(0, 1.75f, -20.0f);
            ObjectUI objUI = PlayerObject.AddComponent<ObjectUI>();
            objUI.Init(100);
            _player = PlayerObject.GetComponent<Player>();
            _player.SetHpBar(objUI);
        }
    }

    private void InitCamera()
    {
        // cameraMgr Connect
        _cameraMgr = Camera.main.GetComponent<CameraMgr>();
    }

    private void InitEnemy()
    {
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
                ObjectUI objUI = EnemyObject.AddComponent<ObjectUI>();
                var enumyState = EnemyObject.GetComponent<EnumyState>();
                enumyState.SetHpBar(objUI);
                _enemyStateList.Add(enumyState);
            }
        }
    }

    public class JoystickInfo
    {
        public JoystickInfo()
        {
            _playerJoystickIsEnd = true;
            _joystickVector = Vector3.zero;
        }

        public bool _playerJoystickIsEnd;
        public Vector3 _joystickVector;
    }

    private JoystickInfo _joystickInfo = new JoystickInfo();

    public JoystickInfo GetJoystickVector()
    {
        return _joystickInfo;
    }

    public void SetJoystickVector(bool isEnd, Vector3 pos)
    {
        _joystickInfo._playerJoystickIsEnd = isEnd;
        _joystickInfo._joystickVector = pos;
    }
    
    public void SetState(state state)
    {
        GameState = state;

        if (state == state.Play)
        {
            _cameraMgr.SetCameraState(CameraMgr.state.Play);
        }        
    }
    

    
}
