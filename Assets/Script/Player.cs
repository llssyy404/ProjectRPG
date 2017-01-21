using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstNameSpace;
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

    // 오브젝트 정보
    public ObjectInfo Info { private set; get; }
    
    private GameManager gameMgr;

    private bool _isMove = false;

    float coolTime = 5f; 
    float addSkillTime = 0f;
  
    bool isOnSkill = false;

    //
    bool isSkill1 = true;
    bool isSkill2 = true;
    bool isSkill3 = true;
    bool iSAttak = true;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        gameMgr = GameManager.GetInstance();
        Info = InfoManager.GetInstance().Player;

        PS = PlayerState.IDEL;
        StartCoroutine(this.CheckPlayerState());
        StartCoroutine("WalkEffect");
        StartCoroutine(this.Skill1CoolTime());
        StartCoroutine(this.Skill2CoolTime());
        StartCoroutine(this.Skill3CoolTime());

    }

    void Update()
    {
        PlayMove();

        
        if(isOnSkill)
        {
            addSkillTime += Time.deltaTime;
            if (addSkillTime >= 1f)
            {
                OnIdel();
                addSkillTime = 0f;
                isOnSkill = false;
            }
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
            Mathf.Atan2(joyStickVector.x, joyStickVector.y) * Mathf.Rad2Deg, player.eulerAngles.z);

        player.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
         */
        //bool isEnd = gameMgr.GetJoystickVector

        
        if (!isEnd)
        {
            //PS = PlayerState.RUN;
            player.eulerAngles = new Vector3(player.eulerAngles.x,
                Mathf.Atan2(joyStickVector.x, joyStickVector.y) * Mathf.Rad2Deg, player.eulerAngles.z);

            player.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
           // _isMove = true;

        }
        else
        {
            //PS = PlayerState.IDEL;
            _isMove = false;
        }
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
        

        if(0 >= hp)
            PS = PlayerState.DIE;
    }


    private ObjectUI _objectUI;
    public void SetHpBar(ObjectUI objectUI)
    {
        _objectUI = objectUI;
    }
    
    IEnumerator WalkEffect()
    {
        while (true)
        {
            if (_isMove == false)
                yield return null;
            else
            {               
                ResourceManager.GetInstance().MakeParticle(this.gameObject.transform.position, "Smoke/CFXM_SmokePuffs", 2.0f);
                SoundManager.GetInstance().PlayOneshotClip("Footsteps/Footstep_Gravel_3");
                yield return new WaitForSeconds(0.5f);
            }
            
        }

    }
        
    public void OnRun()
    {
       // bool isEnd;

       // Vector3 joyStickVector;
       // joyStickVector = gameMgr.GetJoystickVector()._joystickVector;
        //isEnd = gameMgr.GetJoystickVector()._playerJoystickIsEnd;

        PS = PlayerState.RUN;
       // player.eulerAngles = new Vector3(player.eulerAngles.x,
       //      Mathf.Atan2(joyStickVector.x, joyStickVector.y) * Mathf.Rad2Deg, player.eulerAngles.z);

        //player.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        _isMove = true;
    }
    public void OnIdel()
    {
        PS = PlayerState.IDEL;
        _isMove = false;
    }
    public void OnAttackSkill()
    {
        isOnSkill = true;
        PS = PlayerState.ATTACK1;
    }
    public void OnSkill(int index)
    {
        isOnSkill = true;
    
            switch (index)
            {
                case 0:
                    if(isSkill1)
                    {
                        isSkill1 = false;
                        PS = PlayerState.ATTACK2;
                       
                    }
                        break;
                case 1:
                    if (isSkill2)
                    {
                        isSkill2 = false;
                        PS = PlayerState.ATTACK3;
                      
                    }
                    break;
                case 2:
                    if (isSkill3)
                    {
                        isSkill3 = false;
                        PS = PlayerState.ATTACK4;
                        
                    }
                        break;
                default:

                        break;
            }
        
    }


    //쿨타임 스킬 코루틴
    IEnumerator Skill1CoolTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(coolTime);
            
            if (isSkill1 == true)
                yield return null;
            else
                isSkill1 = true;
      
        }
    }

    //2
    IEnumerator Skill2CoolTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(coolTime);

            if (isSkill2 == true)
                yield return null;
            else
                isSkill2 = true;
            
        }
    }

    //3
    IEnumerator Skill3CoolTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(coolTime);

            if (isSkill3 == true)
                yield return null;
            else
                isSkill3 = true;
            
        }
    }


}
