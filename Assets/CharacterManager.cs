using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    Rigidbody2D ridgid;
    Animator anim;
    public float Power;
    bool Canpress = true;
    bool isJump = false;

    public GameObject normalColliderObject;
    public GameObject slideColliderObject;

    public delegate void OnGetCoinDelegate(int num);

    public OnGetCoinDelegate OnGetCoin;
  

    public Action Dead;
    bool isdead = false;
    Vector3 startPos;
    void Start()
    {
        ridgid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startPos = transform.position;
    }

    public void Slide()
    {
        if (Canpress)
        {
            slideColliderObject.SetActive(true);
            normalColliderObject.SetActive(false);

            anim.SetBool("IsSlide", true);
            Invoke("UP", .5f);
            Canpress = false;
        }


    }

    public void Jump()
    {
        if (!isJump)
        {
            isJump = true;
            anim.SetBool("IsJump", true);
            ridgid.AddForce(Vector2.up * Power, ForceMode2D.Impulse);
            // Invoke("UP", .5f);
            Canpress = false;
        }


    }
    private void ResetJump()
    {
        isJump = false;
        Canpress = true;
        anim.SetBool("IsJump", false);

    }
    void UP()
    {
        slideColliderObject.SetActive(false);
        normalColliderObject.SetActive(true);
        Canpress = true;
        anim.SetBool("IsSlide", false);
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -9.8f)
        {
            
            Dead.Invoke();
        }
         
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            ResetJump();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            collision.gameObject.SetActive(false);
            OnGetCoin?.Invoke(1);
        }

        if (collision.tag == "Big")
        {
            collision.gameObject.SetActive(false);
            OnGetCoin?.Invoke(5);
        }
        if (collision.tag == "Bad")
        {
            collision.gameObject.SetActive(false);
            OnGetCoin?.Invoke(-3);
        }
    }

    public void Restart()
    {
        Debug.Log(startPos);
        transform.position = startPos;
      //  gameObject.SetActive(true);
    }


}




