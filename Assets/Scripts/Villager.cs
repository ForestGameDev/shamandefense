﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Villager : MonoBehaviour {

    public delegate void EventBool(bool isSick);

    public static EventBool EventOnSick;
    public static UnityAction EventOnCelebrate;
    public static UnityAction EventOnDead;



    [SerializeField]    float deadTime = 1.0f;
    VillageManager villageManager;

    [HideInInspector]
    public bool kidnapped;




    Animator animator;

	// Use this for initialization
	void Awake () {
        animator = GetComponent<Animator>();
        villageManager = FindObjectOfType<VillageManager>();
	}

    void OnEnable()
    {
        EventOnSick += OnSick;
        EventOnCelebrate += OnCelebrate;
        animator.SetBool("Sick", false);
        animator.SetTrigger("Reset");
        villageManager.villagers.Add(this);
        kidnapped = false;
    }

    void OnDisable()
    {
        EventOnSick -= OnSick;
        EventOnCelebrate -= OnCelebrate;
        villageManager.villagers.Remove(this);
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnSick(bool isSick)
    {
        animator.SetBool("Sick", isSick);
    }

    void OnCelebrate()
    {
        animator.SetTrigger("Celebration");
    }

    public void OnDead()
    {
        animator.SetTrigger("Dead");
        OnDisable();
        if(EventOnDead != null)
        {
            EventOnDead();
        }
        
    }
}
