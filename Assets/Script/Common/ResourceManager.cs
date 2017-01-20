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
    private Dictionary<string, GameObject> _prefabDic;

    private void Init()
    {
        DontDestroyOnLoad(this);
        _particleDic = new Dictionary<string, GameObject>();
        _prefabDic = new Dictionary<string, GameObject>();
    }

    public ParticleSystem MakeParticle(Vector3 pos, string name,float lifeTime)
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
                return null;
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
        {
            StartCoroutine(CoParticleUpdate(particleSystem, lifeTime));
            return particleSystem;
        }
        else
            return null;
    }
    

    IEnumerator CoParticleUpdate(ParticleSystem particle,float lifeTime)
    {
        float time = 0.0f;

        while (lifeTime > time)
        {
            time += Time.deltaTime;
            yield return null;
        }

        if(null != particle)
            Destroy(particle.gameObject);

    }


    public GameObject GetPrefab(string path)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Prefabs/");
        sb.Append(name);

        GameObject prefab;

        if (false == _prefabDic.ContainsKey(sb.ToString()))
        {

            prefab = Resources.Load(sb.ToString()) as GameObject;


            if (null == prefab)
            {
                Debug.LogError("Fail to Load Prefab");
                return null;
            }
            else
                _prefabDic.Add(sb.ToString(), prefab);
        }
        else
        {
            prefab = _particleDic[sb.ToString()];
        }

        return GameObject.Instantiate(prefab) as GameObject;
    }
}
