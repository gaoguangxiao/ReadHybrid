using System.Collections;
using UnityEngine;

public class Hello
{
    public static void Run()
    {
        Debug.Log("Hello, Hybrid");

        //动态挂载热更新脚本
        GameObject go = new GameObject("Test2");
        go.AddComponent<Print>();
    }
}