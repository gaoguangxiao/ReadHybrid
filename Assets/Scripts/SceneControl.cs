using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Naninovel;
public class SceneControl : MonoBehaviour
{
    int laseSceneIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSkipCheckUpdate(int index)
    {
        //视觉阅读返回首页，将视觉阅读引擎关闭
        if(index == 0)
        {
            Engine.Destroy();
        }
        StartCoroutine(LoadScene(index));
        laseSceneIndex = index;
    }

    //加载完毕
    IEnumerator LoadScene(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        operation.completed += OnLoadScene;
        yield return operation;
    }

    //加载完成
    private void OnLoadScene(AsyncOperation obj)
    {
        Debug.Log("load finish");
    }
}
