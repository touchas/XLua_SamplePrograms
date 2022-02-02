using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 泛型基类，无需挂载在游戏对象上
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonBase<T> where T:new()
{
    private static T instance;

    public static T GetInstance()
    {
        if (instance == null)
            instance = new T();
        return instance;
    }
}

