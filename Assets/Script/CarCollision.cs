using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour {

	public float PatrolSpeed = 20.0f;

	//타이머
	public float f_time;



	void Start(){
		//PlayerTrans = GameManager.GetInstance().GetPlayer().transform;



	}
	// Update is called once per frame
	void Update () {
		f_time += Time.deltaTime;
	}
	void OnTriggerEnter(Collider col){
		//일단 야매코드;;
		if (f_time >= 3) {
			if (col.tag == "Car") {
                StartCoroutine(RotationCar());
			} 
		}
	}
	void OnTriggerExit(Collider col){
		//일단 야매코드;;
		if (f_time >= 3) {
			if (col.tag == "Car") {
				CorrectlyCarPos ();
			}
		}
	}
	void CorrectlyCarPos(){
		//transform.eulerAngles.y
		if ((transform.eulerAngles.y <= 30.0f) && (transform.eulerAngles.y >= -30.0f)) {
			Debug.Log ("1");
			//transform.rotation = new Quaternion(0,0,0,0);
		} else if ((transform.eulerAngles.y <= 30.0f + 90.0f) && (transform.eulerAngles.y >= -30.0f + 90.0f)) {
			Debug.Log ("2");
			//transform.rotation = new Quaternion(0,0+90.0f,0,0);
		} else if ((transform.eulerAngles.y <= 30.0f + 180.0f) && (transform.eulerAngles.y >= -30.0f + 180.0f)) {
			Debug.Log ("3");
			//transform.rotation = new Quaternion(0,0+180.0f,0,0);
		} else if ((transform.eulerAngles.y <= 30.0f + 270.0f) && (transform.eulerAngles.y >= -30.0f + 270.0f)) {
			Debug.Log ("4");
			//transform.rotation = new Quaternion(0,0+270.0f,0,0);
		} else {
			Debug.Log ("Break");

		}
	

	}
    IEnumerator RotationCar()
    {
        float time = 0;
		while(time <= 1)
        {
            time += Time.deltaTime;            
            gameObject.transform.Rotate(0, 90 * Time.deltaTime, 0);
            yield return null;
        }

       Debug.Log("CompleteRotate");

    }
}
