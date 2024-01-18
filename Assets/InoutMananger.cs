using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InoutMananger : MonoBehaviour
{
    public CharacterManager characterManaeger;
   
    void Start()
    {
        
    }

    public void OnClickJump()
    {
        characterManaeger.Jump();
    }

    public void OnClickSlide()
    {
        characterManaeger.Slide();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            characterManaeger.Slide();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            characterManaeger.Jump();
        }
    }
}
