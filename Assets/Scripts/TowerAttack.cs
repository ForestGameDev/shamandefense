using UnityEngine;
using System.Collections;

public class TowerAttack : MonoBehaviour {

    [SerializeField]    float range = 1;
    [SerializeField]    float damage = 100;
    [SerializeField]    float speedMultiplier = 1;

    [SerializeField]    ParticleSystem particleSys;


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

    public  void OnEnable()
    {
        particleSys.Emit(20);
    }
    public void OnDisable()
    {

    }



}
