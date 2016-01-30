using UnityEngine;
using System.Collections;
using UnityEngine.Events;


public class EnemyAI : MonoBehaviour {

    public    BezierSpline spline;
    public   float duration;

    UnityAction updateState;

    public Transform exitLocation;
    public float exitDistance = 1.0f;
    public float pathDifference = .3f;

    [Header("Debug")]
    [SerializeField]    float deadTime = 1.0f;
    [SerializeField]    float multiplier;

    Animator animator;
    private float progress = 0;



    void Awake()
    {
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.LogError("EnemyAi need an animator");
            animator.SetTrigger("Reset");
        }
    }

    void OnEnable()
    {
        progress = 0;
        updateState = WalkingPathState;
        multiplier = Random.Range(-pathDifference, pathDifference);
    }

    private void Update()
    {
        updateState();
    }


    void WalkingPathState()
    {
        progress += Time.deltaTime / duration;
        if (progress > 1f)
        {
            progress = 1f;
            updateState = KidnapingState;
            animator.SetTrigger("Kidnap");
            
        }
        Vector3 position = spline.GetPoint(progress);
        transform.position = position + Quaternion.Euler(0, 0, 90) * spline.GetDirection(progress) * multiplier ;

    }

    void KidnapingState()
    {
        transform.position = Vector3.MoveTowards(transform.position, exitLocation.position, Time.deltaTime);
        float distance = Vector3.Distance(transform.position, exitLocation.position);
        if (exitDistance >= distance)
        {
            gameObject.SetActive(false);
        }
    }

    void OnDead()
    {
        animator.SetTrigger("Dead");
        StartCoroutine(WaitAndDisable());

    }

    IEnumerator WaitAndDisable()
    {

        yield return new WaitForSeconds(deadTime);
        gameObject.SetActive(false);
    }



}
