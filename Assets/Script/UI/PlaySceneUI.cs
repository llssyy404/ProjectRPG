using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneUI : MonoBehaviour {

    public Image HpImg;
    public Image ExpImg;
    public GameObject Panal;
  
    public void SetPlayerHpBar(float value)
    {
        HpImg.fillAmount = Mathf.Clamp(value, 0.0f, 1.0f);
    }

    public void SetPlayerExpBar(float value)
    {
        ExpImg.fillAmount = Mathf.Clamp(value, 0.0f, 1.0f);
    }
}
