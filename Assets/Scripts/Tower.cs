using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class Tower : MonoBehaviour {

    //we need a class that initializes the input manager and set the code for all the towers
    [SerializeField]    int requiredSpell;

    [SerializeField]    TowerAttack heal, rain, fire, thunder;
    private Transform areaHeal, areaRain, areaFire, areaThunder, baseHighlight;
    private float timeSpellStart;
    private bool attackPerformed;

    [SerializeField] int levelIndex;

    static PoolDictionary pool = new PoolDictionary();

    [SerializeField]
    Text[] inputs;


    public void SetRequiredSpell(int code)
    {
        requiredSpell = code;

    }

    void Awake()
    {
        LevelManager.OnChangeLevel += OnChangeLevel;
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        InputManager.spellCompleted += SpellCheck;
        InputManager.runeSelected += SelectCheck;
      //  requiredSpell = RequiredSpellManager.GetSpell(2, requiredSpell);


        inputs[0].text = InputToString(requiredSpell / 10);
        inputs[1].text = InputToString(requiredSpell % 10);
    }

    string InputToString(int input)
    {
        switch(input)
        {
            case 1:
                return "A";
            case 2:
                return "S";
            case 3:
                return "D";
            case 4:
                return "F";
            
        }
        return null;
    }

    void OnDisable()
    {
        InputManager.spellCompleted -= SpellCheck;
        InputManager.runeSelected -= SelectCheck;
    }

    void OnChangeLevel(int newLevel)
    {
        if(levelIndex <= newLevel)
        {
            gameObject.SetActive(true);
            LevelManager.OnChangeLevel -= OnChangeLevel;
        }
   }


    private void SelectCheck()
    {
        Debug.Log("AAA");
        if (InputManager.spell == requiredSpell)
        {
            baseHighlight.GetComponent<VisibilityManager>().isVisible = true;
        }
    }

    private void SpellCheck()
    {
        int spellFirstPart = InputManager.spell / 10;
        if (spellFirstPart == requiredSpell)
        {
            int type = InputManager.spell % 10;

            switch (type)
            {
                case 1: //rain
                    Attack(rain);
                    areaRain.GetComponent<VisibilityManager>().isVisible = true;
                    Debug.Log("rain");
                    break;
                case 2: //fire
                    Attack(fire);
                    areaFire.GetComponent<VisibilityManager>().isVisible = true;
                    Debug.Log("fire");
                    break;
                case 3: //thunder
                    Attack(thunder);
                    areaThunder.GetComponent<VisibilityManager>().isVisible = true;
                    Debug.Log("thunder");
                    break;
                case 4: //heal
                    Attack(heal);
                    areaHeal.GetComponent<VisibilityManager>().isVisible = true;
                    Debug.Log("heal");
                    break;
            }
        }
    }

    void Attack(TowerAttack attack)
    {
        GameObject tmpAttck = pool.Get(attack.gameObject);
        tmpAttck.transform.position = transform.position;
        tmpAttck.SetActive(true);
        //attack.ShowParticles();
        //AddCircle(attack.GetRange()); //To see attack area

        baseHighlight.GetComponent<VisibilityManager>().isVisible = false;

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
            else if (area.name.Equals("BaseHighlight"))
                baseHighlight = area;
        }

        /*areaHeal.GetComponent<VisibilityManager>().isVisible = false;
        areaRain.GetComponent<VisibilityManager>().isVisible = false;
        areaFire.GetComponent<VisibilityManager>().isVisible = false;
        areaThunder.GetComponent<VisibilityManager>().isVisible = false;
        baseHighlight.GetComponent<VisibilityManager>().isVisible = false;*/
	}
	
	// Update is called once per frame
	void Update () {
        if (attackPerformed && (Time.time > timeSpellStart + 0.5f))
        {
            //TODO: hide highlight

            areaHeal.GetComponent<VisibilityManager>().isVisible = false;
            areaRain.GetComponent<VisibilityManager>().isVisible = false;
            areaFire.GetComponent<VisibilityManager>().isVisible = false;
            areaThunder.GetComponent<VisibilityManager>().isVisible = false;
            attackPerformed = false;
        }
	}
}