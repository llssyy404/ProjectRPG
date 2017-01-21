using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarState : MonoBehaviour {

	const int MAX = 8;

	enum CarBehavState { IDLE = 0,  PATROL };
	CarBehavState eCarState = CarBehavState.IDLE;
	public float PatrolSpeed = 5.0f;

	// 웨이포인트 상태를 받아오던지 만들던지
	GameObject[] m_wayPoint;
	//테스트 시간;
	public float f_time;
	//자동차 등록
	public GameObject[] Cars;
	//자동차 위치
	public Transform[] CarPos;
	//플레이어 위치
	public Transform PlayerTrans;

	//public Transform wayPointFirst; 
	//Vector3 wayPointFirst;

	// Use this for initialization
	void Start()
	{
//		if (false == GameManager.Initialized())
//			return;
		
		f_time = 0;

		//차와 인간의 거리를 구합시다...
		//프레이아 위치
		//PlayerTrans = GameManager.GetInstance().GetPlayer().transform;
		//카 위치
		m_wayPoint = new GameObject[MAX];
		WayPointInit ();

		// Car_1a 프리팹 등록; 갯수는 나중에 수정염...
		Cars = new GameObject[MAX];
		CarPos = new Transform[MAX];

		//등록
		Cars[0] = (GameObject)Resources.Load("Prefabs/Car_3 (4)");
		Cars[1] = (GameObject)Resources.Load("Prefabs/Car_3 (6)");
		Cars[2] = (GameObject)Resources.Load("Prefabs/Car_3 (7)");
		Cars[3] = (GameObject)Resources.Load("Prefabs/Car_3 (8)");
		Cars[4] = (GameObject)Resources.Load("Prefabs/Car_3 (4)");
		Cars[5] = (GameObject)Resources.Load("Prefabs/Car_3 (6)");
		Cars[6] = (GameObject)Resources.Load("Prefabs/Car_3 (7)");
		Cars[7] = (GameObject)Resources.Load("Prefabs/Car_3 (8)");

		for (int i = 0; i < MAX; ++i) {
			Cars [i] = GameObject.Instantiate (Cars [i], CarPos [i]);
			Cars [i].GetComponent<CarCollision> ().SetDirection (i %2 ==0 ? 1:-1);
			CarPos [i] = Cars [i].transform;
			if(i %2 ==0)
				CarPos [i].transform.Rotate (0, 90*i  , 0); 
			else
				CarPos [i].transform.Rotate (0, -90*i , 0); 

			CarPos [i].transform.position = m_wayPoint [i].transform.position;
			Debug.Log(m_wayPoint [i].transform.position);
		}
		//웨이포인트 위치를 세팅합니다.
//		m_wayPoint = new Vector3[??];

		//먼저 생성되면 PATROL상태로...
		eCarState = CarBehavState.PATROL;
	}	

	// Update is called once per frame
	void Update()
	{
		SetEnumyStateByPlayerPos();         // 적 state 설정
		SetDestinationByEnumyState();       // state에 따른 목표위치 설정
	}

	void WayPointInit()
	{
		m_wayPoint[0] = GameObject.Find( "CarWayPointPos/Way1"); 
		m_wayPoint[1] = GameObject.Find( "CarWayPointPos/Way1 (1)"); 

		m_wayPoint[2] = GameObject.Find( "CarWayPointPos/Way4"); 
		m_wayPoint[3] = GameObject.Find( "CarWayPointPos/Way4 (1)"); 

		m_wayPoint[4] = GameObject.Find( "CarWayPointPos2/Way1"); 
		m_wayPoint[5] = GameObject.Find( "CarWayPointPos2/Way1 (1)"); 

		m_wayPoint[6] = GameObject.Find( "CarWayPointPos2/Way4"); 
		m_wayPoint[7] = GameObject.Find( "CarWayPointPos2/Way4 (1)"); 

	}

	float GetDistanceByTargetPosition(Vector3 TargetPosition)
	{
		// 플레이어와 차 검출
		return Vector3.Distance(transform.position, TargetPosition);
	}


	void SetEnumyStateByPlayerPos()
	{
		
	}

	void Patroll()
	{
		// 직선으로 일단 움직임.. 0번에 right를 쓴 이유는 프리팹이 90도 돌아가있어서...
		for (int i = 0; i < MAX; ++i) {
			CarPos[i].transform.Translate(Vector3.left * PatrolSpeed *  Time.deltaTime );
			Debug.Log ("asd :  "+Cars [i].GetComponent<CarCollision> ().f_dir);
		}

	}

	//--------------------------------------추가---------------------------------
	void Idle(){
//		f_time += Time.deltaTime;
//		//랜덤돌려서 방향을 정함.
// 
//		//방향 전환하고 나서 상태를 패트롤로 돌려줌
//		if (f_time >= 1  && eCarState == CarBehavState.IDLE) {
//			Debug.Log ("요시2" );
//
//			PatrolSpeed = Random.Range (10, 30);
//			CarPos[0].transform.LookAt (m_wayPoint[1].transform.position);
//
//			eCarState = CarBehavState.PATROL;
//			f_time = 0;
//
//		}
	}

			
	void SetDestinationByEnumyState()
	{
		switch (eCarState)
		{
		case CarBehavState.IDLE:
			{
				Idle ();
			}
			break;
		case CarBehavState.PATROL:
			{
				Patroll();
			}
			break;
		default: break;
		}
	}
}
