using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Tower : MonoBehaviour {

    //we need a class that initializes the input manager and set the code for all the towers
    [SerializeField]    int requiredSpell;

    [SerializeField]    TowerAttack heal, rain, fire, thunder;

    public void SetRequiredSpell(int code)
    {
        requiredSpell = code;
    }

    void OnEnable()
    {
        InputManager.spellCompleted += SpellCheck;
    }

    void OnDisable()
    {
        InputManager.spellCompleted -= SpellCheck;
    }

    private void SpellCheck()
    {
        int spellFirstPart = InputManager.spell / 10;
        if (spellFirstPart == requiredSpell)
        {
            int type = InputManager.spell % 10;

            switch (type)
            {
                case 1: //heal
                    Attack(heal);
                    break;
                case 2: //rain
                    Attack(rain);
                    break;
                case 3: //fire
                    Attack(fire);
                    break;
                case 4: //thunder
                    Attack(thunder);
                    break;
            }
        }
    }

    void Attack(TowerAttack attack)
    {
        for (int i = 0; i < EnemyManager.Instance.activeEnemies.Count; ++i)
        {
            EnemyStats enemy = EnemyManager.Instance.activeEnemies[i];
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= attack.GetRange())
            {
                enemy.OnAttacked(attack.GetDamage());
            }
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}