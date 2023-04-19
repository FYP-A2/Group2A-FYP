using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StageMonster : Monster
{
    public Transform target;
    List<Transform> pathTarget = new List<Transform>();
    float pathTargetUpdateTime = 2;
    protected override void Start()
    {
        base.Start();
        if (target == null)
            target = GameObject.Find("Core").GetComponent<Transform>();
    }

    protected override void Update()
    {
        base.Update();
        switch (state)
        {
            case State.Idle:
                Debug.Log(gameObject.name + state.ToString());
                Idle();
                break;
            case State.Move:
                Debug.Log(gameObject.name + state.ToString());
                Move();
                break;
            case State.Attack:
                Debug.Log(gameObject.name + state.ToString());
                agent.SetDestination(transform.position);
                Attack(currentTarget);
                break;
            case State.Die:
                Dead();
                agent.isStopped = true;
                break;
        }
    }

    protected override void Move()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(target.position);
        }

        if (currentTarget != target && hitTargets.Count > 0)
        {
            //bool targetIsBlocked = false;
            bool obstacleInFront = false;            

            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(target.position, path))
            {
                int count = 0;
                if (path.corners.Length > 2)
                {
                    count = 2;
                }
                else
                    count = path.corners.Length;

                for (int i = 1; i < count; i++)
                {
                    if (Physics.Linecast(path.corners[i - 1], path.corners[i], out RaycastHit hit1, layer) &&
                        hit1.transform.CompareTag("breakable"))
                    {
                        //targetIsBlocked = true;
                        if (!pathTarget.Contains(hit1.transform))
                            pathTarget.Add(hit1.transform);
                        break;
                    }
                }
            }

            if (Physics.Raycast(firePoint.position, transform.forward, out RaycastHit hit, enemyScriptable.attackRange, layer))
            {
                if (hit.transform.CompareTag("breakable"))
                {
                    obstacleInFront = true;
                    currentTarget = hit.transform;
                }
            }

            if (obstacleInFront && pathTarget.Contains(currentTarget) || currentTarget == target)
            {               
                state = State.Attack;
            }
            else
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    agent.SetDestination(target.position);
                }
            }
        }
    }

    protected override void Dead()
    {
        base.Dead();
        //Drop
    }
}
