using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player charManager;
    public Text ScoreText;
    public GameObject OverObject;
    public PlatformManager platform;

    public static int score = 0;

   
     AudioSource audi;

    public AudioClip continueSound;
    public AudioClip overSound;

    public AudioClip getCoinSound;
    public AudioClip getBadSound;

    public Weburl requestObject;

    public RewardManager rewardManager;

    public static string RewardString = "";

    public int targetScorestatge1;
    public int targetScorestatge2;

    int currentstage = 1;
    public GameObject HowtoObject2;
    public Player player;

    public Sprite stage2BgSprite;
    public SpriteRenderer bgRenderer;

    public GameObject LineObject;

    public RectTransform imageTransform; // Reference to the RectTransform of your image
    public float moveDuration = 180.0f; // 3 minutes in seconds

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float currentTime = 0.0f;
    bool isEnd = false;

    public LevelGenerator levelGenerator;

    void Start()
    {


        ScoreText.text = score.ToString();
        audi = GetComponent<AudioSource>();
        if(charManager)
        {
            charManager.OnGetCoin += OnGetCoin;
            charManager.Dead += OnDead;
           // Invoke("Test", 2);
        }

      
      

    }

    void Test()
    {
        rewardManager.ShowRewardFrame(score);
    }
    private void StartCountLine()
    {
        startPosition = imageTransform.anchoredPosition;
        endPosition = new Vector3(356.0f, startPosition.y, startPosition.z);
    }

    private void OnGetCoin(int num)
    {
        score += num;
        if (score < 0)
            score = 0;
        ScoreText.text = score.ToString();

        if (num > 0)
        {
            audi.PlayOneShot(getCoinSound);
        }
        else
        {
            audi.PlayOneShot(getBadSound);
        }
           
    }

    // Update is called once per frame
    void Update()
    {
        if(HowtoObject2)
        {
            if (currentstage == 1 && score >= targetScorestatge1)
            {
                HowtoObject2.SetActive(true);
                player.waitForStart = true;
             
                bgRenderer.sprite = stage2BgSprite;
            }

            if (currentstage == 2 && !isEnd)
            {
                currentTime += Time.deltaTime;

                // Calculate the interpolation value between 0 and 1 based on the current time and duration
                float t = Mathf.Clamp01(currentTime / moveDuration);

                // Interpolate the position using Lerp
                imageTransform.anchoredPosition = Vector3.Lerp(startPosition, endPosition, t);

                // If the movement is complete, you can perform any desired actions here
                if (t >= 1.0f)
                {
                    isEnd = true;
                    player.waitForStart = true;
                    if (score >= targetScorestatge2)
                    {
                        rewardManager.ShowRewardFrame(score);
                    }
                    else
                    {
                        OnDead();
                    }
                }
            }
        }
       
    }

    public void OnClickStartStage2()
    {
        score = 0;
        player.waitForStart = false;
        HowtoObject2.SetActive(false);
        StartCountLine();
        LineObject.SetActive(true);
        currentstage = 2;
          
    }
    private void OnDestroy()
    {
        if(charManager)
        {
            charManager.OnGetCoin -= OnGetCoin;
            requestObject.onCompleteDelegateBonus -= OnUpdateBonus;
        }
      
    }

    private void OnDead()
    {
        if(!isEnd)
        {
            isEnd = true;
            audi.PlayOneShot(overSound);
            charManager.gameObject.SetActive(false);
            //platform.SetSpawn(false);
            OverObject.gameObject.SetActive(true);
        }
        
    }

    private void OnUpdateBonus()
    {
        requestObject.onCompleteDelegateBonus -= OnUpdateBonus;
        audi.PlayOneShot(continueSound);
        OverObject.SetActive(false);
        if(currentstage == 1)
        {
            SceneManager.LoadScene("GameScene_RunnerMapGeneration");
        }else
        {
            isEnd = false;
            imageTransform.anchoredPosition = startPosition;
            currentTime = 0;
            player.Restart();
            levelGenerator.Reset();
        }

        //SceneManager.LoadScene("GameScene_RunnerMapGeneration");
     
      //  charManager.gameObject.SetActive(true);
      // charManager.Restart();
       // platform.SetSpawn(true);
    }

    public void OnClickOver()
    {
        score = 0;
        requestObject.onCompleteDelegateBonus -= OnUpdateBonus;

        SceneManager.LoadScene("SampleScene");
      //  rewardManager.ShowRewardFrame(score);
        //
    }
   

    public void OnClickContinue()
    {
        requestObject.onCompleteDelegateBonus += OnUpdateBonus;
        requestObject.UpdateBonus();
    }

    public void Reload()
    {
        score = 0;
        SceneManager.LoadScene("SampleScene");
    }

}
