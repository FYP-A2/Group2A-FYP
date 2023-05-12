using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour, IDamage, IHP
{
    public float hp = 100;
    public Image bloodScreen;
    public Image blackScreen;
    public float maxDuration = 2f;
    public float delay = 1f;
    float delayTimer;
    float timer;
    Color orginColor;
    Vector3 orginPos;
    float orginHp;
    float duration = 0f;
    public bool isDead = false;
    public bool isRespawning = false;
    public float regenarationDelay = 5f;
    float regenaration = 0;

    // Start is called before the first frame update
    void Start()
    {
        orginColor = bloodScreen.color;
        orginPos = transform.position;
        orginHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (bloodScreen.enabled)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                if (bloodScreen.color.a > 0)
                {
                    bloodScreen.color -= new Color(0, 0, 0, Time.deltaTime);
                }
                else
                {
                    BloodScreenReset();
                }
            }
        }

        if(regenaration > 0)
        {
            regenaration -= Time.deltaTime;
        }
        else if (hp < orginHp)
        {
            hp+=Time.deltaTime;
        }

        if (isDead)
        {
            Dead();
        }
        if(isRespawning)
        {
            Respawn();
        }
    }

    void BloodScreenReset()
    {
        timer = 0;
        bloodScreen.color = orginColor;
        bloodScreen.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            hp -= damage;
            regenaration = regenarationDelay;
            if (!bloodScreen.enabled)
            {
                bloodScreen.enabled = true;
            }
            else
            {
                bloodScreen.color = orginColor;
            }
            if (hp <= 0)
            {
                isDead = true;
            }
        }
    }

    private void Dead()
    {
        if (bloodScreen.enabled)
        {
            BloodScreenReset();
        }
        if (!blackScreen.enabled)
        {
            blackScreen.enabled = true;
        }
        if (duration < maxDuration)
        {
            duration += Time.deltaTime;
            blackScreen.color = new Color(0f, 0f, 0f, Mathf.Abs(duration) / maxDuration);
        }
        else
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= delay)
            {
                delayTimer= 0;
                hp = orginHp;
                transform.position = orginPos;
                isDead = false;
                isRespawning = true;
            }
        }
    }

    private void Respawn()
    {      
        if (duration > 0)
        {
            duration -= Time.deltaTime * 0.3f;
            blackScreen.color = new Color(0f, 0f, 0f, Mathf.Abs(duration) / maxDuration);
        }
        else
        {
            isRespawning = false;
            blackScreen.enabled = false;
        }
    }

    public void GetHP(out float max, out float now)
    {
        max = GetMaxHP();
        now = GetHP();
    }

    public float GetHP()
    {
        return hp;
    }

    public float GetMaxHP()
    {
        return orginHp;
    }
}
