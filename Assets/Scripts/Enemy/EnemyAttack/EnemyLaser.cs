using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    GameObject playerObj;
    Rigidbody rb;
    Vector3 velocity;
    Vector3 position;

    public Vector3 acceleration;
    Transform target;

    float period = 2f;

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        target = playerObj.transform;
        position = transform.position;
        rb = this.GetComponent<Rigidbody>();
        //撃ちだし角度
        velocity = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0);
    }

    void Update()
    {
        //ホーミング関連
        acceleration = Vector3.zero;
        Vector3 diff = target.position - transform.position;
        acceleration += (diff - velocity * period) * 2f / (period * period);
        //ホーミング弾の追尾能力
        if (acceleration.magnitude > 10f)
        {
            acceleration = acceleration.normalized * 10f;
        }
        period -= Time.deltaTime;
        velocity += acceleration * Time.deltaTime;

        Invoke("BulletDestroy", 4.0f);
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            BulletDestroy();
        }
    }

    void BulletDestroy()
    {
        Destroy(this.gameObject);
    }
}
