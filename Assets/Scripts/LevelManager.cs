using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour {

    public delegate void EventInt(int index);

    public static EventInt OnChangeLevel;

    [SerializeField] int level = 1;
    [SerializeField] int enemiesPerRound = 100;

    [SerializeField] Text enemyCounter;
    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField]
    Tower[] towersLvl1, towersLvl2, towersLvl3;

    [SerializeField] BezierSpline path2, path3;

    private static LevelManager instance;
    private int remainingEnemies;
    private bool completingLevel;

    public static void RemoveEnemy()
    {
        instance.InstanceRemoveEnemy();
    }

    private void InstanceRemoveEnemy()
    {
        if (!completingLevel)
        {
            if (remainingEnemies > 0)
                remainingEnemies -= 1;
            else
            {
                completingLevel = true;
                for (int i = 0; i < EnemyManager.Instance.activeEnemies.Count; ++i)
                {
                    EnemyStats enemy = EnemyManager.Instance.activeEnemies[i];
                    enemy.OnAttacked(9999);
                }

                level++;
                if(OnChangeLevel != null)
                {
                    OnChangeLevel(level);
                }
               
                if (level == 2)
                {
                    enemySpawner.AddPath(path2);
                }
                else if (level == 3)
                {
                    enemySpawner.AddPath(path3);
                }

                completingLevel = false;
                remainingEnemies = enemiesPerRound;
            }
            instance.UpdateCounter();
        }
    }

	// Use this for initialization
	void Start () {
        instance = this;
        remainingEnemies = enemiesPerRound;
        UpdateCounter();
        if (OnChangeLevel != null)
        {
            OnChangeLevel(1);
        }

    }

    private void UpdateCounter(){
        enemyCounter.text = "×" + remainingEnemies;
    }
}
