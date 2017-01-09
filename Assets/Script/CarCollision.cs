using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour {

	public float PatrolSpeed = 10.0f;

	//타이머
	public float f_time;



	void Start(){
		//PlayerTrans = GameManager.GetInstance().GetPlayer().transform;



	}
	// Update is called once per frame
	void Update () {
		f_time += Time.deltaTime;
	}
//	void OnTriggerExit(Collider col){
//		if (col.tag == "Car") {
//			Debug.Log ("asd");
//
//			//랜덤돌려서 방향을 정함.
//			//방향 전환하고 나서 상태를 패트롤로 돌려줌
//			PatrolSpeed = Random.Range (10, 30);
//			gameObject.transform.Rotate (0, 180, 0);
//		}
//	}
	void OnTriggerEnter(Collider col){
		//일단 야매코드;;
		if (f_time >= 3) {
			//정석
			if (col.tag == "Car") {
				Debug.Log ("asd");

                //랜덤돌려서 방향을 정함.
                //방향 전환하고 나서 상태를 패트롤로 돌려줌
                //PatrolSpeed = Random.Range (10, 30);
                //gameObject.transform.Rotate (0, 180, 0);
                StartCoroutine(RotationCar());
			}
		}

	}

    IEnumerator RotationCar()
    {
        float time = 0;

        while(time < 1)
        {
            time += Time.deltaTime;            
            gameObject.transform.Rotate(0, 90 * Time.deltaTime, 0);
            yield return null;
        }
        
       Debug.Log("CompleteRotate");

    }
}
