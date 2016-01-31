using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverScreenManager : MonoBehaviour {

    [SerializeField] float wait = 3;

    // Use this for initialization
 	void Start () {
         StartCoroutine(Restart());
 	}
 
     IEnumerator Restart()
     {
         yield return new WaitForSeconds(wait);
         SceneManager.LoadScene("World1-1");
     }
}
