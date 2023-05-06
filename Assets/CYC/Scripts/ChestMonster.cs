using System.Collections.Generic;
using UnityEngine;

public class ChestMonster : Monster
{
    public bool getHit;
    public float chaseDistance;
    List<Transform> players = new List<Transform>();
    Vector3 originPos;
    public new enum State { Idle, Home,Chase, Attack, Die }
    public State chestState { get; private set; }
    const string ani_Wake = "Chest_Wake";

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(originPos, moveRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(originPos, chaseDistance);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(originPos, becomeMoveDistance);
    }
    protected override void Start()
    {
        base.Start();
        GameObject[] GOplayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in GOplayers)
        {
            players.Add(go.GetComponent<Transform>());
        }
        originPos = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        switch (chestState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Home:
                Home();
                break;
            case State.Chase:
                Chase(currentTarget);
                break;
            case State.Attack:
                Attack(currentTarget);
                break;
            case State.Die:
                Dead();
                break;
        }
        StateChange();
    }

    protected override void Idle()
    {
        agent.SetDestination(transform.position);
    }

    void Chase(Transform target)
    {
        agent.SetDestination(target.position);
    }

    void Home()
    {
        if(transform.position != originPos)
        {
            agent.SetDestination(originPos);
        }
        else
        {
            animator.SetBool(ani_Wake, false);
            chestState= State.Idle;
        }
    }
    void StateChange()
    {
        if (chestState == State.Idle && getHit)
        {           
            float shortest = 0;
            for(int i=0;i<players.Count;i++)
            {
                float distance = Vector3.Distance(transform.position, players[i].transform.position);
                if (i == 0 || distance < shortest)
                {
                    shortest = distance;
                    currentTarget = players[i];
                }
            }
            chestState = State.Chase;
        }

        if (currentTarget != null)
        {
            if (Vector3.Distance(transform.position, currentTarget.position) < enemyScriptable.attackRange)
            {
                lastState = state;
                chestState = State.Attack;
            }
            else if (chestState == State.Attack)
                chestState = State.Chase;

            if (Vector3.Distance(originPos, currentTarget.position) > chaseDistance)
            {
                currentTarget = null;               
                getHit = false;
                chestState = State.Home;
            }
        }
    }

    public override void TakeDamage(int phyDamage, int magicDamage)
    {        
        base.TakeDamage(phyDamage, magicDamage);
        getHit = true;
        animator.SetBool(ani_Wake, true);
    }

    protected override void Dead()
    {
        base.Dead();
        //Drop
    }
}
