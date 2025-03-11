using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;

public class ReadStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Engine.OnInitializationFinished += OnInitializationFinished;

        bool isInitialized = Engine.Initialized;
        Debug.Log("isInitialized: " + isInitialized);
        if (!isInitialized)
        {
            //手动初始化
            InitEngine();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //手动初始化`Naninovel`
    async void InitEngine()
    {
        await RuntimeInitializer.Initialize();
    }

    void OnInitializationFinished()
    {
        Debug.Log("OnInitializationFinished");
        StartLoadRead();
    }

    async void StartLoadRead()
    {
        var player = Engine.GetService<IScriptPlayer>();
        await player.LoadAndPlay("NaninovelG");

    }
}
