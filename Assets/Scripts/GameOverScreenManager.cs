using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverScreenManager : MonoBehaviour {

    [SerializeField]
    float waitTime = 3;
    // Use this for initialization
    void OnEnable()
    {
        StartCoroutine(End());
    }


    IEnumerator End()
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }
}
