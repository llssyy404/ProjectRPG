using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneUI : MonoBehaviour {


    public GameObject titlePanal;
    public GameObject playPanal;
    public GameObject clearPanal;
    public GameObject gameOverPanal;

    public Image HpImg;
    public Image ExpImg;
    
    private Player _player;
    
    void Start()
    {
        if (false == GameManager.Initialized())
            return;

        SetUIState(GameManager.state.Title);

        _player = Player.GetInstance();        
    }


    public void SetPlayerHpBar(float value)
    {
        HpImg.fillAmount = Mathf.Clamp(value, 0.0f, 1.0f);
        
        if(0 >= value)
            SetUIState(GameManager.state.GameOver);
    }

    public void SetPlayerExpBar(float value)
    {
        ExpImg.fillAmount = Mathf.Clamp(value, 0.0f, 1.0f);
    }

    public void OnClickStartButton()
    {
        SetUIState(GameManager.state.Play);
        GameManager.GetInstance().SetState(GameManager.state.Play);
    }


    public void OnClickAttackButton()
    {
        _player.OnAttackSkill();
    }
    public void OnClickSkillButton(int index)
    {
        _player.OnSkill(index);
    }
    public void OnClickExitButton()
    {
        Application.Quit();
    }


    private void SetUIState(GameManager.state state)
    {
        titlePanal.gameObject.SetActive(state == GameManager.state.Title);
        playPanal.gameObject.SetActive(state == GameManager.state.Play);
        clearPanal.gameObject.SetActive(state == GameManager.state.Clear);
        gameOverPanal.gameObject.SetActive(state == GameManager.state.GameOver);
    }
}
