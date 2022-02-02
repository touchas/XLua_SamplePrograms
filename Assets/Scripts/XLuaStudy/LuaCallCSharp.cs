using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XLua;

public class Test
{
    public void Speak(string str)
    {
        Debug.Log("Test:"+str);
    }
}
namespace TestNameSpace
{
    public class Test2
    {
        public void Speak(string str)
        {
            Debug.Log("Test2:" + str);
        }
    }
}
/// <summary>
/// 自定义测试枚举
/// </summary>
public enum E_MyEnum
{
    Idle,
    Move,
    Atk
}

public class ArrayTest
{
    public int[] array=new int[5]{1,2,3,4,5};

    public List<int> list=new List<int>();

    public Dictionary<int,string> dic=new Dictionary<int, string>();

    public void Test()
    {

    }
}
//想要在Lua中使用拓展方法 一定要在工具类前面加上特性
//建议 Lua中要使用的类 都加上该性能 可以提升性能
//如果不加该特性 除了拓展方法对应的类 其他的类的使用都不会报错
//但是Lua是通过反射的机制去调用的C#类 效率较低
[LuaCallCSharp]
public static class Tools
{
    //Lesson4的拓展方法
    public static void Move(this ExtensionTestClass obj)
    {
        Debug.Log(obj.name + "移动");
    }
}
/// <summary>
/// 扩展方法测试类
/// </summary>
public class ExtensionTestClass
{
    public string name = "ABC";
    public void Speak(string str)
    {
        Debug.Log(str);
    }
    public static void Eat()
    {
        Debug.Log("吃东西");
    }
}
public class RefOutFunTestClass
{
    public int RefFun(int a,ref int b,ref int c,int d)
    {
        b = a + d;
        c = a - d;
        return 100;
    }
    public int OutFun(int a, out int b, out int c, int d)
    {
        b = a;
        c = d;
        return 200;
    }
    public int RefOutFun(int a,out int b,ref int c)
    {
        b = a * 10;
        c = a * 20;
        return 300;
    }
}
public class OverloadedFuncTest
{
    public int Calc()
    {
        return 100;
    }
    public int Calc(int a,int b)
    {
        return a + b;
    }
    public int Calc(int a)
    {
        return a;
    }
    public float Calc(float a)
    {
        return a;
    }
    
}

public class CallDelTesTClass
{
    public UnityAction del;
    public event UnityAction eventAction;

    public void DoEvent()
    {
        if (eventAction != null)
        {
            eventAction();
        }
    }
    public void ClearEvent()
    {
        eventAction = null;
    }
}
public class Array2DTraversalTestClass
{
    public int[,] array = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };
}

public static class SliderEventTestClass
{
    [CSharpCallLua]
    public static List<Type> csharpCallLuaList = new List<Type>()
    {
        typeof(UnityAction<float>)
    };

}
public class T_TestClass
{
    public interface ITest
    {

    }
    public class TestFather
    {
       
    }
    public class TestChild:TestFather,ITest
    {

    }
    public void TestFun1<T>(T a, T b) where T:TestFather
    {
        Debug.Log("有参数有约束的泛型方法");
    }
    public void TestFun2<T>(T a)
    {
        Debug.Log("有参数，没有约束");
    }
    public void TesFun3<T>() where T:TestFather
    {
        Debug.Log("有约束，但是没有参数的泛型函数");
    }
    public void TestFun<T>(T a) where T : ITest
    {
        Debug.Log("有约束，有参数，但是约束不是类是接口");
    }

}
public class LuaCallCSharp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
