using System.Collections;
using UnityEngine;
using TMPro;

public class Hello: MonoBehaviour
{
    public TMP_Text verText;

    private void Start()
    {

        verText.text = "版本：3";

        Debug.Log("Hello start MonoBehaviour");
    }

    public static void Run()
    {
        Debug.Log("Hello, Hybrid");


        
       
    }
}