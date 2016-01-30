using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour {

    [SerializeField] int level = 1;
    [SerializeField] int enemiesPerRound = 100;

    [SerializeField] Text enemyCounter;
    [SerializeField] EnemySpawner enemySpawner;

    private static LevelManager instance;
    private int remainingEnemies;
    private bool completingLevel;

    public static void RemoveEnemy()
    {
        if (!instance.completingLevel)
        {
            if (instance.remainingEnemies > 0)
                instance.remainingEnemies -= 1;
            else
            {
                instance.completingLevel = true;
                for (int i = 0; i < EnemyManager.Instance.activeEnemies.Count; ++i)
                {
                    EnemyStats enemy = EnemyManager.Instance.activeEnemies[i];
                    enemy.OnAttacked(9999);
                }

                instance.level++;
                instance.completingLevel = false;
                instance.remainingEnemies = instance.enemiesPerRound;
            }
            instance.UpdateCounter();
        }
    }

	// Use this for initialization
	void Start () {
        instance = this;
        remainingEnemies = enemiesPerRound;
        UpdateCounter();
	}

    private void UpdateCounter(){
        enemyCounter.text = "×" + remainingEnemies;
    }
}
