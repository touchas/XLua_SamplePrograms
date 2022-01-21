using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class Main : MonoBehaviour
{

    void Start()
    {
        //初始化Lua解析器
        LuaEnv env=new LuaEnv();
        

        //文件加载重定向
        //env.AddLoader(CustomLoader_Editor);
        env.AddLoader(CustomLoader_StreamingAssets);

        //env.DoString("要执行的lua代码")
        env.DoString("require('Main')");

        //清除Lua中没有手动释放的对象 进行垃圾回收
        //帧更新中定时执行 或者 切场景时执行
        env.Tick();
        //销毁Lua解析器
        env.Dispose();

    }
    /// <summary>
    /// 编辑器Lua文件加载重定向
    /// </summary>
    /// <param name="filePath">传入的是require的lua脚本名</param>
    /// <returns></returns>
    private byte[] CustomLoader_Editor(ref string filePath)
    {
        //设置Lua文件所在路径，设置为Lua文件夹下的Lua脚本
        string path=Application.dataPath+"/Lua/"+filePath+".lua";

        if(File.Exists(path))
        {
            Debug.Log(filePath+"文件存在");
            //文件存在，返回byte[]
            return File.ReadAllBytes(path);
        }
        else
        {
            //文件不存在，打印文件路径
            Debug.LogError("编辑器Lua文件重定向失败，文件名为："+filePath);
        }
        return null;
    }
    /// <summary>
    /// StreamingAssets文件加载重定向，打AB包后
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private byte[] CustomLoader_StreamingAssets(ref string filePath)
    {
        Debug.Log("Lua:"+filePath);
        string path = Application.streamingAssetsPath + "/lua";
        //加载AB包
        AssetBundle ab = AssetBundle.LoadFromFile(path);
        //加载Lua文件 为了AB包能够打包，Lua脚本文件必须以.txt作为后缀名
        TextAsset lua=ab.LoadAsset<TextAsset>(filePath+".lua");
        if(lua!=null)
        {
            //文件存在，返回bytes
            return lua.bytes;      
        }
        else
        {
            //文件不存在，打印文件路径
            Debug.LogError("StreamingAssets文件 Lua文件重定向失败，文件名为："+filePath);
        }
        return null;
    }
}
