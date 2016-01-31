using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
    public float health;
    public float speed;
    public float speedMultiplier;

    public float maxHealth = 10;


    EnemyAI enemyAI;

    bool isAlive = true;
    SpriteRenderer spriteRender;

    public void OnAttacked(float damage)
    {
        if(isAlive)
        {
            health -= damage;
            if (health <= 0)
            {
                StopCoroutine("Blink");
                enemyAI.OnDead();
                isAlive = false;
            }
            else
            {
                StartCoroutine(Blink());
            }
            
        }

    }


    IEnumerator Blink()
    {
        Color color = spriteRender.color;
        for(int i=0;i<4;++i)
        {
            color.a = 0;
            spriteRender.color = color;
            yield return new WaitForSeconds(.07f);
            color.a = 1;
            spriteRender.color = color;
            yield return new WaitForSeconds(.07f);
        }
        
    }


    void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        health = maxHealth;
        speedMultiplier = 1.0f;
        isAlive = true;
        EnemyManager.Instance.activeEnemies.Add(this);
    }
    void OnDisable()
    {
        if(EnemyManager.Instance)
        {
            EnemyManager.Instance.activeEnemies.Remove(this);
        }
    }


}
