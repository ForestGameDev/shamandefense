﻿using UnityEngine;
using System.Collections;

public class Shaman : MonoBehaviour {

    [SerializeField]
    Animator _animator;

    Animator animator
    {
        get
        {
            if(_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            return _animator;
        }
    }
	// Use this for initialization

    void OnEnable()
    {
        Villager.EventOnCelebrate += OnCelebrate;
        InputManager.spellModified += OnSpellModified;
        InputManager.spellCompleted += OnSpellComplete;
        Villager.EventOnDead += OnVillagerDead;

        animator.SetTrigger("Reset");

    }

    void OnDisable()
    {
        Villager.EventOnCelebrate -= OnCelebrate;
        InputManager.spellModified -= OnSpellModified;
        InputManager.spellCompleted -= OnSpellComplete;
        Villager.EventOnDead -= OnVillagerDead;
    }

	

    void OnCelebrate()
    {
        animator.SetBool("Dance", true);
        animator.SetBool("Loss", false);
    }

    void OnSpellModified()
    {
        animator.SetBool("Dance", true);
    }

    void OnSpellComplete()
    {
        animator.SetBool("Dance", false);
    }

    void OnVillagerDead()
    {
        animator.SetBool("Loss", true);
        StartCoroutine(LossRoutine());
    }

    IEnumerator LossRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("Loss", false);
    }
}
