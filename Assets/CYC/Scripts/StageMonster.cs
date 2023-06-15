using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class StageMonster : Monster
{
    public Transform target;
    List<Transform> pathTarget = new List<Transform>();
    float pathTargetUpdateTime = 2;

    [Header("player detect")]
    public SphereCollider playerdetected;
    List<Transform> players = new List<Transform>();
    public GameObject indicator;
    Image questionMark;
    public float detectDistance = 15f;
    public float chaseDistance = 30f;
    public float maxHatred = 3f;
    public float hatred = 0f;
    Vector3 orginPos = Vector3.zero;
    ResourceGroupType resource;
    bool isDropped = false;
    Vector3 result = Vector3.zero;

    [Header("WayPoint")]
    public bool wayPointMode = false;
    public List<Transform> waypoints;
    public int currentWaypointIndex = 0;
    private float range = 2f;

    protected override void Start()
    {
        base.Start();
        playerdetected.radius = detectDistance;
        players = playerdetected.GetComponent<AttackArea>().targets;
        questionMark = indicator.GetNamedChild("Img_QuestionMark").GetComponent<Image>();
        if (target == null)
            target = GameObject.Find("core").GetComponent<Transform>();
        //result = RandomPoint(target.position, 15f);
        PathFinding(target.position);
    }

    protected override void Update()
    {
        base.Update();
        switch (state)
        {
            case State.Idle:
                //Debug.Log(gameObject.name + state.ToString());
                Idle();
                break;
            case State.Move:
                //Debug.Log(gameObject.name + state.ToString());
                Move();
                break;
            case State.Attack:
                //Debug.Log(gameObject.name + state.ToString());
                PathFinding(transform.position);
                Attack(currentTarget);
                break;
            case State.Chase:
                Chase();
                break;
            case State.Die:
                Dead();
                agent.isStopped = true;
                break;
        }
        
        PlayerDetection();

        if (currentTarget != null && currentTarget != target && !hitTargets.Contains(currentTarget) && !players.Contains(currentTarget))
        {
            currentTarget = null;
        }
        
        if(Vector3.Distance(transform.position,target.position) > 300f)
        {
            agent.speed = enemyScriptable.speed * 3;
        }
        else
        {
            agent.speed = enemyScriptable.speed;
        }
    }

    void PlayerDetection()
    {
        if (players.Count != 0)
        {
            if (hatred < maxHatred)
            {
                hatred += Time.deltaTime;
                if (!indicator.GetComponent<Image>().enabled)
                    indicator.GetComponent<Image>().enabled = true;
                if (!indicator.activeSelf)
                {
                    indicator.SetActive(true);
                }
                indicator.GetComponent<Image>().fillAmount = hatred / maxHatred;
            }
            else
            {
                if (Vector3.Distance(transform.position, target.position) < 80f)
                {
                    questionMark.color= Color.yellow;
                    return;
                }
                
                if (state != State.Attack && state != State.Chase)
                {
                    if (orginPos==Vector3.zero)
                    {
                        orginPos = transform.position;
                    }
                    questionMark.color = Color.red;
                    if(indicator.GetComponent<Image>().enabled)
                        indicator.GetComponent<Image>().enabled = false;
                    //Chase();
                    state = State.Chase;
                }
            }
        }
        else
        {
            if (state == State.Chase)
            {
                state = State.Move;
            }

            if (hatred > 0)
            {
                if (!indicator.GetComponent<Image>().enabled)
                    indicator.GetComponent<Image>().enabled = true;
                hatred -=Time.deltaTime;
                questionMark.color = Color.white;
                indicator.GetComponent<Image>().fillAmount = hatred / maxHatred;
            }
            else
            {
                if(indicator.activeSelf)
                {
                    if(orginPos!=Vector3.zero)
                    {
                        orginPos = Vector3.zero;
                    }
                    indicator.SetActive(false);                    
                }                
            }           
        }
    }

    void Chase()
    {
        if (players.Count != 0 && Vector3.Distance(orginPos, players[0].position) < chaseDistance)
        {
            PathFinding(players[0].position);            
            if (Physics.Raycast(firePoint.position, (players[0].position - firePoint.position).normalized, out RaycastHit hit, enemyScriptable.attackRange, layer))
            {
                if (hit.transform == players[0])
                {
                    currentTarget = players[0];
                    lastState = state;
                    state = State.Attack;
                }
            }
        }
        else
        {
            questionMark.color = Color.white;
            PathFinding(transform.position);
            state= State.Move;
        }

    }

    protected override void Move()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            PathFinding(target.position);
        }

        if (currentTarget != target && hitTargets.Count > 0)
        {
            if (hitTargets.Contains(target))
            {
                currentTarget= target;
                lastState = state;
                state = State.Attack;
                return;
            }
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
                if (hit.transform.CompareTag("breakable") && hit.transform.GetComponent<MeshRenderer>().enabled)
                {
                    obstacleInFront = true;
                    currentTarget = hit.transform;
                }               
            }

            if (obstacleInFront && pathTarget.Contains(currentTarget) || currentTarget == target)
            {
                lastState = state;
                state = State.Attack;
            }
            else
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    PathFinding(target.position);
                }
            }
        }
    }

    protected override void Dead()
    {
        base.Dead();

        if (!isDropped)
        {
            int num = Random.Range(2, 6);
            if (num > 2)
            {
                ResourceGroupType.Instance.Add(num, 1);
            }
            else
            {
                ResourceGroupType.Instance.Add(num, 10);
            }
            isDropped = true;
        }
    }

    void PathFinding(Vector3 target)
    {
        if (wayPointMode)
        {
            if (state == State.Chase)
            {
                agent.SetDestination(target);
                return;
            }

            if (isAttacked && currentTarget == this.target)
            {
                agent.SetDestination(transform.position);
                return;
            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if(currentWaypointIndex == waypoints.Count - 1) { return; }
                if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) <= range * 2)
                {
                    if (currentWaypointIndex != waypoints.Count - 1)
                    {
                        currentWaypointIndex++;
                    }                    
                }
                if (currentWaypointIndex == waypoints.Count - 1)
                {
                    Vector3 pos = RandomPoint(waypoints[currentWaypointIndex].position, range);
                    agent.SetDestination(pos); //Don't forget to initiate the first movement.
                    NavMeshPath path = new NavMeshPath();
                    if (NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path))
                    {
                        agent.SetPath(path);
                    }
                    StartCoroutine(Coroutine());
                    IEnumerator Coroutine()
                    {
                        if (path.status == NavMeshPathStatus.PathComplete)
                        {
                            agent.SetPath(path);
                        }
                        yield return null;
                    }
                }
                else
                {
                    agent.SetDestination(RandomPoint(waypoints[currentWaypointIndex].position, range));
                }
                //Debug.Log(waypoints[currentWaypointIndex].name);
            }
            
        }
        else
        {
            agent.SetDestination(target); //Don't forget to initiate the first movement.
            NavMeshPath path = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path))
            {
                agent.SetPath(path);
            }
            StartCoroutine(Coroutine());
            IEnumerator Coroutine()
            {
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    agent.SetPath(path);
                }
                yield return null;
            }
        }
    }

    Vector3 RandomPoint(Vector3 center, float range)
    {     
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        return new Vector3(randomPoint.x,center.y,randomPoint.z);
    }
}
