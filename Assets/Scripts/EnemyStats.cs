using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {
    public float health;
    public float speed;
    public float speedMultiplier;

    public float maxHealth = 10;


    EnemyAI enemyAI;

    bool isAlive = true;


    public void OnAttacked(float damage)
    {
        if(isAlive)
        {
            health -= damage;
            if (health <= 0)
            {
                enemyAI.OnDead();
                isAlive = false;
            }
        }

    }

    void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
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
