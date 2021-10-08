using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 0f;
    [SerializeField] float upForce = 0f;

    Rigidbody rb;

    [SerializeField] int speedCount;
    float inputH;
    float inputV;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");

        //プレイヤー移動
        float x = inputH * speed;
        float z = inputV * speed;
        rb.MovePosition(rb.position + new Vector3(x, 0, z));

        //ダッシュ
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * 2;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = speed / 2;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ジャンプ
            rb.AddForce(Vector3.up * upForce);
        }
    }
}
