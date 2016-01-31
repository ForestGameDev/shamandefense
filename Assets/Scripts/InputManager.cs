using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Text;

public class InputManager : MonoBehaviour {

    public static UnityAction spellModified, //to be used by the UI to show the buttons pressed
                              runeSelected, //activate rune in map and UI
                              spellCompleted; //to be used by the towers to throw an attack
    public static int spell = 0;
    private InputHandler btnL, btnD, btnR, btnU, btn1, btn2, btn3, btn4;
    public int amountRitualSteps;
    public bool blocked;

    public void SetAmountRitualSteps(int amount)
    {
        amountRitualSteps = amount;
    }

	// Use this for initialization
	void Start () {
        btnL = new InputHandler("L");
        btnD = new InputHandler("D");
        btnR = new InputHandler("R");
        btnU = new InputHandler("U");

        btn1 = new InputHandler("1");
        btn2 = new InputHandler("2");
        btn3 = new InputHandler("3");
        btn4 = new InputHandler("4");
	}
	
	// Update is called once per frame
	void Update () {
        if (!blocked)
        {
            InputHandler btnPressed = null;

            if (spell < (Mathf.Pow(10, amountRitualSteps - 2)))
            {
                if (btnL.isPressedOnce())
                    btnPressed = btnL;
                else if (btnD.isPressedOnce())
                    btnPressed = btnD;
                else if (btnR.isPressedOnce())
                    btnPressed = btnR;
                else if (btnU.isPressedOnce())
                    btnPressed = btnU;
            }
            else
            {
                if (btn1.isPressedOnce())
                    btnPressed = btn1;
                else if (btn2.isPressedOnce())
                    btnPressed = btn2;
                else if (btn3.isPressedOnce())
                    btnPressed = btn3;
                else if (btn4.isPressedOnce())
                    btnPressed = btn4;
            }

            if (btnPressed != null)
            {
                int pressedCode = GetCodeValue(btnPressed.GetKeyName());
                spell = (spell * 10) + pressedCode;

                Debug.Log("New spell: " + spell);

                if (spellModified != null)
                    spellModified();
                CheckRuneSelected();
                CheckSpellCompleted();
            }
        }
	}

    private int GetCodeValue(string direction)
    {
        if (direction.Equals("L") || direction.Equals("1")) return 1;
        else if (direction.Equals("D") || direction.Equals("2")) return 2;
        else if (direction.Equals("R") || direction.Equals("3")) return 3;
        else return 4;
    }

    private void CheckRuneSelected()
    {
        if (spell > (Mathf.Pow(10, amountRitualSteps - 2)))
        {
            if (runeSelected != null)
                runeSelected();
        }
    }

    private void CheckSpellCompleted()
    {
        //Debug.Log(spell + " > " + Mathf.Pow(10, amountRitualSteps - 1));
        if (spell > (Mathf.Pow(10,amountRitualSteps - 1)) )
        {
            if (spellCompleted != null)
                spellCompleted();
            spell = 0;
        }
    }
}

class InputHandler
{
    private bool wasPressed;
    private string key;
   // private int sign = 0;

    public InputHandler(string keyName)
    {
        key = keyName;
    }

    public string GetKeyName()
    {
        return key;
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