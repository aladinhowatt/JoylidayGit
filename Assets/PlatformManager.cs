using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject[] PlatformPrefab;

    float counter = 0;
    public float Tick;
    bool isSpawning = true;
    void Start()
    {
        
    }

   public void SetSpawn(bool setSpawn)
    {
        isSpawning = setSpawn;
    }
    void Update()
    {
        if (!isSpawning) return;
        counter += Time.deltaTime;

        if(counter > Tick)
        {
            counter = 0;
            var a = Random.Range(0, 100)%6;
            GameObject tmp;
           
              if(a==0)     tmp = Instantiate(PlatformPrefab[a],new Vector3(12,-1,0),Quaternion.identity);
            if (a == 1) tmp = Instantiate(PlatformPrefab[a], new Vector3(12, 0, 0), Quaternion.identity);

            if (a == 2) tmp = Instantiate(PlatformPrefab[a], new Vector3(12, -1, 0), Quaternion.identity);
            if (a == 3) tmp = Instantiate(PlatformPrefab[a], new Vector3(12, 0, 0), Quaternion.identity);

            if (a == 4) tmp = Instantiate(PlatformPrefab[a], new Vector3(12, -1, 0), Quaternion.identity);
            if (a == 5) tmp = Instantiate(PlatformPrefab[a], new Vector3(12, 0, 0), Quaternion.identity);

        }
    }
}
