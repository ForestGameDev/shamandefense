using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputUI : MonoBehaviour {
    public GameObject[] bongos;
    public Text input1, input2, input3;
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
                
                input1.text = GetLetter(type);
                select1.gameObject.SetActive(false);
                select2.gameObject.SetActive(true);
                break;
            case 2:
                input2.text = GetLetter(type);
                select2.gameObject.SetActive(false);
                select3.gameObject.SetActive(true);
                spellsDialog.gameObject.SetActive(true);
                break;
            case 3:
                input3.text = GetLetter(type);
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
        input1.text = "";
        input2.text = "";
        input3.text = "";
    }

    private string GetLetter(int type)
    {
        switch (type)
        {
            case 1: return "A";
            case 2: return "S";
            case 3: return "D";
            case 4: return "F";
            default: return "?";
        }
    }

	// Use this for initialization
	void Start () {
        select2.gameObject.SetActive(false);
        select3.gameObject.SetActive(false);
        spellsDialog.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (commandCompleted && (Time.time > timeCompletedCommand + 0.5f))
        {
            input1.text = "";
            input2.text = "";
            input3.text = "";
            commandCompleted = false;
            select1.gameObject.SetActive(true);
            spellsDialog.gameObject.SetActive(false);
            inputManager.blocked = false;
        }
	}
}
