using UnityEngine;
using System.Collections;

public class TowerAttack : MonoBehaviour {

    [SerializeField]    float range = 1;
    [SerializeField]    float damage = 100;
    [SerializeField]    float speedMultiplier = 1;

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

}
