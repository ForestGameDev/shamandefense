using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BurnableForest : MonoBehaviour {

    [SerializeField]   int index = -1;

    void OnEnable()
    {
        LevelManager.OnChangeLevel += OnBurn;
    }

    void OnDisable()
    {
        LevelManager.OnChangeLevel -= OnBurn;
    }

        	
    public void OnBurn(int index)
    {
        if(this.index == index)
        {
            StartCoroutine(BurnCoroutine());
        }
        
    }

    IEnumerator BurnCoroutine()
    {
        foreach (Transform child in transform)
        {
            yield return new WaitForSeconds(.1f);
            child.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
