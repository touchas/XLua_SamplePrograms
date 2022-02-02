using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LuaAddTxtSuffixEditor : Editor
{

    [MenuItem("XLua/生成txt后缀的Lua文本")]
    
    public static void CopyLuaToTxt()
    {


        //设置要拷贝的所有Lua文件的路经
        string path=Application.dataPath+"/Lua/";

        //判断路径是否存在
        if(!Directory.Exists(path))
            return;
        //递归查找该路径下的所有文件
        string[] strs= Directory.GetFiles(path,"*.lua",SearchOption.AllDirectories);
        for(int i=0;i<strs.Length;i++)
        {
            strs[i]=strs[i].Replace("\\","/");
            Debug.Log(strs[i]);
        }
        //把Lua文件拷贝到一个新的文件夹中
        //声明一个新路径
        string newPath=Application.dataPath+"/LuaToAB/";

        //判断新路径文件夹 是否存在，不存在则创建路径
        if(!Directory.Exists(newPath))
            Directory.CreateDirectory(newPath);
        else
        {
            //得到该路径中 所有后缀.txt的文件 
            string[] oldFileStrs=Directory.GetFiles(newPath,"*.txt");
            for(int i=0;i<oldFileStrs.Length;i++)
            {
                //删除找到的.txt文件
                File.Delete(oldFileStrs[i]);
            }
        }
        
        List<string> newFileNames=new List<string>();
        string fileName;
        for(int i=0;i<strs.Length;i++)
        {
            //得到新的文件路径 用于拷贝
            fileName=newPath+strs[i].Substring(strs[i].LastIndexOf("/")+1)+".txt";
            Debug.Log(strs[i].Substring(strs[i].LastIndexOf("/")+1)+".txt");
            newFileNames.Add(fileName);
            File.Copy(strs[i],fileName);
        }
        AssetDatabase.Refresh();
        //刷新过后再来该指定包
        for(int i=0;i<newFileNames.Count;i++)
        {
            //该API传入的路径 必须是 相对Assets文件夹的 Assets/.../....
            AssetImporter importer=AssetImporter.GetAtPath(newFileNames[i].Substring(newFileNames[i].IndexOf("Assets")));
            if(importer!=null)
                importer.assetBundleName="lua";
        }
        Debug.Log("生成完毕");
    }


}
