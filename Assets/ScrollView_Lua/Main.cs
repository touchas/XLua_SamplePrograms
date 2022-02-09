using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XLua;
using UnityEngine.Events;
using System;

namespace ScrollView_Lua
{
    public class XLuaSystem
    {
        [LuaCallCSharp]
        public static List<Type> luaCallCsharpList = new List<Type>()
        {
            typeof(UnityAction<float>),
            typeof(GameObject)
        };
    }
    public class Main : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            XLuaMgr.GetInstance().Init();
            XLuaMgr.GetInstance().DoLuaFile("SVLua_Main");
            //GameObject obj=new GameObject();
            //GetComponent<Image>().overrideSprite
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}