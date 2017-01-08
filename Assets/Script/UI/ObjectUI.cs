using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectUI : MonoBehaviour {

    private GameObject hpbar;
    private RectTransform hpBarRectTransform;

    private Image _hpbarImg;
    private int _hp;
    private int _maxhp;
    
	// Use this for initialization
	public void Init (int maxhp) 
    {
        _hp = maxhp;
        _maxhp = maxhp;

        GameObject prefab = Resources.Load("Prefabs/Hpbar") as GameObject;
        hpbar = GameObject.Instantiate(prefab) as GameObject;
        _hpbarImg = hpbar.transform.GetChild(0).GetComponent<Image>();
        hpBarRectTransform = hpbar.GetComponent<RectTransform>();
        hpBarRectTransform.SetParent(GameManager.GetInstance().playsceneUI.Panal.transform);        
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 pos = this.transform.position;
        pos.y += 5;
        Vector3 viewPos = Camera.main.WorldToViewportPoint(pos);
        Vector2 anchorPos = new Vector2(viewPos.x * 1280, viewPos.y * 720);
        hpBarRectTransform.anchoredPosition = anchorPos;
	}

    void OnDestroy()
    {
        Destroy(hpbar);
    }


    public void DamageHp(int damage)
    {
        _hp -= damage;

        _hpbarImg.fillAmount = Mathf.Clamp((float)_hp / _maxhp, 0.0f, 1.0f);
    }
}
