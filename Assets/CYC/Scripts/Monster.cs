using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IMonster
{
    [SerializeField] EnemyScriptableObject enemyScriptable;
    NavMeshAgent agent;
    int hp, damage;
    float defense, resistance;
    float attackDelay, burntTime, slowTime, reductionTime;
    bool isBurnt, isSlow, isDefenseBreak, isAttacked;
    public SphereCollider sphereCollider;
    public Transform target, hitTarget = null;
    GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sphereCollider = GetComponent<SphereCollider>();
        Initialization();
        burntTime = slowTime = reductionTime = 0;
        isBurnt = isSlow = isDefenseBreak = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null) Move();
        if(isBurnt) Burnt();
        if(isSlow) Slow();
        if(isDefenseBreak) DefenseBreak();
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
        if (hp < 0)
        {
            Dead();
        }
    }

    public void GetBurnt(int phyDamage, int magicDamage, int burntDamage, float burntTime)
    {
        hp -= (int)(phyDamage * defense + magicDamage * resistance);
        StartCoroutine(Ignite(burntDamage));
        this.burntTime= burntTime;
        isBurnt = true;
    }

    public void GetSlow(int phyDamage, int magicDamage, float slowRatio, float slowTime)
    {
        hp -= (int)(phyDamage * defense + magicDamage * resistance);
        if (hp < 0)
        {
            Dead();
            return;
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
        if(hitTarget == null)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.SetDestination(transform.position);
            Attack(hitTarget);
        }
    }

    void Attack(Transform target)
    {
        IDamage Idamage = target.GetComponent<IDamage>();
        if (Idamage != null && !isAttacked)
        {
            if (!enemyScriptable.isRanged)
            {
                transform.LookAt(hitTarget.position);
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
        if(burntTime > 0)
        {
            burntTime -= Time.deltaTime;
        }
        else
        {
            isBurnt= false;
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
        if (isBurnt)
        {
            hp -= damage;
            yield return new WaitForSeconds(1);
        }
        else
            yield return null;
    }

    IEnumerator ResetAttack(float attackDelay)
    {
        while (isAttacked)
        {
            yield return new WaitForSeconds(attackDelay);
            isAttacked = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if (other.transform.tag == "breakable")
            {
                hitTarget = other.transform;
            }
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            transform.LookAt(hitTarget.position);
            bullet.Shoot(transform.forward,damage,gameObject);
        }
    }
}