using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer sp;

    public float Speed;
    float width = 46.4f;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        width += Speed * Time.deltaTime;
        sp.size = new Vector2(width, 10.6f);
    }
}
