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
        //���������p�x
        velocity = new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0);
    }

    void Update()
    {
        //�z�[�~���O�֘A
        acceleration = Vector3.zero;
        Vector3 diff = target.position - transform.position;
        acceleration += (diff - velocity * period) * 2f / (period * period);
        //�z�[�~���O�e�̒ǔ��\��
        if (acceleration.magnitude > 10f)
        {
            acceleration = acceleration.normalized * 20f;
        }
        period -= Time.deltaTime;
        velocity += acceleration * Time.deltaTime;

        Invoke("BulletDestroy", 2.5f);
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