using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour {

    public    BezierSpline spline;

    UnityAction updateState;
    [HideInInspector]
    public static UnityAction EventOnStop;
    public static UnityAction RemoveEnemy;

    public Transform exitLocation;
    public float exitDistance = 1.0f;
    public float pathDifference = .3f;
    public float velocity = 1;

    [SerializeField]    float deadTime = 1.0f;

    [Header("Debug")]

    [SerializeField]    float multiplier;

    Animator animator;
    private float progress = 0;
    public Vector3 angle;

    EnemyStats enemyStats;

    SpriteRenderer spriteRender;
    VillageManager villageManager;

    Villager kidnap;

    void Awake()
    {
        villageManager = GameObject.FindObjectOfType<VillageManager>();
        spriteRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogError("EnemyAi need an animator");
            animator.SetTrigger("Reset");
        }
        enemyStats = GetComponent<EnemyStats>();
    }

    void OnEnable()
    {
        progress = 0;
        updateState = WalkingPathState;
        multiplier = Random.Range(-pathDifference, pathDifference);
        EventOnStop += OnStop;
        kidnap = null;
    }

    void OnDisable()
    {
        EventOnStop -= OnStop;
        if(kidnap)
        {
            kidnap.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(updateState != null)
        updateState();
    }

    void WalkingPathState()
    {
        progress += Time.deltaTime / enemyStats.speed * enemyStats.speedMultiplier;
        if (progress > 1f)
        {
            progress = 1f;
            updateState = KidnapingState;

            for(int i=0; i <villageManager.villagers.Count;++i)
            {
                Villager tmpVilla =  villageManager.villagers[i];
                if(tmpVilla.kidnapped == false)
                {
                    kidnap = tmpVilla;
                    kidnap.kidnapped = true;
                    kidnap.GetComponent<AudioSource>().Play();
                    kidnap.OnDead();
                    break;   
                }
            }
            if(kidnap == null)
            {
                LevelManager.GameOver();
            }
            
            animator.SetTrigger("Kidnap");                
        }
        Vector3 position = spline.GetPoint(progress);
        angle = spline.GetDirection(progress);
        spriteRender.flipX = angle.x >= 0;
       // transform.Translate(angle* velocity * Time.deltaTime);

        transform.position = position +  Quaternion.Euler(0, 0, 90) * spline.GetDirection(progress) * multiplier ;

    }

    void KidnapingState()
    {
        transform.position = Vector3.MoveTowards(transform.position, exitLocation.position, Time.deltaTime);
        if(kidnap)
        {
            kidnap.transform.position = Vector3.MoveTowards(kidnap.transform.position, transform.position, Time.deltaTime);
        }
        
        float distance = Vector3.Distance(transform.position, exitLocation.position);
        if (exitDistance >= distance)
        {
                LevelManager.RemoveEnemy();
            gameObject.SetActive(false);
        }
    }


    public void OnDead()
    {
        LevelManager.RemoveEnemy();
        animator.SetTrigger("Dead");
        StartCoroutine(WaitAndDisable());
        updateState = null;

    }

    IEnumerator WaitAndDisable()
    {

        yield return new WaitForSeconds(deadTime);
        gameObject.SetActive(false);
    }


    void OnStop()
    {
        animator.Stop();
        enabled = false;
    }


}
