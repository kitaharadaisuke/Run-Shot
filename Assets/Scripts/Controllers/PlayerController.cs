using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerData player;
    [SerializeField] Slider hpBar;
    [SerializeField] float upForce = 0f;

    GameInput gameInput;
    Rigidbody rb;
    Vector2 moveInput;

    int speed;
    int hp;
    int jumpCount;
    float stamina;
    float bg;
    float inputH;
    float inputV;

    private void Awake() => gameInput = new GameInput();
    private void OnEnable() => gameInput.Enable();
    private void OnDisable() => gameInput.Disable();
    private void OnDestroy() => gameInput.Dispose();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = player.Speed;
        hp = player.MaxHP;
        stamina = player.Stamina;
        bg = player.BG;
    }

    void Update()
    {
        //hpo[
        hpBar.value = hp;
        moveInput = gameInput.Player.Move.ReadValue<Vector2>();

        Vector3 cameraFoward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraFoward * moveInput.y + Camera.main.transform.right * moveInput.x;
        rb.velocity = moveForward * speed + new Vector3(0, rb.velocity.y, 0);

        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveForward), 0.1f);
        }
        //_bV
        if (gameInput.Player.Dash.triggered)
        {
            speed = speed * 2;
        }
        else if (gameInput.Player.UnDash.triggered)
        {
            speed = speed / 2;
        }
        //Wv
        if (jumpCount <= 1)
        {
            if (gameInput.Player.Jump.triggered)
            {
                jumpCount++;
                rb.AddForce(Vector3.up * upForce);
            }
        }
        //ñð
        if (gameInput.Player.Avoid.triggered)
        {
            StartCoroutine("AvoidCoroutine");
            stamina -= 10;
        }
        //ÊíU
        if (gameInput.Player.NormalAttack.triggered)
        {
            bg -= 2;
        }

        //X^~iÁï
        if (speed == 2)
        {
            if (stamina >= 0)
            {
                stamina -= 0.1f;
            }
            //r[Q[Wñ
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

    private void OnCollisionEnter(Collision collision)
    {
        //ßÚG
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hp -= 10;
            StartCoroutine("DamageCoroutine");
        }
        //£G
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            hp -= 5;
            StartCoroutine("DamageCoroutine");
        }

        //Ún»è
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }


    //_[Wã³GÔ
    IEnumerator DamageCoroutine()
    {
        this.gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        yield return new WaitForSeconds(0.5f);
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    //ñð³GÔ
    IEnumerator AvoidCoroutine()
    {
        this.gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        yield return new WaitForSeconds(2.0f);
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
