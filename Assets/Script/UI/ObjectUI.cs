﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectUI : MonoBehaviour {

    private GameObject hpbar;
    private RectTransform hpBarRectTransform;

	// Use this for initialization
	void Start () 
    {
        GameObject prefab = Resources.Load("Prefabs/Hpbar") as GameObject;
        hpbar = GameObject.Instantiate(prefab) as GameObject;
        hpBarRectTransform = hpbar.GetComponent<RectTransform>();
        hpBarRectTransform.SetParent(GameManager.GetInstance().playsceneUI.Panal.transform);        
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 pos = this.transform.position;
       
        Vector3 viewPos = Camera.main.WorldToViewportPoint(pos);
        Vector2 anchorPos = new Vector2(viewPos.x * 1280, viewPos.y * 720);
        hpBarRectTransform.anchoredPosition = anchorPos;
	}

    void OnDestroy()
    {
        Destroy(hpbar);
    }
}
