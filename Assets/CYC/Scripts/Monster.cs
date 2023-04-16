using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Monster : MonoBehaviour, IMonster
{
    [SerializeField] protected EnemyScriptableObject enemyScriptable;
    protected NavMeshAgent agent;
    public int hp, damage;
    [SerializeField] float defense, resistance;
    float attackDelay, burntTime, slowTime, reductionTime;
    bool isBurnt, isDefenseBreak, isAttacked;
    public bool isSlow { get; private set; }
    public SphereCollider sphereCollider;
    protected List<Transform> hitTargets;
    GameObject bulletPrefab;
    public Slider slider;
    //[SerializeField]
    GameObject fireEffect, slowEffect, toxicEffect;
    public GameObject displayDamage;
    public LayerMask layer;

    protected Animator animator;
    protected const string ani_Attack = "Animation_Attack", ani_Move = "Animation_Move", ani_GetHit = "Animation_GetHit", ani_Die = "Animation_Die";
    public Transform firePoint;

    public enum State { Idle, Move, Attack, Die };
    public State state { get; protected set; }

    protected Transform currentTarget;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Initialization();
        burntTime = slowTime = reductionTime = 0;
        isBurnt = isSlow = isDefenseBreak = false;
        hitTargets = sphereCollider.GetComponent<AttackArea>().targets;
        slider.maxValue = hp;
        slider.value = hp;

        animator = GetComponent<Animator>();
        if (firePoint == null)
            firePoint = transform;

        state = State.Idle;

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isBurnt) Burnt();
        if (isSlow) Slow();
        if (isDefenseBreak) DefenseBreak();
        slider.value = hp;
        if (animator != null)
            animator.SetFloat(ani_Move, agent.velocity.magnitude);
    }

    protected virtual void Idle()
    {
        if (!agent.pathPending)
            state = State.Move;
    }

    protected void Initialization()
    {
        hp = enemyScriptable.hp;
        damage = enemyScriptable.damage;
        defense = enemyScriptable.defense;
        resistance = enemyScriptable.resistance;
        sphereCollider.radius = enemyScriptable.attackRange;
        attackDelay = enemyScriptable.attackDelay;
        if (enemyScriptable.isRanged)
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

    public virtual void TakeDamage(int phyDamage, int magicDamage)
    {
        int finalDamage = (int)(phyDamage * (1 - defense) + magicDamage * (1 - resistance));
        hp -= finalDamage;
        if (animator != null)
            animator.SetTrigger(ani_GetHit);
        ShowDamage(finalDamage, Color.black);
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
            fireEffect.transform.localPosition = new Vector3(0, -enemyScriptable.height / 2, 0);
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
        this.slowTime = slowTime;
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
    protected virtual void Move()
    {

    }
    protected virtual void Attack(Transform target)
    {
        if (currentTarget == null)
            state = State.Move;

        if (target != null)
        {
            if (target.TryGetComponent<IDamage>(out IDamage Idamage) && !isAttacked)
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
    protected virtual void Dead()
    {
        if (animator != null)
            animator.SetBool(ani_Die, true);
        Destroy(gameObject, 3f);
    }

    void Burnt()
    {
        if (burntTime + 0.5 > 0)
        {
            burntTime -= Time.deltaTime;
        }
        else
        {
            isBurnt = false;
            if (fireEffect != null)
            {
                Destroy(fireEffect);
                fireEffect = null;
            }
        }
    }

    void Slow()
    {
        if (slowTime > 0)
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
        if (reductionTime > 0)
        {
            reductionTime -= Time.deltaTime;
        }
        else
        {
            isDefenseBreak = false;
            defense = enemyScriptable.defense;
            resistance = enemyScriptable.resistance;
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

    protected virtual void Shoot(Transform target)
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            transform.LookAt(target.position);
            //bullet.Shoot(transform.forward, damage, gameObject);
            bullet.Shoot((target.position - firePoint.position).normalized, damage, gameObject);
        }
    }

    public void ShowDamage(int DamageShow, Color color)
    {
        GameObject x = Instantiate(displayDamage, slider.transform.position, slider.transform.rotation, transform);
        x.GetComponent<TextMove>().text.color = color;
        x.GetComponent<TextMove>().SetDamage(DamageShow);
        Destroy(x, 1f);
    }
}