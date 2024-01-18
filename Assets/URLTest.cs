using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class URLTest : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetURLFromPage();

    // Use this for initialization
    void Start()
    {
        Debug.Log("SES");
        Debug.Log(GetURLFromPage());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
