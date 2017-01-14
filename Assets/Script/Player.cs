using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDEL,
    RUN,
    ATTACK1,
    ATTACK2,
    ATTACK3,
    ATTACK4,
    DIE
}

public class Player : MonoBehaviour
{
    //싱글톤
    private static Player _instance;
    public static Player GetInstance()
    {
        return _instance;
    }
    public Transform player;

    //캐릭터 이동속도
    float moveSpeed = 15f;

    //애니메이션
    public Animator playerAnim;

    //캐릭터 상태
    public PlayerState PS;

    //플레이어 사망여부
    private bool isDie = false;

    // 플레이어 체력
    private int hp = 100;

    private GameManager gameMgr;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        gameMgr = GameManager.GetInstance();
        PS = PlayerState.IDEL;
        StartCoroutine(this.CheckPlayerState());
    }

    void Update()
    {
        PlayMove();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PS = PlayerState.ATTACK1;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PS = PlayerState.ATTACK2;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PS = PlayerState.ATTACK3;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            PS = PlayerState.ATTACK4;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            PS = PlayerState.DIE;
        }

    }

    IEnumerator CheckPlayerState()
    {
        while (!isDie)
        {
            switch (PS)
            {
                case PlayerState.IDEL:
                    playerAnim.SetBool("IsRun", false);
                    playerAnim.SetBool("IsAttack", false);
                    playerAnim.SetBool("IsAttack2", false);
                    playerAnim.SetBool("IsAttack3", false);
                    playerAnim.SetBool("IsAttack4", false);
                    break;
                case PlayerState.RUN:
                    playerAnim.SetBool("IsRun", true);
                    playerAnim.SetBool("IsAttack", false);
                    playerAnim.SetBool("IsAttack2", false);
                    playerAnim.SetBool("IsAttack3", false);
                    playerAnim.SetBool("IsAttack4", false);
               
                   
                    break;
                case PlayerState.ATTACK1:
                    playerAnim.SetBool("IsAttack", true);
                    playerAnim.SetBool("IsAttack2", false);
                    playerAnim.SetBool("IsAttack3", false);
                    playerAnim.SetBool("IsAttack4", false);
                    break;
                case PlayerState.ATTACK2:
                    playerAnim.SetBool("IsAttack2", true);
                    playerAnim.SetBool("IsAttack", false);
                    playerAnim.SetBool("IsAttack3", false);
                    playerAnim.SetBool("IsAttack4", false);
                    break;
                case PlayerState.ATTACK3:
                    playerAnim.SetBool("IsAttack3", true);
                    playerAnim.SetBool("IsAttack", false);
                    playerAnim.SetBool("IsAttack2", false);
                    playerAnim.SetBool("IsAttack4", false);
                    break;
                case PlayerState.ATTACK4:
                    playerAnim.SetBool("IsAttack4", true);
                    playerAnim.SetBool("IsAttack", false);
                    playerAnim.SetBool("IsAttack2", false);
                    playerAnim.SetBool("IsAttack3", false);
                    break;
                case PlayerState.DIE:
                    playerAnim.SetTrigger("IsDie");
                    break;
            }
            yield return null;
        }
    }
    //조이스틱으로 캐릭터 움직이기
    public void PlayMove()
    {
        bool isEnd;
        Vector3 joyStickVector;
        joyStickVector = gameMgr.GetJoystickVector()._joystickVector;
        isEnd = gameMgr.GetJoystickVector()._playerJoystickIsEnd;
        /*
        player.eulerAngles = new Vector3(player.eulerAngles.x,
            Mathf.Atan2(JoyStick.instance.axis.x, JoyStick.instance.axis.y) * Mathf.Rad2Deg, player.eulerAngles.z);

        player.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
         */
        //bool isEnd = gameMgr.GetJoystickVector
        

        if(!isEnd)
        {
            PS = PlayerState.RUN;
            player.eulerAngles = new Vector3(player.eulerAngles.x,
                 Mathf.Atan2(joyStickVector.x, joyStickVector.y) * Mathf.Rad2Deg, player.eulerAngles.z);

            player.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
            PS = PlayerState.IDEL;

    }

    void OnCollisionEnter(Collision coll)
    {
        //충돌체크
        if (coll.gameObject.tag == "")
        {
            //맞는 애니메이션
            playerAnim.SetTrigger("IsHit");
            //카메라 쉐이킹
            Camera.main.GetComponent<CameraMgr>().ShakeCamera(0.5f);
        }
    }

    public void DamageHp(int damage)
    {
        hp -= damage;
        _objectUI.DamageHp(damage);
        Debug.Log(hp);

        if(0 >= hp)
            PS = PlayerState.DIE;
    }


    private ObjectUI _objectUI;
    public void SetHpBar(ObjectUI objectUI)
    {
        _objectUI = objectUI;
    }
}
