using Newtonsoft.Json;
using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static DatabaseConnector2;

public class Weburl : MonoBehaviour
{
    public Text urlText;
    public static string userId;
    const string token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImZkYmJjNmU3LWQ5NmUtNGY0YS1iN2ZjLWFlZmY1MzBjYmUxMyIsInVzZXJfaWQiOiIyT3hNNWJIUXFNcFZzeW9YZVlJcVlwSDJDeXYiLCJleHAiOjE4NDMyMDE2MDV9.Qr954P7B3U3Ff8PWKXmTzGvWmFhpxeGvlz9IXprJaGc";
    DateTime timeStamp;

    public TextMeshProUGUI NameText;
    public Text PointText;
    public UiManager GameManager;
    string refId;

    public delegate void  OnCompleteUpdateBonus();
    public OnCompleteUpdateBonus onCompleteDelegateBonus;

    public delegate void OnSaveItem(ItemCodeData itemData);
    public OnSaveItem onSaveItem;

    public delegate void OnGetUserItem(Dictionary<string,string> itemDatas);
    public OnGetUserItem onGetUserItem;

    public delegate void OnCompleteUseItem();
    public OnCompleteUseItem onCompleteUseItem;

    public GameObject ConnectingObject;
    public GameObject ErrorObject;
    public GameObject NoItemObject;

    bool waiting = false;

    private const string Username = "game";
    private const string Password = "v)C$sMn7g:{6dkej";

    public bool FirstFrame =true;

    void Start()
    {
        if (!FirstFrame) return;
       userId = "2U4cBujerbxGPKNn7N7e5RoV1gS";
        GetUser();
      return;

        var parameters = URLParameter.GetSearchParameters();

       // Debug.Log(URLParameter.Protocol);
       //Debug.Log(URLParameter.Hostname);
       // Debug.Log(URLParameter.Port);
       // Debug.Log(URLParameter.Pathname);
      //  Debug.Log(URLParameter.Search);
      //  Debug.Log(URLParameter.Hash);

        string url = Application.absoluteURL;
      //  Debug.Log("Current URL: " + url);

        string site;
        if (parameters.TryGetValue("user_id", out site))
        {
           // Debug.Log(site);
            userId = site;
            urlText.text = site;

            Invoke("Delay", 2);
           
            // use "site" here
        }
        else
        {
          //  Debug.Log("Notfound");
            // no parameter with name "site" found
        }
    }
    void Delay()
    {
        GetUser();
    }
    internal void UpdateBonus()
    {
        CancelInvoke();
        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjI3ZTM0ZDE0LTAyODktNDE4Mi1hYjU3LTA4ZTU5OTE0ODNlOSIsInVzZXJfaWQiOiIxY3VPeklnV2NCY0pXMHpIQ0Z3SFU2MWxnUUQiLCJleHAiOjE4NDE5MTAwNTd9.IW4X4fvsHcbXxxR7FjcDEW4Yiey6n2Tf2D4AAww5BWM";
        timeStamp = DateTime.Now;
        var bb = timeStamp.ToString("yyyy-MM-dd HH:mm:ss");

       

        waiting = true;
        Invoke("TimeOut", 15);
        ConnectingObject.SetActive(true);
        Debug.Log("qweqewqewqweqw");
        RestClient.Get(new RequestHelper
        {
            Uri = "http://188.166.198.198:5000/bonus_update_step1/" + userId + "?bonus_return=-1",
            Headers = new Dictionary<string, string>
            {
                { "Authorization", token },
                { "x-timestamp", bb },
                { "Content-Type", "application/json" }
            },
        }).Then(res =>
        {
           Debug.Log(res.Text);

            RefCode refCode = JsonConvert.DeserializeObject<RefCode>(res.Text);
          //  Debug.Log(refCode.refcode);

          StartCoroutine (UpdateStep2(refCode.refcode));
            

        }).Catch(err =>
        {
            Debug.Log(err.Message);
          //  ConnectingObject.SetActive(false);
           // ErrorObject.SetActive(true);
        }); 



    }

    IEnumerator  UpdateStep2(string refcode)
    {

        var code = refcode;
        RestClient.Get(new RequestHelper
        {
            Uri = "http://188.166.198.198:5000/bonus_update_step2/" + userId + "?ref=" + refcode,
            Headers = new Dictionary<string, string>
            {
              
            },
        }).Then(res =>
        {
           // Debug.Log(res.Text);

            if (res.Text.Contains("reference_id"))
            {
                waiting = false;
                ConnectingObject.SetActive(false);
                onCompleteDelegateBonus?.Invoke();
                // GameManager.CompleteDecrease();
                StopAllCoroutines();
                return;
            }


        }).Catch(err =>
        {
            // Debug.Log(err.Message);
            //  ConnectingObject.SetActive(false);
            // ErrorObject.SetActive(true);
        });
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(UpdateStep2(code));
    }

    /*
IEnumerator  ResultUpdate()
{
   yield return new WaitForSeconds(1.5f);
   string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjI3ZTM0ZDE0LTAyODktNDE4Mi1hYjU3LTA4ZTU5OTE0ODNlOSIsInVzZXJfaWQiOiIxY3VPeklnV2NCY0pXMHpIQ0Z3SFU2MWxnUUQiLCJleHAiOjE4NDE5MTAwNTd9.IW4X4fvsHcbXxxR7FjcDEW4Yiey6n2Tf2D4AAww5BWM";
   //timeStamp = DateTime.Now;
   var bb = timeStamp.ToString("yyyy-MM-dd HH:mm:ss");

   Debug.Log("iod: " + refId);
   RestClient.Get(new RequestHelper
   {
       Uri = "http://188.166.198.198:5000/bonus_update/" + userId,
       Headers = 
       {
           { "Authorization", token },
           { "x-timestamp", bb },
           { "ref", refId },
           { "x-device-id", "23r6YqQkDH20yp5lvqaL9gW21Hi" }
       },
   }).Then(res =>
   {
       Debug.Log(res.Text);
       if(res.Text.Contains("reference_id"))
       {
           onCompleteDelegateBonus?.Invoke();
          // GameManager.CompleteDecrease();
       }


   }).Catch(err => Debug.Log(err.Message)); ;
}*/
    void TimeOut()
    {
        if(waiting)
        {
            ConnectingObject.SetActive(false);
            ErrorObject.SetActive(true);
        }
    }
    private void GetUser()
    {
        CancelInvoke();
        waiting = true;
        Invoke("TimeOut", 15);
        timeStamp = DateTime.Now;
        var bb = timeStamp.ToString("yyyy-MM-dd HH:mm:ss");
        //Debug.Log(bb);
        RestClient.DefaultRequestHeaders["Authorization"] = "Bearer " + token;
        RestClient.Get(new RequestHelper
        {
            Uri = "http://188.166.198.198:5000/profile/" + userId,
            Headers = new Dictionary<string, string>
            {
                 { "Authorization", token },
                { "x-timestamp", bb }
            },
        }).Then(res =>
        {
            waiting = false;
            //Debug.Log(res.Text);
            UserData person = JsonUtility.FromJson<UserData>(res.Text);
          //  Debug.Log(person.member_id);
            ConnectingObject.SetActive(false);
            string decodedText = Regex.Unescape(person.firstname);
            string decodedText2 = Regex.Unescape(person.lastname);


            //Debug.Log(decodedText); 

            NameText.text = decodedText + " " + decodedText2;
            PointText.text = person.bonus + " Joylicoin";
            // EditorUtility.DisplayDialog("Response", res.Text, "Ok");
        }).Catch(err => {
            //  Debug.Log(err.Message);
            // ConnectingObject.SetActive(false);
            // ErrorObject.SetActive(true);
            GetUser();
        }
        ); 
    }


    [System.Serializable]
    public class UserReward
    {
        public Dictionary<string, string> rewardData;
    }

    public IEnumerator GetItem(int score)
    {

       var rewardItem =  RandomizeRewardItem( DatabaseConnector2.rewardItems);
        Debug.Log(rewardItem.description);
        string url = "https://joyliday-2f073-default-rtdb.asia-southeast1.firebasedatabase.app/" + userId + ".json";

        Dictionary<string, string> userReward = new Dictionary<string, string>();
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonData = request.downloadHandler.text;
               
                Debug.Log("JSON data: " + jsonData);
                try
                {
                    //  UserReward userReward = JsonConvert.DeserializeObject<UserReward>(jsonData);
                    userReward = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
                    if (userReward != null )
                    {
                        // Deserialization successful, process userReward object
                        Debug.Log("Deserialization successful:");
                        foreach (var pair in userReward)
                        {
                            Debug.Log("Key: " + pair.Key);
                            Debug.Log("Value: " + pair.Value);
                        }
                    }
                    else
                    {
                        Debug.LogError("Deserialization failed: UserReward object or rewardData dictionary is null");
                    }
                }
                catch (JsonException ex)
                {
                    Debug.LogError("Error during deserialization: " + ex.Message);
                }
            }
            else
            {
                Debug.LogError("Error getting data: " + request.error);
            }
        }

        // Generate a unique key for the new data entry
        string newKey = Guid.NewGuid().ToString();

        // Construct the JSON data with the new key and description
        string data = "{\"" + newKey + "\":\"" + rewardItem.description + "\"}";

        userReward.Add(newKey, rewardItem.description);
        // Log the data before sending the request
        Debug.Log("Data to send: " + data);

        var userRewardJson = JsonConvert.SerializeObject(userReward);
        Debug.Log(userRewardJson);
        
        // Create a POST request with the data
        using (UnityWebRequest request = UnityWebRequest.Put(url, userRewardJson))
        {

           request.SetRequestHeader("Content-Type", "application/json");

            // Send the request
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("POST request sent successfully");
            }
            else
            {
                Debug.LogError("Error sending POST request: " + request.error);
            }
        }

        /*
        ItemCodeData itemDatass = new ItemCodeData();
        int randomId = UnityEngine.Random.Range(0, 9999);
        itemDatass.rw_id = randomId.ToString();
        itemDatass.rw_isValid = "True";
        string randomValue = UnityEngine.Random.Range(0f, 1f) < 0.7f ? "Reward 1 Token" : "Reward 2 Token";
        itemDatass.rw_ItemDescription = randomValue;
        itemDatass.rw_Score = score.ToString();
        itemDatass.rw_Owner = "DFRAWEFCV!EFSF";


        // ItemCodeDataList itemList = new ItemCodeDataList();
        List<ItemCodeData> itemCodeDataList = new List<ItemCodeData>();
        string jsonString = PlayerPrefs.GetString("ItemCodeDataList");
        if (!string.IsNullOrEmpty(jsonString))
        {
            itemCodeDataList = JsonConvert.DeserializeObject<List<ItemCodeData>>(jsonString);
           // itemList = JsonConvert.SerializeObject(jsonString);
        }
        itemCodeDataList.Add(itemDatass);
      //  itemList.itemCodeDataList.Add(itemDatass);

        string updatedJsonString = JsonConvert.SerializeObject(itemCodeDataList);
        Debug.Log(updatedJsonString);
        PlayerPrefs.SetString("ItemCodeDataList", updatedJsonString);
        PlayerPrefs.Save();


        onSaveItem.Invoke(itemDatass);*/

    }



    RewardItem RandomizeRewardItem(List<RewardItem> originalList)
    {
        // Calculate the total weight based on tier chances
        int totalWeight = 0;
        foreach (var item in originalList)
        {
            totalWeight += GetTierWeight(item.tier);
        }

        // Generate a random value within the total weight
        int randomValue = UnityEngine.Random.Range(0, totalWeight);

        // Find the reward item corresponding to the random value
        int cumulativeWeight = 0;
        foreach (var item in originalList)
        {
            cumulativeWeight += GetTierWeight(item.tier);
            if (randomValue < cumulativeWeight)
            {
                return item;
            }
        }

        // This should never be reached
        return originalList[0];
    }

    int GetTierWeight(int tier)
    {
        // Adjust weights according to tier chances
        switch (tier)
        {
            case 1:
                return 50; // 50% chance
            case 2:
                return 30; // 30% chance
            case 3:
                return 20; // 20% chance
            default:
                return 0;
        }
    }



    public IEnumerator GetUserItem()
    {
        string url = "https://joyliday-2f073-default-rtdb.asia-southeast1.firebasedatabase.app/" + userId + ".json";

        Dictionary<string, string> userReward = new Dictionary<string, string>();
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonData = request.downloadHandler.text;

                Debug.Log("JSON data: " + jsonData);
                try
                {
                    //  UserReward userReward = JsonConvert.DeserializeObject<UserReward>(jsonData);
                    userReward = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
                    if (userReward != null)
                    {
                        // Deserialization successful, process userReward object
                        Debug.Log("Deserialization successful:");
                        foreach (var pair in userReward)
                        {
                            Debug.Log("Key: " + pair.Key);
                            Debug.Log("Value: " + pair.Value);
                        }
                    }
                    else
                    {
                        Debug.LogError("Deserialization failed: UserReward object or rewardData dictionary is null");
                    }
                }
                catch (JsonException ex)
                {
                    Debug.LogError("Error during deserialization: " + ex.Message);
                }
            }
            else
            {
                Debug.LogError("Error getting data: " + request.error);
            }
        }

       

        /*
        List<ItemCodeData> itemCodeDataList = new List<ItemCodeData>();
        string jsonString = PlayerPrefs.GetString("ItemCodeDataList");
        if (!string.IsNullOrEmpty(jsonString))
        {
            itemCodeDataList = JsonConvert.DeserializeObject<List<ItemCodeData>>(jsonString);
            // itemList = JsonConvert.SerializeObject(jsonString);
        }*/
        onGetUserItem.Invoke(userReward);
        


        //////Not rest for now 
        /*
                CancelInvoke();
                waiting = true;
                Invoke("TimeOut", 15);
                UserJson userJson = new UserJson();
                userJson.id = userId;



            var jsonstr = JsonConvert.SerializeObject(userJson);
                ConnectingObject.SetActive(true);

                string auth = System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{Username}:{Password}"));
                RestClient.Get(new RequestHelper
                {
                    Headers = new Dictionary<string, string>
                    {
                         { "Authorization", "Basic "+auth },
                    },
                    Uri = "http://163.47.11.172:5000/rewards_user/" + userId,
                }).Then(res =>
                {
                    waiting = false;
                    ConnectingObject.SetActive(false);
                    //Debug.Log(res.Text);


                    List<ItemCodeData> itemDatass = JsonConvert.DeserializeObject<List<ItemCodeData>>(res.Text);
                  //  Debug.Log(itemDatass.Count);
                    onGetUserItem.Invoke(itemDatass);



                }).Catch(err =>
                {
                    Debug.Log(err.Message);
                    if(err.Message.Contains("404"))
                    {
                        NoItemObject.SetActive(true);
                    }
                    // Debug.Log(err.Message);
                    //  ConnectingObject.SetActive(false);
                    //  ErrorObject.SetActive(true);
                });
        */
    }


    public IEnumerator UseItemCode(string itemId)
    {
        string aa = "https://joyliday-2f073-default-rtdb.asia-southeast1.firebasedatabase.app/" + userId + "/" + itemId + ".json";
        string output = aa.Replace(" ", ""); // Replace white space with empty string
        Debug.Log(output); // Output: "HelloWorld"
        using (UnityWebRequest request = UnityWebRequest.Delete(output))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onCompleteUseItem.Invoke();
                Debug.LogError("Success: " + request.result);
            }
            else
            {
                Debug.LogError("Error getting data: " + request.error);
            }
        }
        /*
        List<ItemCodeData> itemCodeDataList = new List<ItemCodeData>();
        string jsonString = PlayerPrefs.GetString("ItemCodeDataList");
        if (!string.IsNullOrEmpty(jsonString))
        {
            itemCodeDataList = JsonConvert.DeserializeObject<List<ItemCodeData>>(jsonString);
            // itemList = JsonConvert.SerializeObject(jsonString);
        }
        for(int i = 0; i< itemCodeDataList.Count;i++)
        {
            if(itemCodeDataList[i].rw_id.Equals(itemId))
            {
                itemCodeDataList.RemoveAt(i);
                break;
            }
        }

        string updatedJsonString = JsonConvert.SerializeObject(itemCodeDataList);
        Debug.Log(updatedJsonString);
        PlayerPrefs.SetString("ItemCodeDataList", updatedJsonString);
        PlayerPrefs.Save();
        onCompleteUseItem.Invoke();
        */
        ///////REst for now
        /*
                CancelInvoke();
                waiting = true;
                Invoke("TimeOut", 15);
                UserJson userJson = new UserJson();
                userJson.id = userId;



                var jsonstr = JsonConvert.SerializeObject(userJson);
                ConnectingObject.SetActive(true);

                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonstr);
                string auth = System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{Username}:{Password}"));
                RestClient.Delete(new RequestHelper
                {
                    Headers = new Dictionary<string, string>
                    {
                         { "Authorization", "Basic "+auth },
                    },
                    Uri = "http://163.47.11.172:5000/reward/"+itemId,
                    BodyRaw = jsonBytes,
                }).Then(res =>
                {
                    waiting = false;
                    ConnectingObject.SetActive(false);
                  //  Debug.Log(res.Text);
                    onCompleteUseItem.Invoke();
                }).Catch(err =>
                {
                   // Debug.Log(err.Message);
                    // Debug.Log(err.Message);
                    //  ConnectingObject.SetActive(false);
                    //  ErrorObject.SetActive(true);
                });
        */
    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Serializable]
public class UserData
{
    public string birthdate;
    public string channel;
    public int bonus;
    public int coin;
    public string contact_email;
    public string created_date;
    public string firstname;
    public string gender;
    public string language;
    public string lastname;
    public string member_card_no;
    public string member_class;
    public string member_id;
    public string member_point;
    public string mobile;
    public string point_expire_date;
    public string point_expiring;
    public string registered_by;

}

[System.Serializable]
public class UpdateRespond
{
    public string code;
    [JsonProperty("ref")]
    public string refId { get; set; }
}

    [System.Serializable]
public class RequestData
{
    [JsonProperty("ref")]
    public string RefValue { get; set; }
    [JsonProperty("bonus_id")]
    public string bonus_id { get; set; }
    [JsonProperty("branch")]
    public string branch { get; set; }
    [JsonProperty("transaction_date")]
    public string transaction_date{ get; set; }
    [JsonProperty("set_bonus")]
    public int set_bonus { get; set; }
    [JsonProperty("set_point_rate")]
    public int set_point_rate { get; set; }
    [JsonProperty("c_spend")]
    public int c_spend { get; set; }
    [JsonProperty("bonus_return")]
    public int bonus_return { get; set; }
    [JsonProperty("b_employee")]
    public string b_employee { get; set; }
    [JsonProperty("int_spend")]
    public int int_spend { get; set; }
    [JsonProperty("point_adj")]
    public int point_adj { get; set; }
    [JsonProperty("redeem_b")]
    public int redeem_b { get; set; }
    [JsonProperty("discount")]
    public int discount { get; set; }
}

[System.Serializable]
public class GetITemData
{
    public string id { get; set; }
    public string points { get; set; }
}

[System.Serializable]
public class ItemCodeData
{
    public string rw_ItemDescription { get; set; }
    public string rw_Owner { get; set; }
    public string rw_Score { get; set; }
    public string rw_id { get; set; }
    public string rw_isValid { get; set; }
    public string rw_itemCode { get; set; }

}

[System.Serializable]
public class RefCode
{
    public string code { get; set; }
    [JsonProperty("ref")]
    public string refcode { get; set; }
   

}

public class UserJson
{
    public string id { get; set; }

}
    [System.Serializable]
    public class ItemCodeDataList
    {
        public List<ItemCodeData> itemCodeDataList = new List<ItemCodeData>();
    }

}
