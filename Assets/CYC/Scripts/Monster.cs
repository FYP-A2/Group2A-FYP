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
    float defense, resistance;
    float attackDelay, burntTime, slowTime, reductionTime;
    bool isBurnt, isSlow, isDefenseBreak, isAttacked;
    public SphereCollider sphereCollider;
    public Transform target;
    List<Transform> hitTargets;
    GameObject bulletPrefab;
    public Slider slider;
    [SerializeField]GameObject fireEffect, slowEffect;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Initialization();
        burntTime = slowTime = reductionTime = 0;
        isBurnt = isSlow = isDefenseBreak = false;
        hitTargets = sphereCollider.GetComponent<AttackArea>().targets;
        slider.maxValue = hp;
        slider.value = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null) Move();
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
        hp -= (int)(phyDamage * defense  + magicDamage * resistance);
        if (hp <= 0)
        {
            Dead();
        }
    }

    public void GetBurnt(int phyDamage, int magicDamage, int burntDamage, float burntTime)
    {
        hp -= (int)(phyDamage * defense + magicDamage * resistance);
        if (hp <= 0)
        {
            Dead();
            return;
        }
        if (fireEffect == null)
        {
            fireEffect = Instantiate(enemyScriptable.fireEffect, transform);
            fireEffect.transform.localPosition= Vector3.zero;
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
        hp -= (int)(phyDamage * defense + magicDamage * resistance);
        if (hp <= 0)
        {
            Dead();
            return;
        }
        if (slowEffect == null)
        {
            slowEffect = Instantiate(enemyScriptable.slowEffect, transform);
            slowEffect.transform.localPosition = Vector3.zero;
        }
        agent.speed = enemyScriptable.speed * slowRatio;
        this.slowTime= slowTime;
        isSlow = true;
    }

    public void DefenseReduction(int value, float reductionTime)
    {
        defense = enemyScriptable.defense * value;
        resistance = enemyScriptable.resistance * value;
        this.reductionTime = reductionTime;
        isDefenseBreak = true;
    }
    void Move()
    {
        if(hitTargets.Count>0)
        {
            //Debug.Log("Attack");
            agent.SetDestination(transform.position);
            Attack(hitTargets[0]);           
        }
        else
        {
            //Debug.Log("Move");
            agent.SetDestination(target.position);
        }
    }

    void Attack(Transform target)
    {
        IDamage Idamage = target.GetComponent<IDamage>();
        if (Idamage != null && !isAttacked)
        {
            if (!enemyScriptable.isRanged)
            {
                transform.LookAt(hitTargets[0].position);
                Idamage.TakeDamage(damage);
                isAttacked = true;
                StartCoroutine(ResetAttack(attackDelay));
            }
            else
            {
                Shoot();
                isAttacked = true;
                StartCoroutine(ResetAttack(attackDelay));
            }
        }
    }
    void Dead()
    {
        Destroy(gameObject);
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
        if(isDefenseBreak)
        {
            reductionTime -= Time.deltaTime;
        }
        else
        {
            isDefenseBreak= false;
            defense= enemyScriptable.defense;
            resistance= enemyScriptable.resistance;
        }
    }
    IEnumerator Ignite(int damage)
    {
        while (isBurnt)
        {
            Debug.Log("burnt");
            hp -= damage;
            if (hp <= 0)
            {
                Dead();
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

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            transform.LookAt(hitTargets[0].position);
            bullet.Shoot(transform.forward,damage,gameObject);
        }
    }
}