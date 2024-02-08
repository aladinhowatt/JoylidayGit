using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Weburl;

public class RewardManager : MonoBehaviour
{
    public GameObject RewardFrame;
    public GameObject PresentBox;


    public GameObject ItemCodeFrame;
    string itemCode;
    public Text itemCodeText;

    public Weburl webUrl;

    ItemCodeData itemdata;

    void Start()
    {
        webUrl.onSaveItem += OnSaveItem;
    }

    private void OnSaveItem(ItemCodeData itemData)
    {
        RewardFrame.SetActive(true);
        this.itemdata = itemData;
    }

    public void ShowRewardFrame(int score)
    {
        StartCoroutine(webUrl.GetItem(score));
       


    }

    public void OnClickReward()
    {
        PresentBox.SetActive(false);

        ItemCodeFrame.SetActive(true);
        PresentBox.SetActive(false);
        itemCodeText.text = itemdata.rw_ItemDescription;

       
    }

    public void OnClickOk()
    {
        RewardFrame.SetActive(false);
        SceneManager.LoadScene("SampleScene");

    }
    private void OnDestroy()
    {
        webUrl.onSaveItem -= OnSaveItem;
    }
}
