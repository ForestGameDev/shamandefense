using UnityEngine;
using System.Collections;

public class WinScreen : MonoBehaviour {

    [SerializeField]    float waitTime = 3;
	// Use this for initialization
	void Start () {
        StartCoroutine(End());
    }


    IEnumerator End()
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }
}
