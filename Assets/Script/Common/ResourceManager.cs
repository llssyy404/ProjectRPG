using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    private static ResourceManager _instance;
    public static ResourceManager GetInstance()
    {
        if (null == _instance)
        {
            GameObject resourceObj = new GameObject();
            var resourceManager = resourceObj.AddComponent<ResourceManager>();
            _instance = resourceManager;
            resourceManager.Init();
            resourceManager.name = "ResourceManager";

        }
        return _instance;
    }

   
    private Dictionary<string, GameObject> _particleDic;

    private void Init()
    {
        DontDestroyOnLoad(this);
        _particleDic = new Dictionary<string, GameObject>();
    }

    public void MakeParticle(Vector3 pos, string name,float lifeTime)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Prefabs/Effect/");
        sb.Append(name);

        GameObject effectObj;

        if (false == _particleDic.ContainsKey(sb.ToString()))
        {

            effectObj = Resources.Load(sb.ToString()) as GameObject;
           

            if (null == effectObj)
            {
                Debug.LogError("Fail to Load Effect");
                return;
            }
            else
                _particleDic.Add(sb.ToString(), effectObj);
        }
        else
        {
            effectObj = _particleDic[sb.ToString()];
        }

        var particleObj = GameObject.Instantiate(effectObj) as GameObject;

        particleObj.transform.position = pos;          
        var particleSystem = particleObj.GetComponent<ParticleSystem>();        
        if (null != particleSystem)
            StartCoroutine(CoParticleUpdate(particleSystem, lifeTime));
    }
    

    IEnumerator CoParticleUpdate(ParticleSystem particle,float lifeTime)
    {
        float time = 0.0f;

        while (lifeTime > time)
        {
            time += Time.deltaTime;
            Debug.Log(time);
            yield return null;
        }
        
        Destroy(particle.gameObject);

    }
}
