using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private static SoundManager _instance;
    public static SoundManager GetInstance()
    {
        if(null ==_instance)
        {
            GameObject soundObject = new GameObject();
            var soundManager = soundObject.AddComponent<SoundManager>();
            _instance = soundManager;
            soundManager.Init();
            soundObject.name = "SoundManager";
            
        }
        return _instance;
    }

    
    private AudioSource _source;
    private Dictionary<string, AudioClip> _soundDic;

    private void Init()
    {
        DontDestroyOnLoad(this);
        gameObject.AddComponent<AudioListener>();
        _source = this.gameObject.AddComponent<AudioSource>();
        _source.playOnAwake = false;
        _soundDic = new Dictionary<string, AudioClip>();
    }
    
    public void PlayLoopBgm(string name)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Sound/");
        sb.Append(name);

        AudioClip bgm;

        if (false ==_soundDic.ContainsKey(sb.ToString()))
        {        
            bgm = (AudioClip)Resources.Load(sb.ToString());

            if (null == bgm)
            {
                Debug.LogError("Fail to Load Bgm");
                return;
            }
            else
                _soundDic.Add(sb.ToString(), bgm);                
        }
        else
        {
            bgm = _soundDic[sb.ToString()];                        
        }

        _source.Stop();
        _source.clip = bgm;
        _source.loop = true;
        _source.Play();
    }
   
    public void PlayOneshotClip(string name)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Sound/");
        sb.Append(name);

        AudioClip oneShotClip;

        if (false == _soundDic.ContainsKey(sb.ToString()))
        {
            oneShotClip = (AudioClip)Resources.Load(sb.ToString());

            if (null == oneShotClip)
            {
                Debug.LogError("Fail to Load OneShot");
                return;
            }
            else
                _soundDic.Add(sb.ToString(), oneShotClip);                        
        }
        else
        {
             oneShotClip = _soundDic[sb.ToString()];            
        }

        _source.PlayOneShot(oneShotClip);
        
    }

}
