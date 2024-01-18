using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour
{
    public GameObject ContinueBut;
    public GameObject EndBut;
    private void OnEnable()
    {
        Invoke("Delayy", 1f);
    }
   void Delayy()
    {
        ContinueBut.SetActive(true);
        EndBut.SetActive(true);
    }

    private void OnDisable()
    {
        ContinueBut.SetActive(false);
        EndBut.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
