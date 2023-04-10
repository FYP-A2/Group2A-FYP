using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WildMonster : Monster
{
    public float moveRange;
    public float becomeMoveDistance;
    public float chaseDistance;
    List<Transform> players = new List<Transform>();
    Vector3 originPos;
    public new enum State {Idle, Move, Chase, Attack, Die }
    public State wildState { get;private set; }
    protected override void Start()
    {
        base.Start();
        GameObject[] GOplayers = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject go in GOplayers)
        {
            players.Add(go.GetComponent<Transform>());
        }
        originPos = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        switch(wildState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Move:
                Move(); 
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

    protected override void Move()
    {
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            Vector3 randomPoint = originPos + Random.insideUnitSphere * moveRange;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, moveRange, NavMesh.AllAreas))
                agent.SetDestination(hit.position);
        }
    }

    void StateChange()
    {
        if(wildState == State.Idle)
        {
            foreach (Transform t in players)
            {
                if (Vector3.Distance(transform.position, t.position) < becomeMoveDistance)
                {
                    currentTarget = t;
                    wildState = State.Move;
                    break;
                }
            }
        }

        if (currentTarget != null)
        {
            if (Vector3.Distance(transform.position, currentTarget.position) < chaseDistance)
            {
                wildState = State.Chase;
            }

            if (Vector3.Distance(transform.position, currentTarget.position) < enemyScriptable.attackRange)
            {
                wildState = State.Attack;
            }

            if (Vector3.Distance(originPos, currentTarget.position) > moveRange)
            {
                wildState = State.Idle;
            }
        }
    }
}
