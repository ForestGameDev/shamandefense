using UnityEngine;
using System.Collections;

public class TowerAttack : MonoBehaviour {

    [SerializeField]    float range = 1;
    [SerializeField]    float damage = 100;
    [SerializeField]    float speedMultiplier = 1;


    Animator anim;

    AudioSource audioS;

    SpriteRenderer spritRenderer;

    public float GetRange()
    {
        return range;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetSpeedMultiplier()
    {
        return speedMultiplier;
    }

    public void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            spritRenderer.enabled = false;
        }
        if(!audioS.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnEnable()
    {
        spritRenderer.enabled = true;
    }

    public void Awake()
    {
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
        spritRenderer = GetComponent<SpriteRenderer>();
    }
}
