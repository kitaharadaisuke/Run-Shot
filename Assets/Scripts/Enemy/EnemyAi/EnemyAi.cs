using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    Animator anm;

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
        IDOL = 0,    //âΩÇ‡ÇµÇƒÇ¢Ç»Ç¢Ç∆Ç´
        WAIT = 1,    //ë“ã@
        MOVE = 2,    //ìÆÇ¢ÇƒÇ¢ÇÈéû
        ATTACK = 3,  //çUåÇ
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        coolTime = 0;
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anm = GetComponent<Animator>();
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
                anm.SetBool("Idol", true);
                anm.SetBool("Walk", false);
                anm.SetBool("Attack", false);
                break;
            case Action.WAIT:
                attackRange.gameObject.SetActive(false);
                coolTime += Time.deltaTime;
                if (coolTime >= 2)
                {
                    action = Action.MOVE;
                    coolTime = 0;
                }
                anm.SetBool("Idol", true);
                anm.SetBool("Walk", false);
                anm.SetBool("Attack", false);
                break;
            case Action.MOVE:
                attackRange.gameObject.SetActive(true);
                agent.isStopped = false;
                agent.destination = target.position;
                anm.SetBool("Walk", true);
                anm.SetBool("Idol", false);
                anm.SetBool("Attack", false);
                break;
            case Action.ATTACK:
                agent.isStopped = true;
                attackTime += Time.deltaTime;
                if (attackTime >= 2)
                {
                    action = Action.WAIT;
                    attackTime = 0;
                }
                anm.SetBool("Attack", true);
                anm.SetBool("Walk", false);
                anm.SetBool("Idol", false);
                break;
        }
    }
}
