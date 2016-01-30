using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;


public class VillageManager : MonoBehaviour {

    enum Catastrophes
    {
        NONE,
        DROUGHT,
        SICKNESS
    }

    [SerializeField] int requiredSpell;
    [SerializeField] float catastropheProbability = 10;
    [SerializeField] float catastropherCountDown = 10f;
    [SerializeField] Catastrophes currentCatastrophe;
    [Header("Debug")]
    [SerializeField]
    float catastropheCounter = 0;
    [SerializeField]
    float random;

    public List<Villager> villagers = new List<Villager>();


    


    [SerializeField] UnityAction currentState;

    // Use this for initialization
    void OnEnable () {
        currentState = NormalState;
        catastropheCounter = 0;
        currentCatastrophe = Catastrophes.NONE;
    }

    void OnDisable()
    {

    }


	
	// Update is called once per frame
	void Update () {
        if(currentState != null)
        {
            currentState();
        }
 	}


    void NormalState()
    {
        catastropheCounter += Time.deltaTime;

        random = Random.value * catastropheCounter - 2;
        if (random >= catastropheProbability)
        {
            catastropheCounter = 0;
            currentState = CatastropheState;
            currentCatastrophe = Random.value > .5f ? Catastrophes.DROUGHT: Catastrophes.SICKNESS;
            
            switch(currentCatastrophe)
            {
                case Catastrophes.DROUGHT:
                    break;
                case Catastrophes.SICKNESS:
                    if(Villager.EventOnSick != null)
                    {
                        Villager.EventOnSick(true);
                    }

                    break;
            }
            //TODO registrar al spell checker
        }
    }

    void CatastropheState()
    {
        catastropheCounter += Time.deltaTime;
        if(catastropheCounter >= catastropherCountDown)
        {
            catastropheCounter -= catastropherCountDown;
            KillVillager();
        }
    }

    void KillVillager()
    {
        if(villagers.Count > 0)
        {
            villagers[0].OnDead();
        }
    }

    public void OnVillagerDead()
    {
        if(villagers.Count == 0)
        {
            if(EnemyAI.EventOnStop != null)
            {
                EnemyAI.EventOnStop();
            }
            
        }
    }

    void OnSpellCheck()
    {
        //TODO implentar la comparacion si es correcto cambiar a normal

        if (Villager.EventOnSick != null)
        {
            Villager.EventOnSick(false);
        }
        OnEnable();
    }
}
