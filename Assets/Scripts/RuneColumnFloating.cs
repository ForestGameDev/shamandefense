using UnityEngine;
using System.Collections;

public class RuneColumnFloating : MonoBehaviour {


    [SerializeField] float distance;
    [SerializeField] float time;

    float randomSeed;
    // Use this for initialization
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        randomSeed = Random.value;
    }

    void Update()
    {
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.PingPong(Time.time + randomSeed, time) * distance;
        transform.position = startPos + pos;
    }
	

}
