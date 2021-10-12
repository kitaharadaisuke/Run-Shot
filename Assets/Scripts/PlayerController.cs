using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerData player;
    [SerializeField] float upForce = 0f;

    Rigidbody rb;

    int speed;
    float stamina;
    float bg;
    float inputH;
    float inputV;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = player.Speed;
        stamina = player.Stamina;
        bg = player.BG;
    }

    void Update()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");

        //プレイヤー移動
        float x = inputH * speed / 10;
        float z = inputV * speed / 10;
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            bg -= 2;
        }

        //スタミナ消費
        if (speed == 2)
        {
            if (stamina >= 0)
            {
                stamina -= 0.1f;
            }
            //ビームゲージ回復
            if (bg < 100)
            {
                bg += 0.1f;
            }
        }
        else
        {
            if (stamina <= 100)
            {
                stamina += 0.1f;
            }
        }
    }
}
