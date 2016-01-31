using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputUI : MonoBehaviour {
    public GameObject[] bongos;
    public Image arrow1, arrow2;
    public Text textSpell;
    public Image select1, select2, select3, spellsDialog;
    public InputManager inputManager;

    [SerializeField] AudioClip[] drumAudio;
    AudioSource audioSource;

    private float timeCompletedCommand;
    private bool commandCompleted;

    private int currentStep = 1;

    void OnEnable()
    {
        InputManager.spellModified += InputDetected;
    }

    void OnDisable()
    {
        InputManager.spellModified -= InputDetected;
    }

    private void InputDetected()
    {
        int type = InputManager.spell % 10;
        audioSource.clip = drumAudio[type - 1];
        audioSource.Play();
        iTween.ShakeScale(bongos[currentStep - 1],Vector3.up, .25f);
        switch (currentStep)
        {
            case 1:
                arrow1.gameObject.SetActive(true);
                arrow1.transform.eulerAngles = new Vector3(0, 0, GetAngle(type));
                select1.gameObject.SetActive(false);
                select2.gameObject.SetActive(true);
                break;
            case 2:
                arrow2.gameObject.SetActive(true);
                arrow2.transform.eulerAngles = new Vector3(0, 0, GetAngle(type));
                select2.gameObject.SetActive(false);
                select3.gameObject.SetActive(true);
                spellsDialog.gameObject.SetActive(true);
                break;
            case 3:
                textSpell.gameObject.SetActive(true);
                textSpell.text = type.ToString();
                select3.gameObject.SetActive(false);
                commandCompleted = true;
                timeCompletedCommand = Time.time;
                inputManager.blocked = true;
                break;
        }

        currentStep++;
        if (currentStep > 3)
        {
            currentStep = 1;
        }
    }

    private void Clear()
    {
        arrow1.gameObject.SetActive(false);
        arrow2.gameObject.SetActive(false);
        textSpell.gameObject.SetActive(false);
    }

    private int GetAngle(int type)
    {
        /*switch (type)
        {
            case 1: return "A";
            case 2: return "S";
            case 3: return "D";
            case 4: return "F";
            default: return "?";
        }*/
        return 90 * type;
    }

	// Use this for initialization
	void Start () {
        arrow1.gameObject.SetActive(false);
        arrow2.gameObject.SetActive(false);
        textSpell.gameObject.SetActive(false);

        select2.gameObject.SetActive(false);
        select3.gameObject.SetActive(false);
        spellsDialog.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (commandCompleted && (Time.time > timeCompletedCommand + 0.5f))
        {
            arrow1.gameObject.SetActive(false);
            arrow2.gameObject.SetActive(false);
            textSpell.gameObject.SetActive(false);
            commandCompleted = false;
            select1.gameObject.SetActive(true);
            spellsDialog.gameObject.SetActive(false);
            inputManager.blocked = false;
        }
	}
}
