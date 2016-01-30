using UnityEngine;
using System.Collections;

public class TowerAttack : MonoBehaviour {

    [SerializeField]    float range = 1;
    [SerializeField]    float damage = 100;
    [SerializeField]    float speedMultiplier = 1;


    ParticleSystem particle;
    void Awake()
    {
        particle.GetComponent<ParticleSystem>();
        particle.Stop();

    }

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

    public  void ShowParticles()
    {
        particle.Play();
    }

}
