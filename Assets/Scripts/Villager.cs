using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Villager : MonoBehaviour {

    public delegate void EventBool(bool isSick);

    public static EventBool EventOnSick;
    UnityAction EventOnCelebrate;


    [SerializeField]    float deadTime = 1.0f;
    VillageManager villageManager;



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

    }

    public void OnDead()
    {
        EventOnSick -= OnSick;
        EventOnCelebrate -= OnCelebrate;
        animator.SetTrigger("Dead");
        OnDisable();
        villageManager.OnVillagerDead();
        StartCoroutine(WaitAndDisable());
    }

    IEnumerator WaitAndDisable()
    {

        yield return new WaitForSeconds(deadTime);
        gameObject.SetActive(false);
    }
}
