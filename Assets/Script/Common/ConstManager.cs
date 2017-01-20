using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConstNameSpace
{
    public enum ObjectType
    {
        Player,
        Enemy,
        Car,
    }

    public class ObjectInfo
    {
        public int Hp { get; private set;}
        public int Atk { get; private set;}
        
        public ObjectInfo(int hp, int atk)
        {
            Hp = hp;
            Atk = atk;
        }
    }
	
}