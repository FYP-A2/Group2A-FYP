using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityStandardAssets.Utility.TimedObjectActivator;

public class Monster : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    [SerializeField] EnemyScriptableObject enemyScriptable;
    public Slider slider;
    public int hp = 100;
    int damage, fireTime;
    float freezeTime;
    float attackDelay = 2f, attackRange;
    bool attacked, inAttackRange, onFire, onFreeze;
    Animator animator;
    //Drop drop;
    public ParticleSystem fireEffect,freezeEffect;
    public SpawnManager spawn;

    public Transform core;
    public List<Transform> gate = new List<Transform>();
    public Vector3 targetPos;
    public Transform target;

    private void Awake()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //drop = GetComponent<Drop>();
        //animator = GetComponent<Animator>();
        spawn = GameObject.Find("Spawn").GetComponent<SpawnManager>();
        AgentConfig();
        //if (spawn.boostCount > 0)
        //    hp = (int)(enemyScriptable.health * spawn.boost);
        //else
            hp = enemyScriptable.health;
        damage = enemyScriptable.damage;
        this.slider.minValue = 0;
        this.slider.maxValue= hp;
        this.slider.value = slider.maxValue;
        attackRange = enemyScriptable.radius * 2;
        //animator.speed = enemyScriptable.animSpeed;
        //animator.Rebind();                  
    }

    // Update is called once per frame
    void Update()
    {
        if (1==1)
        {
            if (hp <= 0f)
            {
                Dead();
            }
            else
            {
                if (target != null)
                {
                    inAttackRange = Vector3.Distance(transform.position, targetPos) < attackRange;
                    //Debug.Log(inAttackRange);
                    if (inAttackRange)
                    {
                        agent.SetDestination(transform.position);
                        transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
                        if (animator != null)
                            animator.SetBool("Move", false);
                        Attack();
                    }
                    else
                    {
                        Move();
                    }
                }                
            }
        }
        else
            agent.SetDestination(transform.position);

        OnFreeze();
    }

    void Move()
    {
        //Debug.Log("move");
        //agent.SetDestination(core.position);
        if (target != null)
        {
            agent.SetDestination(targetPos);
        }
        if (animator != null)
        {
            animator.SetBool("Move", true);
        }
    }
    void Attack()
    {       
        if (!attacked)
        {
            if (animator != null)
                animator.SetTrigger("Attack");  
            //Debug.Log(this.name + "Hit" + target.name);
            //player.GetComponent<Player>().TakeDamage(damage);
            target.GetComponent<Test>().getHit(damage);
            attacked = true;
            Invoke("ResetAttack", attackDelay);
        }
        else
        {
            if (animator != null)
              animator.SetBool("Move", false);
        }
    }

    void ResetAttack()
    {
        attacked = false;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        this.slider.value = hp;
    }

    void Dead()
    {
        if (this.gameObject.name.Equals("Boss"))
        {
            this.gameObject.SetActive(false);
            this.Start();
            //drop.DropLoot();
        }
        else
        {
            Destroy(this.gameObject);
            //drop.DropLoot();
        }
        
    }

    void AgentConfig()
    {
        agent.baseOffset = enemyScriptable.baseOffset;
        agent.speed = enemyScriptable.speed;
        agent.angularSpeed = enemyScriptable.angularSpeed;
        agent.acceleration = enemyScriptable.acceleration;
        agent.stoppingDistance = enemyScriptable.stoppingDistance;
        agent.radius = enemyScriptable.radius;
        agent.height = enemyScriptable.height;    
        agent.obstacleAvoidanceType = enemyScriptable.obstacleAvoidance;
        agent.avoidancePriority = enemyScriptable.avoidancePriority;
        agent.areaMask = enemyScriptable.areaMask;
        //agent.velocity = enemyScriptable.velocity;
    }

    public void TakeFire(int dmg)
    {
        hp -= dmg;
        this.slider.value = hp;
        if (!onFire)
        {
            fireTime = 0;
            InvokeRepeating("OnFire", 1f, 1f);
            fireEffect.Play();
            onFire= true;
        }
    }

    void OnFire()
    {
        fireTime++;
        hp -= 25;
        this.slider.value = hp;
        if (fireTime == 3 || hp <= 0)
        {
            Debug.Log("stop");
            CancelInvoke("OnFire");
            fireEffect.Stop();
            onFire= false;
        }
    }

    public void TakeFreeze(int dmg)
    {
        hp -= dmg;
        this.slider.value = hp;
        if (!onFreeze)
        {
            freezeEffect.Play();
            onFreeze = true;
            freezeTime = 0;
        }
    }

    void OnFreeze()
    {
        if (onFreeze)
        {
            freezeTime += Time.deltaTime;           
            if(freezeTime < 5)
            {              
                agent.speed = enemyScriptable.speed / 2;               
            }
            else
            {
                freezeEffect.Stop();
                agent.speed = enemyScriptable.speed;
                onFreeze = false;
            }
        }       
    }

    void UpdateTarget()
    {
        gate = new List<Transform>();
        core = GameObject.Find("Core").GetComponent<Transform>();
        Transform[] gates = GameObject.Find("Gate").transform.Cast<Transform>().ToArray();
        foreach (Transform g in gates)
            if (g.gameObject.activeInHierarchy)
                gate.Add(g);
        if (gate != null)
        {
            float shortestDistance = Vector3.Distance(transform.position, core.position);
            Vector3 nearestTargetPos = GroundPosition(core.position);
            Transform nearestTarget = core;
            foreach (Transform g in gate)
            {
                float distanceToTarget = Vector3.Distance(transform.position, g.position);
                if (distanceToTarget < shortestDistance)
                {
                    shortestDistance = distanceToTarget;
                    nearestTargetPos = g.position;
                    nearestTarget = g;
                }
            }
            if (nearestTargetPos != null)
            {
                target = nearestTarget;
                targetPos = nearestTargetPos;
            }
        }
        else
        {
            target = core;
            targetPos = GroundPosition(core.position);
        }
    }

    Vector3 GroundPosition(Vector3 pos)
    {
        RaycastHit hit;

        Physics.Raycast(pos, Vector3.down, out hit);

        return hit.point;
    }
}
