using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Unity.Netcode;

public class Monster : NetworkBehaviour, IMonster
{
    [SerializeField] EnemyScriptableObject enemyScriptable;
    NavMeshAgent agent;
    public NetworkVariable<int> hp;
    public int damage;
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
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if(target== null)
            target = GameObject.Find("Core").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        if (IsServer)
        {
            Initialization();
        }
        burntTime = slowTime = reductionTime = 0;
        isBurnt = isSlow = isDefenseBreak = false;
        hitTargets = sphereCollider.GetComponent<AttackArea>().targets;
        slider.maxValue = hp.Value;
        slider.value = hp.Value;
        Debug.Log(NetworkObject.GetInstanceID() +":" +hp.Value);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) Move();
        if(isBurnt) Burnt();
        if(isSlow) Slow();
        if(isDefenseBreak) DefenseBreak();
        slider.value = hp.Value;
    }

    void Initialization()
    {
        hp.Value = enemyScriptable.hp;
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
        if (!IsServer) return;
        int finalDamage = (int)(phyDamage * (1 - defense) + magicDamage * (1 - resistance));
        hp.Value -= finalDamage;
        ShowDamage(finalDamage,Color.black);
        Debug.Log(NetworkObject.GetInstanceID() + ":" + hp.Value);
        if (hp.Value <= 0)
        {
            Dead();
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
        if (!IsServer) return;
        if (hitTargets.Count>0)
        {
            //Debug.Log("Attack");
            agent.SetDestination(transform.position);
            Attack(hitTargets[0]);           
        }
        else
        {
            //Debug.Log("Move");
            if(!agent.hasPath)
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
        GetComponent<NetworkObject>().Despawn();
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
        if (!IsServer) yield return null;
        while (isBurnt)
        {
            //Debug.Log("burnt");
            hp.Value -= damage;
            ShowDamage(damage, Color.red);
            if (hp.Value <= 0)
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

    public void ShowDamage(int DamageShow, Color color)
    {
        GameObject x = Instantiate(displayDamage, slider.transform.position, slider.transform.rotation,transform);
        x.GetComponent<TextMove>().text.color = color;
        x.GetComponent<TextMove>().SetDamage(DamageShow);
        Destroy(x, 1f);
    }
}