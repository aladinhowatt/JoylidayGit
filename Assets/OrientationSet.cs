using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScreenOrientation = MarksAssets.ScreenOrientationWebGL.ScreenOrientationWebGL.ScreenOrientation;

public class OrientationSet : MonoBehaviour
{
    public GameObject RotateObj;

    public GameObject Button;
    public GameObject Button2;


    public GameObject GameUi;

    void Start()
    {
        Button.SetActive(true);
    }
    public void setText(int orient)
    {
        ScreenOrientation orientation = (ScreenOrientation)orient;
        Debug.Log("SSSS");
        if (orientation == ScreenOrientation.Portrait || orientation == ScreenOrientation.PortraitUpsideDown)
        {
            RotateObj.SetActive(true);
          //  Button.SetActive(false);
           // Button2.SetActive(false);
            GameUi.SetActive(false);
        }
        else
        {
            RotateObj.SetActive(false);
          
            GameUi.SetActive(true);
        }
    }
}
