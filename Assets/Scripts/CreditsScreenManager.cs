using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsScreenManager : MonoBehaviour {

    void OnEnable()
    {
        InputManager.spellCompleted += CheckExit;
    }

    void OnDisable()
    {
        InputManager.spellCompleted -= CheckExit;
    }

    private void CheckExit()
    {
        if (InputManager.spell == 244)
            SceneManager.LoadScene("Title");
    }
}
