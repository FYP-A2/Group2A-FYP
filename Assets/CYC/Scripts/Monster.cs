using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour, IMonster
{
    [SerializeField] EnemyScriptableObject enemyScriptable;
    NavMeshAgent agent;
    public int hp, damage;
    [SerializeField] float defense, resistance;
    float attackDelay, burntTime, slowTime, reductionTime;
    bool isBurnt, isDefenseBreak, isAttacked;
    public bool isSlow { get; private set; }
    public SphereCollider sphereCollider;
    public Transform target;
    List<Transform> hitTargets;
    GameObject bulletPrefab;
    public Slider slider;
    [SerializeField]GameObject fireEffect, slowEffect, toxicEffect;
    public GameObject displayDamage;
    public LayerMask layer;

    Animator animator;
    const string ani_Attack = "Animation_Attack", ani_Move = "Animation_Move", ani_GetHit = "Animation_GetHit", ani_Die = "Animation_Die";
    public Transform firePoint;
    public enum State{Move,Attack,Die};
    public State state { get; private set; }
    Transform currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        if(target== null)
            target = GameObject.Find("Core").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        Initialization();
        burntTime = slowTime = reductionTime = 0;
        isBurnt = isSlow = isDefenseBreak = false;
        hitTargets = sphereCollider.GetComponent<AttackArea>().targets;
        slider.maxValue = hp;
        slider.value = hp;

        animator= GetComponent<Animator>();
        if (firePoint == null)
            firePoint = transform;

        if (target != null)
            state = State.Move;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Move:
                Move();
                break;
            case State.Attack:
                agent.SetDestination(transform.position);
                Attack(currentTarget);
                if (currentTarget == null)
                    state = State.Move;
                break; 
            case State.Die:
                Dead();
                agent.SetDestination(transform.position);
                break;
        }
        //if(target != null) Move();
        if(isBurnt) Burnt();
        if(isSlow) Slow();
        if(isDefenseBreak) DefenseBreak();
        slider.value = hp;
    }

    void Initialization()
    {
        hp = enemyScriptable.hp;
        damage = enemyScriptable.damage;
        defense = enemyScriptable.defense;
        resistance = enemyScriptable.resistance;
        sphereCollider.radius = enemyScriptable.attackRange;
        attackDelay = enemyScriptable.attackDelay;
        if(enemyScriptable.isRanged)
        {
            bulletPrefab = enemyScriptable.bullet;
        }

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
    }

    public void TakeDamage(int phyDamage, int magicDamage)
    {
        int finalDamage = (int)(phyDamage * (1 - defense) + magicDamage * (1 - resistance));
        hp -= finalDamage;
        if(animator!=null)
            animator.SetTrigger(ani_GetHit);
        ShowDamage(finalDamage,Color.black);
        if (hp <= 0)
        {
            state = State.Die;
        }
    }

    public void GetBurnt(int phyDamage, int magicDamage, int burntDamage, float burntTime)
    {
        TakeDamage(phyDamage, magicDamage);
        if (fireEffect == null)
        {
            fireEffect = Instantiate(enemyScriptable.fireEffect, transform);
            fireEffect.transform.localPosition= new Vector3(0,-enemyScriptable.height/2,0);
            fireEffect.transform.localScale *= 1 + enemyScriptable.radius;
        }
        if (!isBurnt)
        {
            isBurnt = true;
            StartCoroutine(Ignite(burntDamage));
        }
        this.burntTime = burntTime;                  
    }

    public void GetSlow(int phyDamage, int magicDamage, float slowRatio, float slowTime)
    {
        TakeDamage(phyDamage, magicDamage);
        if (slowEffect == null)
        {
            slowEffect = Instantiate(enemyScriptable.slowEffect, transform);
            slowEffect.transform.localPosition = new Vector3(0, -enemyScriptable.height / 2, 0);
            slowEffect.transform.localScale *= 1 + enemyScriptable.radius;
        }
        agent.speed = enemyScriptable.speed * (1 - slowRatio);
        this.slowTime= slowTime;
        isSlow = true;
    }

    public void DefenseReduction(float value, float reductionTime)
    {
        defense = enemyScriptable.defense * (1 - value);
        resistance = enemyScriptable.resistance * (1 - value);
        if (toxicEffect == null)
        {
            toxicEffect = Instantiate(enemyScriptable.toxicEffect, transform);
            toxicEffect.transform.localPosition = new Vector3(0, -enemyScriptable.height / 2, 0);
            toxicEffect.transform.localScale *= 1 + enemyScriptable.radius;
        }
        this.reductionTime = reductionTime;
        isDefenseBreak = true;
    }
    void Move()
    {
        if (currentTarget != target && hitTargets.Count > 0)
        {
            Physics.Raycast(firePoint.position, transform.forward, out RaycastHit hit, enemyScriptable.attackRange, layer);
            //Debug.DrawRay(transform.position, transform.forward * enemyScriptable.attackRange, Color.black, 1f);
            if (hit.transform != null && hit.transform.tag == "breakable")
            {
                //Debug.Log("Attack");
                if (hitTargets.Find((x) => x == target) != null)
                {
                    //agent.SetDestination(transform.position);
                    //Attack(target);
                    currentTarget = target;
                    state = State.Attack;
                }
                else
                {                   
                    //agent.SetDestination(transform.position);
                    //Attack(hit.transform);
                    currentTarget = hit.transform;
                    state = State.Attack;
                }
            }
            else
            {
                if (!agent.hasPath && !isAttacked)
                {
                    if (animator != null)
                        animator.SetFloat(ani_Move, agent.velocity.magnitude);
                    agent.SetDestination(target.position);
                }
            }
        }
        else
        {
            //Debug.Log("Move");
            if (!agent.hasPath)
            {
                if (animator != null)
                    animator.SetFloat(ani_Move, agent.velocity.magnitude);
                agent.SetDestination(target.position);
            }
        }
    }
    void Attack(Transform target)
    {
        if (target != null)
        {
            if (target.TryGetComponent<IDamage>(out IDamage Idamage)  && !isAttacked)
            {
                if (!enemyScriptable.isRanged)
                {                    
                    if (animator != null)
                        animator.SetTrigger(ani_Attack);
                    transform.LookAt(target.position);
                    Idamage.TakeDamage(damage);
                    isAttacked = true;                    
                    StartCoroutine(ResetAttack(attackDelay));
                }
                else
                {
                    if (animator != null)
                        animator.SetTrigger(ani_Attack);
                    Shoot(target);
                    isAttacked = true;                   
                    StartCoroutine(ResetAttack(attackDelay));
                }
            }
        }
    }
    void Dead()
    {
        if (animator != null)
            animator.SetBool(ani_Die,true);
        Destroy(gameObject,3f);
    }

    void Burnt()
    {
        if(burntTime+0.5 > 0)
        {
            burntTime -= Time.deltaTime;
        }
        else
        {
            isBurnt= false;
            if (fireEffect != null)
            {
                Destroy(fireEffect);
                fireEffect = null;             
            }
        }    
    }

    void Slow()
    {
        if(slowTime > 0)
        {
            slowTime -= Time.deltaTime;
        }
        else
        {
            isSlow = false;
            agent.speed = enemyScriptable.speed;
            if (slowEffect != null)
            {
                Destroy(slowEffect);
                slowEffect = null;               
            }
        }
    }

    void DefenseBreak()
    {
        if(reductionTime>0)
        {
            reductionTime -= Time.deltaTime;
        }
        else
        {
            isDefenseBreak= false;
            defense= enemyScriptable.defense;
            resistance= enemyScriptable.resistance;
            if (toxicEffect != null)
            {
                Destroy(toxicEffect);
                toxicEffect = null;
            }
        }
    }
    IEnumerator Ignite(int damage)
    {
        while (isBurnt)
        {
            //Debug.Log("burnt");
            hp -= damage;
            ShowDamage(damage, Color.red);
            if (hp <= 0)
            {
                state = State.Die;
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator ResetAttack(float attackDelay)
    {
        while (isAttacked)
        {
            yield return new WaitForSeconds(attackDelay);
            isAttacked = false;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other != null)
    //    {
    //        if (other.transform.tag == "breakable")
    //        {
    //            hitTargets = other.transform;
    //        }
    //    }
    //}

    void Shoot(Transform target)
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            transform.LookAt(target.position);
            bullet.Shoot(transform.forward,damage,gameObject);
        }
    }

    public void ShowDamage(int DamageShow, Color color)
    {
        GameObject x = Instantiate(displayDamage, slider.transform.position, slider.transform.rotation,transform);
        x.GetComponent<TextMove>().text.color = color;
        x.GetComponent<TextMove>().SetDamage(DamageShow);
        Destroy(x, 1f);
    }
}