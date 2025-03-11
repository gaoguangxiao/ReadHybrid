using HybridCLR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class LoadDll : MonoBase
{

    void Start()
    {

        NetManager.Instance.Register(this);

        // Editor环境下，HotUpdate.dll.bytes已经被自动加载，不需要加载，重复加载反而会出问题。
        //要访问`Documents`文件夹，可以使用`Application.persistentDataPath`方法，该方法返回一个适用于特定平台的文件路径，在iOS上这个路径指向`Document`
        //Application.streamingAssetsPath("");
#if !UNITY_EDITOR
        //Assembly hotUpdateAss = Assembly.Load(File.ReadAllBytes($"{Application.streamingAssetsPath}/HotUpdate.dll.bytes"));

        string temporaryCachePath = Application.temporaryCachePath;
        string webResourcePath = Path.Combine(temporaryCachePath, "WebResource");
        // 确保目录存在
        if (!Directory.Exists(webResourcePath))
        {
            Directory.CreateDirectory(webResourcePath);
        }

        string hotUpdatePath = webResourcePath + "/pkg/static/1/HotUpdate.dll.bytes";

        Debug.Log("hotUpdatePath" + hotUpdatePath);
        Assembly hotUpdateAss = Assembly.Load(File.ReadAllBytes($"hotUpdatePath"));
#else
        // Editor下无需加载，直接查找获得HotUpdate程序集
        Assembly hotUpdateAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotUpdate");
#endif

        Type type = hotUpdateAss.GetType("Hello");
        type.GetMethod("Run").Invoke(null, null);
    }

    private void OnDestroy()
    {
        Debug.Log("LoadDll OnDestroy");
        NetManager.Instance.UnRegister(this);
    }

    //获取客户端存储的更新路径

}
