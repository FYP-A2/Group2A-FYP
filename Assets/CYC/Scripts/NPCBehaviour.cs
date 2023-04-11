using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public enum Type { InOrder, Random, Standing }
    public Type type;

    public float talkDistance = 2f;
    public float textBubbleTime = 5f;
    public List<string> talkTxt;
    public TMP_Text text;

    Animator animator;
    const string ani_Move = "Animation_Move";
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = moveSpeed;
        currentWaypointIndex = 0;
        timer = stayTime;
        //SetDestination(waypoints[currentWaypointIndex]);
    }

    void Update()
    {
        switch (type)
        {
            case Type.InOrder:
                InOrderMove();
                break;
            case Type.Random:
                RandomMove();
                break;
            case Type.Standing:
                Standing();
                break;
        }
        if (animator != null)
            animator.SetFloat(ani_Move, agent.velocity.magnitude);
        Talk();
    }

    void SetDestination(Transform transform)
    {
        agent.SetDestination(transform.position);
    }

    void InOrderMove()
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

    void Standing()
    {

    }

    void Talk()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, talkDistance);
        foreach(Collider c in colliders)
        {
            if (text!= null && c.tag == "Player" && !text.IsActive())
            {
                text.text = talkTxt[Random.Range(0,talkTxt.Count)];
                text.enabled = true;
                StartCoroutine(CloseTextBubble());
                break;
            }
        }
    }

    private IEnumerator CloseTextBubble()
    {
        yield return new WaitForSeconds(textBubbleTime);
        text.enabled = false;
    }
}