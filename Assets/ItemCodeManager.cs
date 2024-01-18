using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Weburl;

public class ItemCodeManager : MonoBehaviour
{

    public GameObject MainFrame;
    public GameObject ItemCodeFrame;

    public GameObject contEntParent;
    public ItemCode itemCodePrefab;

    public Weburl webUrl;

    public GameObject UseItemCodeCanvas;
    public Text ItemCodeDescriptionText;
    ItemCodeData currentCode;
    void Start()
    {
        webUrl.onGetUserItem += onGetUSerItem;

        webUrl.onCompleteUseItem += onCompleteUseItemCode;
    }

    

    private void onGetUSerItem(List<ItemCodeData> itemDatas)
    {
        MainFrame.SetActive(false);
        ItemCodeFrame.SetActive(true);


        foreach (Transform child in contEntParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var a in itemDatas)
        {
            var qq = Instantiate(itemCodePrefab.gameObject, contEntParent.transform);
            qq.GetComponent<ItemCode>().setText(a.rw_ItemDescription);
            qq.GetComponent<Button>().onClick.AddListener(delegate { ShowUseItemPopup(a); });

        }
    }

    private void OnDestroy()
    {
        webUrl.onGetUserItem -= onGetUSerItem;
        webUrl.onCompleteUseItem -= onCompleteUseItemCode;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnClickItemCodeButton()
    {
        webUrl.GetUserItem();




        /*
        foreach(var a in GameManager.rewardArray)
        {
            if(a.Length > 1)
            {
                var qq = Instantiate(itemCodePrefab.gameObject, contEntParent.transform);
                qq.GetComponent<ItemCode>().setText(a);
                qq.GetComponent<Button>().onClick.AddListener(delegate { RemoveReward(a); });
            }
        }*/

    }

    private void ShowUseItemPopup(ItemCodeData a)
    {

        UseItemCodeCanvas.SetActive(true);

        ItemCodeDescriptionText.text = a.rw_ItemDescription;
        currentCode = a;
        //SceneManager.LoadScene("SampleScene");
    }

    public void OnClickUseITemCode()
    {
        webUrl.UseItemCode(currentCode.rw_id);
    }

    private void onCompleteUseItemCode()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnCancelUseItem()
    {
        UseItemCodeCanvas.SetActive(false);
    }

    public void OnClickBackButton()
    {
        MainFrame.SetActive(true);
        ItemCodeFrame.SetActive(false);
    }
}
