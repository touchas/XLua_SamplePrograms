using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ABMgr : SingletonAutoMono<ABMgr>
{
    //主包
    private AssetBundle mainAB = null;

    //依赖包获取用的配置文件
    private AssetBundleManifest manifest = null;

    //字典 用字典来存储 加载过的AB包 防止重复加载报错
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// AB包存放路径 方便修改
    /// </summary>
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }
    /// <summary>
    /// 主包名 在打包前要注意命名
    /// 例如：StanderdedWindows要改为PC
    /// </summary>
    private string MainABName
    {
        get
        {
#if UNITY_IOS
            return "IOS";
#elif UNITY_ANDROID
            return "Android";
#else
            return "PC";
#endif
        }
    }
    /// <summary>
    /// 加载AB包
    /// </summary>
    /// <param name="abName"></param>
    public void LoadAB( string abName )
    {
        //加载AB包
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        //我们获取依赖包相关信息
        AssetBundle ab = null;
        string[] strs = manifest.GetAllDependencies(abName);
        for (int i = 0; i < strs.Length; i++)
        {
            //判断包是否加载过
            if (!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        //加载资源来源包
        //判断字典中是否包含 如果没有加载过 再加载
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }
    //同步加载 不指定类型 默认直接实例化
    public Object LoadRes(string abName, string resName,bool isInstantiate=true)
    {
        //加载AB包
        LoadAB(abName);
        //判断资源是不是GameObject
        //如果是 直接实例化返回给外部
        Object obj = abDic[abName].LoadAsset(resName);
        if(isInstantiate)
        {
            if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
        }
        else
        {
            return obj;
        }
        
    }
    //同步加载 根据泛型指定类型 默认直接实例化
    public T LoadRes<T>(string abName, string resName,bool isInstantiate=true) where T:Object
    {
        //加载AB包
        LoadAB(abName);
        //为了外面方便 在加载资源时 判断一下 资源是不是GameObject
        //如果是 直接实例化了 再返回给外部
        T obj = abDic[abName].LoadAsset<T>(resName);
        if(isInstantiate)
        {
            if (obj is GameObject)
                return Instantiate(obj);
            else
                return obj;
        }
        else
        {
            return obj;     
        }
        
    }
    //所有包的卸载
    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }
}
