﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviour {

    public GameObject player;

    public float offsetX = 0f;
    public float offsetY = 25f;
    public float offsetZ = -35f;

    public float shakes = 0f;
    public float shakeAmout = 0.7f;
    public float decreaseFactor = 1f;

    Vector3 originalPos;
    bool CameraShaking;

    Vector3 cameraaPosition;

	// Use this for initialization
	void Start () 
    {
        player = GameManager.GetInstance().GetPlayer().gameObject;
        originalPos = transform.position;
        CameraShaking = false;
	}

	public void ShakeCamera(float shaking)
    {
        shakes = shaking;
        originalPos = transform.position;
        CameraShaking = true;
    }
  
	//카메라 shaking
	void FixedUpdate () 
    {
        if (CameraShaking)
        {
            if (shakes > 0)
            {
                transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmout;
              //  transform.position += new Vector3(0f, 0f, -30f);

                shakes -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakes = 0f;
                transform.localPosition = originalPos;
                CameraShaking = false;
            }
        }
	}

    //카메라 move
    void LateUpdate()
    {
        cameraaPosition.x = player.transform.position.x +offsetX;
        cameraaPosition.y = player.transform.position.y +offsetY;
        cameraaPosition.z = player.transform.position.z +offsetZ;

        transform.position = Vector3.Lerp(transform.position, cameraaPosition, 5f * Time.deltaTime);

        Camera.main.transform.LookAt(player.transform);
    }
}