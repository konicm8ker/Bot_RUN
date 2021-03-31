using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Browser : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern bool GetBrowser();
    public bool isFirefox;

    void Awake()
    {
        isFirefox = GetBrowser();
        Debug.Log("Is Firefox: " + isFirefox);
    }
}
