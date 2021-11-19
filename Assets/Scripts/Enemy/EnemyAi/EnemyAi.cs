using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] Transform target;

    float coolTime;
    float attackTime;

    NavMeshAgent agent;
    Rigidbody rb;
    Transform attackRange;

    public Action action;

    public enum Action
    {
        IDOL = 0,    //�������Ă��Ȃ��Ƃ�
        WAIT = 1,    //�ҋ@
        MOVE = 2,    //�����Ă��鎞
        ATTACK = 3,  //�U��
    }

    void Start()
    {
        coolTime = 0;
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        action = Action.IDOL;
        attackRange = transform.Find("AttackRange");
    }

    void Update()
    {
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