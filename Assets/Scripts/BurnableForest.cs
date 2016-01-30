using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BurnableForest : MonoBehaviour {

    // Use this for initialization
    public delegate void EventInt(int  index);

    static public EventInt EventOnBurn;

    [SerializeField]   int index = -1;

    void OnEnable()
    {
        EventOnBurn += OnBurn;
    }

    void OnDisable()
    {
        EventOnBurn -= OnBurn;
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
