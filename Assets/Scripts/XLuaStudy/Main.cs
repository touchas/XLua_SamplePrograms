using System.Threading;
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
using UnityEngine.Events;
//无参无返回值
//XLua已经处理了无参无返回的委托，不需要加[CSharpCallLua]
public delegate void CustomCall();
//有参无返回，一个参数
[CSharpCallLua]
public delegate void CustomCall_v1(int a);
//有参无返回，两个参数
[CSharpCallLua]
public delegate void CustomCall_v2(int a,int b);
//有参有返回，一个返回
[CSharpCallLua]
public delegate int CustomCall_v_r1(int a);
//因为第一个返回值是number,这里采用int
//一共有5个返回值,第一个作为函数默认的返回值，后面有几个返回值就加几个out,并对应类型
[CSharpCallLua]
public delegate int CustomCall_v_mr(int a,out int b,out bool c,out string d,out int e);

//变长参数的类型根据实际情况设定，通用情况用object但会增加开销
[CSharpCallLua]
public delegate void CustomCall_mv(params object[] args);
//变长参数+固定参数
[CSharpCallLua]
public delegate void CustomCall_mv_2(int a,params object[] args);



public class CallLuaClass
{
    //在这个类中去声明成员变量
    //名字一定要和Lua那边一样
    //公共 私有和保护没办法赋值
    //这个自定义中的 变量 可以更多也可以更少
    //如果变量比 lua中的少 就会忽略它
    //如果变量比 lua中的多 不会赋值 也会忽略
    public int testInt;
    public bool testBool;
    public float testFloat;
    public float testString;
    public UnityAction testFun;

    public CallLuaInClass testInClass;

    public int i;

    public void Test()
    {
        Debug.Log(testInt);
    }

}
public class CallLuaInClass
{
    public int testInClass;
}

//接口中不允许有成员变量
//我们用属性来接收
//接口和类的规则一样 其中的的属性多了少了不影响结果 无法就是忽略他们

//嵌套几乎和类一样 无法是要遵循接口的规则
//如果接口中的内容变了，需要重新清理代码再生成代码
[CSharpCallLua]
public interface ICSharpCallInterface
{
    int testInt
    {
        get;
        set;
    }
    bool testBool
    {
        get;
        set;
    }
    float testFloat
    {
        get;
        set;
    }
    //string testString
    //{
    //    get;
    //    set;
    //}
    UnityAction testFun
    {
        get;
        set;
    }
    string test222
    {
        get;
        set;
    }
}


public class Main : MonoBehaviour
{

    void Start()
    {
        XLuaMgr.GetInstance().Init();
        XLuaMgr.GetInstance().DoLuaFile("Main");

        #region C#调用Lua变量
        //C#无法直接调用Lua脚本中的本地变量
        // int local =XLuaMgr.GetInstance().Global.Get<int>("testLocal");
        // Debug.Log(local);

        // int Lua_Int= XLuaMgr.GetInstance().Global.Get<int>("testInt");
        // Debug.Log("Lua中的Int变量："+Lua_Int);

        // string Lua_Str=XLuaMgr.GetInstance().Global.Get<string>("testStr");
        // Debug.Log("Lua中的string变量："+Lua_Str);

        // bool Lua_Bool=XLuaMgr.GetInstance().Global.Get<bool>("testBool");
        // Debug.Log("Lua中的Bool变量："+Lua_Bool);

        // float Lua_Float=XLuaMgr.GetInstance().Global.Get<float>("testFloat");
        // Debug.Log("Lua中的float变量："+Lua_Float);
        // //Lua中没有类型的概念
        // double Lua_Double=XLuaMgr.GetInstance().Global.Get<double>("testFloat");
        // Debug.Log("Lua中的double变量："+Lua_Double);

        //无参返回
        // CustomCall call=XLuaMgr.GetInstance().Global.Get<CustomCall>("testFun1");
        // call();
        // //有参无返回，一个参数
        // CustomCall_v1 call_v1=XLuaMgr.GetInstance().Global.Get<CustomCall_v1>("testFun2_1");
        // call_v1(123);
        // //有参无返回，两个参数
        // CustomCall_v2 call_v2=XLuaMgr.GetInstance().Global.Get<CustomCall_v2>("testFun2_2");
        // call_v2(111,222);
        // //有参数有返回值 一个返回值
        // CustomCall_v_r1 call_v_r1=XLuaMgr.GetInstance().Global.Get<CustomCall_v_r1>("testFun2_3");
        // Debug.Log("有参有返回值的函数："+call_v_r1(456));
        // //多返回值函数用out来接收
        // CustomCall_v_mr call_v_mr=XLuaMgr.GetInstance().Global.Get<CustomCall_v_mr>("testFun3");
        // int a,b;
        // bool c;
        // string d;
        // int e;
        // Debug.Log(call_v_mr(1,out b,out c,out d,out e));

        //变长参数
        // CustomCall_mv call_mv=XLuaMgr.GetInstance().Global.Get<CustomCall_mv>("testfun4_1");
        // call_mv(1,2,3,4,5,"AA",true);
        // //变长参数+固定参数
        // CustomCall_mv_2 call_mv_2=XLuaMgr.GetInstance().Global.Get<CustomCall_mv_2>("testfun4_2");
        // call_mv_2(100,1,2,3,4,5,"AA",true);
        
        // Debug.Log("**********List***********");
        // List<int> list=XLuaMgr.GetInstance().Global.Get<List<int>>("testList");
        // for(int i=0;i<list.Count;i++)
        // {
        //     Debug.Log("List: "+ list[i]);
        // }

        // List<object> list2=XLuaMgr.GetInstance().Global.Get<List<object>>("testList2");
        // for(int i=0;i<list2.Count;i++)
        // {
        //     Debug.Log("objectList: "+list2[i]);
        // }
        // Debug.Log("**********Dictionary***********");
        // Dictionary<string,int> dic=XLuaMgr.GetInstance().Global.Get<Dictionary<string,int>>("testDic");
        // foreach(string item in dic.Keys)
        // {
        //     Debug.Log("Dic: "+item+"_"+dic[item]);
        // }



        #endregion    
        #region C#调用Lua中的“类”
        // CallLuaClass obj = XLuaMgr.GetInstance().Global.Get<CallLuaClass>("testClass");
        // Debug.Log(obj.testInt);
        // Debug.Log(obj.testBool);
        // Debug.Log(obj.testFloat);
        // Debug.Log(obj.testString);
        // Debug.Log(obj.i);
        #endregion
        #region C#调用Lua中的“类作为接口”
        // ICSharpCallInterface obj = XLuaMgr.GetInstance().Global.Get<ICSharpCallInterface>("testClass");
        // Debug.Log(obj.testInt);
        // Debug.Log(obj.testBool);
        // Debug.Log(obj.testFloat);
        // Debug.Log(obj.test222);
        // obj.testFun();
        // //接口拷贝 是引用拷贝 改了值 lua表中的值也变了
        // obj.testInt = 10000;
        // ICSharpCallInterface obj2 = XLuaMgr.GetInstance().Global.Get<ICSharpCallInterface>("testClass");
        // Debug.Log(obj2.testInt);    
        #endregion
    }
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
        TextAsset lua = ABMgr.GetInstance().LoadRes<TextAsset>("lua", filePath + ".lua");
        if (lua != null)
            return lua.bytes;
        else
            Debug.Log("StreamingAssets文件夹AB包Lua文件重定向失败，文件名为：" + filePath);
        return null;
    }
}
