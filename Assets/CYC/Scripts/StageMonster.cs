using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StageMonster : Monster
{
    public Transform target;
    
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
                Idle();
                break;
            case State.Move:
                Move();
                break;
            case State.Attack:
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
        if (currentTarget != target && hitTargets.Count > 0)
        {
            //bool targetIsBlocked = false;
            bool obstacleInFront = false;
            List<Transform> pathTarget = new List<Transform>();

            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(target.position, path))
            {
                for (int i = 1; i < path.corners.Length; i++)
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
                agent.SetDestination(transform.position);
                state = State.Attack;
            }
            else
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (animator != null)
                        animator.SetFloat(ani_Move, agent.velocity.magnitude);
                    agent.SetDestination(target.position);
                }
            }
        }
        else
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (animator != null)
                    animator.SetFloat(ani_Move, agent.velocity.magnitude);
                agent.SetDestination(target.position);
            }
        }
    }
}
