using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public delegate void EventInt(int index);

    public static EventInt OnChangeLevel;
    [SerializeField]
    GameObject ChangeScreenGUI, EndScreen;

    [SerializeField] int level = 1;
    [SerializeField] int enemiesPerRound = 100;

    [SerializeField] Text enemyCounter;
    [SerializeField] EnemySpawner enemySpawner;

    [SerializeField]
    Tower[] towersLvl1, towersLvl2, towersLvl3;

    [SerializeField] BezierSpline path2, path3;

    private static LevelManager instance;
    private int remainingEnemies;
    private bool completingLevel, startingGameOver;

    public static void RemoveEnemy()
    {
        instance.InstanceRemoveEnemy();
    }

    public static void GameOver()
    {
        EnemyManager.Instance.Clear();
        Tower.pool.Clear();
        instance.InstanceGameOver();
    }

    private void InstanceGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void InstanceRemoveEnemy()
    {
        if (!completingLevel && !startingGameOver)
        {
            if (remainingEnemies > 0)
                remainingEnemies -= 1;
            if (remainingEnemies <= 0)
            {
                completingLevel = true;
                for (int i = 0; i < EnemyManager.Instance.activeEnemies.Count; ++i)
                {
                    EnemyStats enemy = EnemyManager.Instance.activeEnemies[i];
                    enemy.OnAttacked(9999);
                }

                level++;

                if (level < 4)
                {
                    if (ChangeScreenGUI)
                    {
                        ChangeScreenGUI.SetActive(true);
                    }


                    if (OnChangeLevel != null)
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
                else
                {
                    if (EndScreen)
                    {
                        EndScreen.SetActive(true);
                    }

                    /*if (OnChangeLevel != null)
                    {
                        OnChangeLevel(level);
                    }*/
                    enemySpawner.SetWaitTimes(0.3f, 0.1f);

                    completingLevel = false;
                    remainingEnemies = 9999;
                }
            }
            UpdateCounter();
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
