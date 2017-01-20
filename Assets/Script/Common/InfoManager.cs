using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConstNameSpace;

public class InfoManager : MonoBehaviour {


    public ObjectInfo Player {get; private set;}
    public ObjectInfo Enemy {get; private set;}

    private static InfoManager _instance;
    public static InfoManager GetInstance()
    {
        if (null == _instance)
        {
            GameObject InfoObj = new GameObject();
            var InfoManager = InfoObj.AddComponent<InfoManager>();
            _instance = InfoManager;
            InfoManager.Init();
            InfoManager.name = "InfoManager";

        }
        return _instance;
    }

    void Init()
    {
        Player = new ObjectInfo(100, 10);
        Enemy = new ObjectInfo(100, 10);
    }
}
