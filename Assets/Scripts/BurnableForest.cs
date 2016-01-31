using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BurnableForest : MonoBehaviour {

    [SerializeField]   int index = -1;

    void OnEnable()
    {
        LevelManager.OnChangeLevel += OnBurn;
        spriteRen.enabled = false;
    }

    void OnDisable()
    {
        LevelManager.OnChangeLevel -= OnBurn;
    }

    SpriteRenderer spriteRen;
    void Awake()
    {
        spriteRen = GetComponent<SpriteRenderer>();
    }

        	
    public void OnBurn(int index)
    {
        if(this.index <= index)
        {
            spriteRen.enabled = true;
            Destroy(this);
        }
        
    }
}
