﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Tower : MonoBehaviour {

    //we need a class that initializes the input manager and set the code for all the towers
    [SerializeField]    int requiredSpell;

    [SerializeField]    TowerAttack heal, rain, fire, thunder;
    private Transform areaHeal, areaRain, areaFire, areaThunder;
    private float timeSpellStart;
    private bool attackPerformed;

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
        Debug.Log("Hey!");
        int spellFirstPart = InputManager.spell / 10;
        Debug.Log(spellFirstPart + " == "+ requiredSpell);
        if (spellFirstPart == requiredSpell)
        {
            int type = InputManager.spell % 10;

            switch (type)
            {
                case 1: //heal
                    Attack(heal);
                    areaHeal.GetComponent<Renderer>().enabled = true;
                    Debug.Log("heal");
                    break;
                case 2: //rain
                    Attack(rain);
                    areaRain.GetComponent<Renderer>().enabled = true;
                    Debug.Log("rain");
                    break;
                case 3: //fire
                    Attack(fire);
                    areaFire.GetComponent<Renderer>().enabled = true;
                    Debug.Log("fire");
                    break;
                case 4: //thunder
                    Attack(thunder);
                    areaThunder.GetComponent<Renderer>().enabled = true;
                    Debug.Log("thunder");
                    break;
            }
        }
    }

    void Attack(TowerAttack attack)
    {
        //AddCircle(attack.GetRange());
        attackPerformed = true;
        for (int i = 0; i < EnemyManager.Instance.activeEnemies.Count; ++i)
        {
            EnemyStats enemy = EnemyManager.Instance.activeEnemies[i];
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= attack.GetRange())
            {
                enemy.OnAttacked(attack.GetDamage());
                enemy.speedMultiplier = attack.GetSpeedMultiplier();
            }
        }
        timeSpellStart = Time.time;
    }

    private void AddCircle(float range)
    {
        float theta_scale = 0.1f;             //Set lower to add more points
        double size = (2.0 * Mathf.PI) / theta_scale; //Total number of points in circle.

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetWidth(0.01F, 0.01F);
        lineRenderer.SetVertexCount((int)size + 1);

        int i = 0;
        for (float theta = 0; theta < 2 * (float)Mathf.PI; theta += 0.1f)
        {
            float x = range * Mathf.Cos(theta);
            float y = range * Mathf.Sin(theta);

            Vector3 pos = new Vector3(x, y, -1) + transform.localPosition;
            lineRenderer.SetPosition(i, pos);
            i += 1;
        }
    }

	// Use this for initialization
	void Start () {
        foreach (Transform area in this.GetComponentInChildren<Transform>())
        {
            if (area.name.Equals("HealArea"))
                areaHeal = area;
            else if (area.name.Equals("RainArea"))
                areaRain = area;
            else if (area.name.Equals("FireArea"))
                areaFire = area;
            else if (area.name.Equals("ThunderArea"))
                areaThunder = area;
        }

        areaHeal.GetComponent<Renderer>().enabled = false;
        areaRain.GetComponent<Renderer>().enabled = false;
        areaFire.GetComponent<Renderer>().enabled = false;
        areaThunder.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (attackPerformed && (Time.time > timeSpellStart + 0.2f))
        {
            areaHeal.GetComponent<Renderer>().enabled = false;
            areaRain.GetComponent<Renderer>().enabled = false;
            areaFire.GetComponent<Renderer>().enabled = false;
            areaThunder.GetComponent<Renderer>().enabled = false;
            attackPerformed = false;
        }
	}
}