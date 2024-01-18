using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject Ui;
    public GameObject Bg;
    public PlatformManager platform;
    SoundManager soundManager;

   public Weburl requestObject;

    AudioSource audi;
    public AudioClip clickSound;
    
    void Start()
    {
        audi = GetComponent<AudioSource>();
        requestObject.onCompleteDelegateBonus += OnUpdateBonus;
       soundManager = GameObject.Find("SoundObject").GetComponent<SoundManager>();
         soundManager.PlayTitleSound();
    }

    public void StartGame()
    {
        Debug.Log("qqqqqq");
         DecreaseBonus();

    

    }

    public void OnUpdateBonus()
    {
        audi.PlayOneShot(clickSound);
        Ui.SetActive(false);
        Bg.SetActive(false);
        Debug.Log("SADSADS");
       requestObject.onCompleteDelegateBonus -= OnUpdateBonus;
       soundManager.PlayInGameSound();
        SceneManager.LoadScene("GameScene_RunnerMapGeneration");
       
      

        // platform.enabled = true;
        //Debug.Log("DFDSFDS");


    }
    private void OnDestroy()
    {
        requestObject.onCompleteDelegateBonus -= OnUpdateBonus;
    }
    private void DecreaseBonus()
    {
        Debug.Log("bbbb");
        requestObject.UpdateBonus();
    }

    void Update()
    {
        
    }
}
