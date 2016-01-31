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
        Villager.EventOnDead += OnVillagerDead;
    }
    void OnDisable()
    {
        Villager.EventOnDead -= OnVillagerDead;
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
    
}
