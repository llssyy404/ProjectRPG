using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneUI : MonoBehaviour {

    public Image HpImg;
    public Image ExpImg;
    public GameObject Panal;

    public GameObject title;
    public GameObject startButton;

    void start()
    {
     
    }

    public void SetPlayerHpBar(float value)
    {
        HpImg.fillAmount = Mathf.Clamp(value, 0.0f, 1.0f);
    }

    public void SetPlayerExpBar(float value)
    {
        ExpImg.fillAmount = Mathf.Clamp(value, 0.0f, 1.0f);
    }

    public void OnClickStartButton()
    {
        title.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        GameManager.GetInstance().SetState(GameManager.state.Play);
    }
}
