using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneUI : MonoBehaviour {


    public GameObject titlePanal;
    public GameObject playPanal;

    public Image HpImg;
    public Image ExpImg;
    
    private Player _player;
    
    void Start()
    {
        if (false == GameManager.Initialized())
            return;

        titlePanal.gameObject.SetActive(true);
        playPanal.gameObject.SetActive(false);
        _player = Player.GetInstance();        
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
        titlePanal.gameObject.SetActive(false);
        playPanal.gameObject.SetActive(true);
        GameManager.GetInstance().SetState(GameManager.state.Play);
    }


    public void OnClickAttackButton()
    {
        _player.OnAttackSkill();
        Debug.Log("Clicked Attack");
    }
    public void OnClickSkillButton(int index)
    {
        _player.OnSkill(index);
        Debug.Log(string.Format("Clicked Skill {0}", index));
    }
}
