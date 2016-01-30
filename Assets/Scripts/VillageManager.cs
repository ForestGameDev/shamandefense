using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;


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


    [Header("Spell Input")]
    [SerializeField] GameObject inputParent;
    [SerializeField] Text[] inputs;


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
        InputManager.spellCompleted += OnSpellCheck;
        inputParent.SetActive(false);
    }
    void OnDisable()
    {
        InputManager.spellCompleted -= OnSpellCheck;
    }

    string InputToString(int input)
    {
        switch (input)
        {
            case 1:
                return "A";
            case 2:
                return "S";
            case 3:
                return "D";
            case 4:
                return "F";

        }
        return null;
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

            requiredSpell = RequiredSpellManager.GetSpell(3, requiredSpell);
            inputParent.SetActive(true);


            inputs[0].text = InputToString(requiredSpell / 100);
            inputs[1].text = InputToString((requiredSpell%100) / 10);
            inputs[2].text = InputToString(requiredSpell % 10);

            switch (currentCatastrophe)
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

        if(InputManager.spell ==requiredSpell)
        {
            //TODO implentar la comparacion si es correcto cambiar a normal

            if (Villager.EventOnSick != null)
            {
                Villager.EventOnSick(false);
            }
            OnEnable();
            inputParent.SetActive(false);
        }

    }
}
