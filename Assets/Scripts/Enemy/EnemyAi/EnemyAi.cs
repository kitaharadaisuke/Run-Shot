using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{

    float coolTime;
    float attackTime;

    Transform target;
    GameObject player;
    NavMeshAgent agent;
    Rigidbody rb;
    Transform attackRange;

    public Action action;

    public enum Action
    {
        IDOL = 0,    //何もしていないとき
        WAIT = 1,    //待機
        MOVE = 2,    //動いている時
        ATTACK = 3,  //攻撃
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        coolTime = 0;
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        action = Action.IDOL;
        attackRange = transform.Find("AttackRange");
    }

    void Update()
    {
        target = player.transform;
        switch (action)
        {
            case Action.IDOL:
                action = Action.MOVE;
                break;
            case Action.WAIT:
                attackRange.gameObject.SetActive(false);
                coolTime += Time.deltaTime;
                if (coolTime >= 2)
                {
                    action = Action.MOVE;
                    coolTime = 0;
                }
                break;
            case Action.MOVE:
                attackRange.gameObject.SetActive(true);
                agent.isStopped = false;
                agent.destination = target.position;
                break;
            case Action.ATTACK:
                agent.isStopped = true;
                attackTime += Time.deltaTime;
                if (attackTime >= 2)
                {
                    action = Action.WAIT;
                    attackTime = 0;
                }
                break;
        }
    }
}
