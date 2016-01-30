using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Text;

public class InputManager : MonoBehaviour {

    public static UnityAction spellModified, //to be used by the UI to show the buttons pressed
                              spellCompleted; //to be used by the towers to throw an attack
    public static int spell = 0;
    private InputHandler btn1, btn2, btn3, btn4;
    public int amountRitualSteps;

    public void SetAmountRitualSteps(int amount)
    {
        amountRitualSteps = amount;
    }

	// Use this for initialization
	void Start () {
        btn1 = new InputHandler("1");
        btn2 = new InputHandler("2");
        btn3 = new InputHandler("3");
        btn4 = new InputHandler("4");
	}
	
	// Update is called once per frame
	void Update () {
        InputHandler btnPressed = null;
        if (btn1.isPressedOnce())
            btnPressed = btn1;
        else if (btn2.isPressedOnce())
            btnPressed = btn2;
        else if (btn3.isPressedOnce())
            btnPressed = btn3;
        else if (btn4.isPressedOnce())
            btnPressed = btn4;

        if (btnPressed != null)
        {
            int pressedCode;
            int.TryParse(btnPressed.GetKeyName(), out pressedCode);
            spell = (spell * 10) + pressedCode;

            if (spellModified != null)
                spellModified();
            CheckSpellCompleted();
        }
	}

    private void CheckSpellCompleted()
    {
        if (spell > (Mathf.Pow(10,amountRitualSteps - 1)) )
        {
            if (spellModified != null)
                spellCompleted();
            spell = 0;
        }
    }
}

class InputHandler
{
    private bool wasPressed;
    private string key;
    private int sign = 0;

    public InputHandler(string keyName)
    {
        key = keyName;
    }

    public string GetKeyName()
    {
        return key;
    }

    public InputHandler(string keyName, bool positive)
    {
        key = keyName;
        sign = positive ? 1 : -1;
    }

    public bool isPressedOnce()
    {
        if (Input.GetButton(key))
        {
            if (!wasPressed)
            {
                wasPressed = true;
                return true;
            }
        }
        else
        {
            wasPressed = false;
        }
        return false;
    }

    public bool isPressed()
    {
        return Input.GetButton(key);
    }
}