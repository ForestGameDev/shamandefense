using UnityEngine;
using System.Collections;

public class TowerAttack : MonoBehaviour {

    [SerializeField]    float range = 1;
    [SerializeField]    float damage = 100;

    public float GetRange()
    {
        return range;
    }

    public float GetDamage()
    {
        return damage;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
