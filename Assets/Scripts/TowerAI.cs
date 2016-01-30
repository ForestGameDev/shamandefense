using UnityEngine;
using System.Collections;

public class TowerAI : MonoBehaviour {

    [SerializeField]    float range = 1;
    [SerializeField]    float damage = 100;

    void OnEnable()
    {
        InvokeRepeating("Attack", .01f, 1f);
    }


    void OnDisable()
    {
        CancelInvoke("Attack");
    }


	// Use this for initialization
	void Start () {
        /*
        float theta_scale = 0.1f;             //Set lower to add more points
        double size = (2.0 * Mathf.PI) / theta_scale; //Total number of points in circle.

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetWidth(0.2F, 0.2F);
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
        */
        
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void Attack()
    {
        for(int i=0;i<EnemyManager.Instance.activeEnemies.Count;++i)
        {
            EnemyStats enemy = EnemyManager.Instance.activeEnemies[i];
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance <= range)
            {
                enemy.OnAttacked(damage);
            }
        }
    }
}
