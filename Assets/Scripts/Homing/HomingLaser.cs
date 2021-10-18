using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingLaser : MonoBehaviour
{
    GameObject enemyObj;
    Rigidbody rb;
    Vector3 velocity;
    Vector3 position;

    public Vector3 acceleration;
    Transform target;

    float period = 2f;

    void Start()
    {
        enemyObj = GameObject.FindGameObjectWithTag("Enemy");
        target = enemyObj.transform;
        position = transform.position;
        rb = this.GetComponent<Rigidbody>();
        velocity = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-3.0f, 3.0f), 0);
    }

    void Update()
    {
        acceleration = Vector3.zero;
        Vector3 diff = target.position - transform.position;
        acceleration += (diff - velocity * period) * 2f / (period * period);
        if (acceleration.magnitude > 100f)
        {
            acceleration = acceleration.normalized * 100f;
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
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }

    void BulletDestroy()
    {
        Destroy(this.gameObject);
    }
}
