using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour {

    void OnEnable()
    {
        InputManager.spellCompleted += CheckStart;
    }

    void OnDisable()
    {
        InputManager.spellCompleted -= CheckStart;
    }

    private void CheckStart()
    {
        if (InputManager.spell == 432)
            SceneManager.LoadScene("World1-1");

        if (InputManager.spell == 244)
            SceneManager.LoadScene("Credits");
    }
}
