using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour {

	public float PatrolSpeed = 20.0f;

	private Quaternion Right = Quaternion.identity;

	//타이머
	public float f_time;
	public float f_dir = 1;

	void Start(){
//		//PlayerTrans = GameManager.GetInstance().GetPlayer().transform;
//		if (false == GameManager.Initialized())
//			return;
		Right.eulerAngles = new Vector3(0,90,0);

	}
	// Update is called once per frame
	void Update () {
		f_time += Time.deltaTime;
	}
	void OnTriggerEnter(Collider col){
		//일단 야매코드;;
		if (f_time >= 2) {
			if (col.tag == "Car") {
                StartCoroutine(RotationCar());
			} 
		}
	}
	public void SetDirection(float dir){
		f_dir = dir; 
		Debug.Log (f_dir);
	}
//	void OnTriggerExit(Collider col){
//		//일단 야매코드;;
//		if (f_time >= 3) {
//			if (col.tag == "Car") {
//				CorrectlyCarPos ();
//			}
//		}
//	}
	void CorrectlyCarPos(){

		//transform.eulerAngles.y
		if ((transform.eulerAngles.y <= 30.0f) && (transform.eulerAngles.y >= -30.0f)) {
			Debug.Log ("1");
			transform.rotation = Quaternion.Euler(0,0.0f,0);
		} else if ((transform.eulerAngles.y <= 30.0f + 90.0f) && (transform.eulerAngles.y >= -30.0f + 90.0f)) {
			Debug.Log ("2");
			transform.rotation = Quaternion.Euler(0,0+90.0f,0);
		} else if ((transform.eulerAngles.y <= 30.0f + 180.0f) && (transform.eulerAngles.y >= -30.0f + 180.0f)) {
			Debug.Log ("3");
			transform.rotation = Quaternion.Euler(0,0+180.0f,0);
		} else if ((transform.eulerAngles.y <= 30.0f + 270.0f) && (transform.eulerAngles.y >= -30.0f + 270.0f)) {
			Debug.Log ("4");
			transform.rotation = Quaternion.Euler(0,0+270.0f,0);
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
            gameObject.transform.Rotate(0, -90 * f_dir* Time.deltaTime, 0);

            yield return null;
        }
		CorrectlyCarPos ();

       Debug.Log("CompleteRotate");

    }
}
