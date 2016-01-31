using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverScreenManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Restart());
	}

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("World1-1");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
