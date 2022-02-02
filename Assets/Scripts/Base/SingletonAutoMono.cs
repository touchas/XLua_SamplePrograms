using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用于需要继承MonoBehaviour的单例基类，无需挂载在游戏对象上
/// </summary>
public class SingletonAutoMono<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance;

    public static T GetInstance()
    {
        if( instance == null )
        {
            GameObject obj = new GameObject();
            //设置对象的名w为脚本名
            obj.name = typeof(T).ToString();
            //防止切换场景被移除
            DontDestroyOnLoad(obj);
            //返回脚本对象
            instance = obj.AddComponent<T>();
        }
        return instance;
    }

}
