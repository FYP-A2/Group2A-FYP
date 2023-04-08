using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    public float moveSpeed = 3.5f;
    public Transform[] waypoints;
    public int currentWaypointIndex;

    public float stayTime = 3f;    
    public float timer;

    public bool isLastPoint = false;
    public enum Type{ Constant, Random }
    public Type type;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        currentWaypointIndex = 0;
        timer = stayTime;
        SetDestination(waypoints[currentWaypointIndex]);
    }

    void Update()
    {
        switch (type)
        {
            case Type.Constant:
                ConstantMove();
                break;
            case Type.Random:
                RandomMove();
                break;
        }
    }

    void SetDestination(Transform transform)
    {
        agent.SetDestination(transform.position);
    }

    void ConstantMove()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                if (!isLastPoint)
                {
                    currentWaypointIndex++;
                    if (currentWaypointIndex == waypoints.Length - 1)
                    {
                        isLastPoint = true;
                    }
                }
                else
                {
                    currentWaypointIndex--;
                    if (currentWaypointIndex == 0)
                    {
                        isLastPoint = false;
                    }
                }
                SetDestination(waypoints[currentWaypointIndex]);
                timer = stayTime;
            }
        }
    }

    void RandomMove()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                currentWaypointIndex = Random.Range(0, waypoints.Length);
                SetDestination(waypoints[currentWaypointIndex]);
                timer = stayTime;
            }
        }
    }
}