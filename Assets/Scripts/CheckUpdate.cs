using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Newtonsoft.Json;
using System.Globalization;

//Unity和客户端的检查更新
public class CheckUpdate : MonoBase
{
    public TMP_Text proText;

    //必须实现，接受客户端的回调结果
    private void Awake()
    {
        BridgeScript.CallRegisterCallBackDelegate();
    }

    // Start is called before the first frame update
    void Start()
    {

        NetManager.Instance.Register(this);

        //调用下载接口
        Dictionary<string, object> paramsDicts = new Dictionary<string, object>();
        paramsDicts.Add("type", "1");
        paramsDicts.Add("item", "config.json");
        paramsDicts.Add("path","/unity/config/");//将资源下载至某目录
        paramsDicts.Add("progressAction", "checkUpdate");
        Message message = new Message(MessageType.Type_plug, MessageType.loadPkg, paramsDicts);
        BridgeScript.Instance.CallApp(message);

        //int loadedSize = 80;
        //int totalSize = 90;
        //float pro = (float)loadedSize / totalSize * 100;

        //float number = 123.4567f;
        //string formattedNumber = pro.ToString("F2"); // 结果为"123.46"

        //proText.text = "更新进度:" + pro.ToString("F2") + "%";
        //Debug.Log("bridge.loadedSize" + loadedSize);
        //Debug.Log("bridge.totalSize" + totalSize);
        //Debug.Log("bridge.pro" + pro);
    }

    private void OnDestroy()
    {
        Debug.Log("CheckUpdate OnDestroy");
        NetManager.Instance.UnRegister(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSkipCheckUpdate()
    {
        StartCoroutine(LoadScene(1));
    }

    //接受`BridgeScript`回调
    public override void ReceiveMessage(Message message)
    {
        Debug.Log("CheckUpdate ReceiveMessage：" + message.Command);
        if (message.Command == MessageType.loadPkg)
        {
            Debug.Log("loadpkg complete：" + message.Data);
            //ResponseLodPkg
            //进入主界面
            StartCoroutine(LoadScene(1));

        } else if(message.Command == "checkUpdate")
        {
            //计算mes的进度
            ResponseLoadPkg bridge = JsonConvert.DeserializeObject<ResponseLoadPkg>(message.ResponseObject.data);
            float pro = (float)bridge.loadedSize/bridge.totalSize*100;
            //Debug.Log("bridge.loadedSize" + bridge.loadedSize);
            //Debug.Log("bridge.totalSize" + bridge.totalSize);
            //Debug.Log("bridge.pro" + pro);
            //proText.text = "更新进度:" + pro + "%";
            proText.text = "更新进度:" + pro.ToString("F2") + "%";
        }
        //Debug.Log(message.Command);

    }

    //加载完毕
    IEnumerator LoadScene(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.completed += OnLoadScene;
        yield return operation;
    }

    //加载完成
    private void OnLoadScene(AsyncOperation obj)
    {
        Debug.Log("load finish");
    }

}

public class ResponseLoadPkg
{
    public int totalSize;
    public int loadedSize;
    public int speed;
    public int loaded;
    public int total;
}
