using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
public class XLuaMgr :SingletonBase<XLuaMgr>
{
    //lua解析器
    private LuaEnv luaEnv;

    /// <summary>
    /// 获取Lua中的_G
    /// </summary>
    /// <value></value>
    public LuaTable Global
    {
        get
        {
            return luaEnv.Global;
        }
    }
    public void Init()
    {
        //已经初始化了 别初始化 直接返回
        if (luaEnv != null)
            return;
        //初始化
        luaEnv = new LuaEnv();
        //加载Lua脚本，重定向
        luaEnv.AddLoader(CustomLoader_Editor);
        luaEnv.AddLoader(CustomLoader_StreamingAssets);
    }
    /// <summary>
    /// 编辑器Lua文件加载重定向
    /// </summary>
    /// <param name="filePath">传入的是require的lua脚本名</param>
    /// <returns></returns>
    private byte[] CustomLoader_Editor(ref string filePath)
    {
        #region 原始代码
        // //设置Lua文件所在路径，设置为Lua文件夹下的Lua脚本
        // string path=Application.dataPath+"/Lua/"+filePath+".lua";

        // Debug.Log(path);
        // if(File.Exists(path))
        // {
        //     Debug.Log(filePath+"文件存在");
        //     //文件存在，返回byte[]
        //     return File.ReadAllBytes(path);
        // }
        // else
        // {
        //     //文件不存在，打印文件路径
        //     Debug.Log("编辑器Lua文件重定向失败，文件名为："+filePath);
        // }
        // return null;    
        #endregion

        #region 改进代码
        string path=Application.dataPath+"/Lua/";
        //查找所有文件夹下后缀为.lua的文件
        string[] paths= Directory.GetFiles(path,filePath+".lua",SearchOption.AllDirectories);
        if(paths.Length==0)
        {
            Debug.Log("编辑器Lua文件重定向失败，文件名为："+filePath);
            return null;      
        }
            
        if(File.Exists(paths[0]))
        {
            // Debug.Log(filePath+"文件存在");
            //文件存在，返回byte[]
            return File.ReadAllBytes(paths[0]);
        }
        else
        {
            //文件不存在，打印文件路径
            Debug.Log("编辑器Lua文件重定向失败，文件名为："+filePath);
            return null;
        }
        #endregion
        
    }
    /// <summary>
    /// StreamingAssets文件加载重定向，打AB包后
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private byte[] CustomLoader_StreamingAssets(ref string filePath)
    {
        TextAsset lua = ABMgr.GetInstance().LoadRes<TextAsset>("lua", filePath + ".lua");
        if (lua != null)
            return lua.bytes;
        else
            Debug.Log("StreamingAssets文件夹AB包Lua文件重定向失败，文件名为：" + filePath);
        return null;
    }
    /// <summary>
    /// 传入文件名 执行lua脚本
    /// </summary>
    /// <param name="fileName"></param>
    public void DoLuaFile(string fileName)
    {
        string str = string.Format("require('{0}')", fileName);
        DoString(str);
    }
    /// <summary>
    /// 执行lua脚本
    /// </summary>
    /// <param name="str"></param>
     public void DoString(string str)
    {
        if(luaEnv==null)
        {
            Debug.Log("解析器未初始化");
            return;
        }
        luaEnv.DoString(str);
    }
    /// <summary>
    /// 垃圾清除
    /// </summary>
    public void Tick()
    {
        luaEnv.Tick();
    }
    /// <summary>
    /// 销毁解析器
    /// </summary>
    public void  Dispose()
    {
        luaEnv.Dispose();
        luaEnv = null;
    }
}
