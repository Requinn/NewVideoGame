using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPause : MonoBehaviour {
    public GameObject pauseCanvas;
    public GameObject optionCanvas;

    public float wait = 1.0f;
    public enum Page
    {
        none, option, credits
    }

    private MouseLook firstpersonCamera;
    private Page currentpage;
    bool busy = false;
    bool paused = false;

	// Use this for initialization
	void Start ()
    {
        firstpersonCamera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MouseLook>();
        pauseCanvas = GameObject.Find("Pause");
        optionCanvas = GameObject.Find("OptionMenu");
        //Are all these set actives necessary? I'm thinking you can just leave the Canvases.
        pauseCanvas.SetActive(false);
        optionCanvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	    if(!busy && (Input.GetKeyDown(KeyCode.Escape)))
        {
            busy = true;
            paused = !paused;
            if (paused)
            {
                StartCoroutine(beginPause());
            }
            else
            {
                StartCoroutine(endPause());
            }
        }
	}

    IEnumerator beginPause()
    {
        firstpersonCamera.enabled = false;
        //Enables usage of mouse in the menu, find something better? works for now though.
        Screen.lockCursor = false;
        Time.timeScale = 0.1f;
        pauseCanvas.SetActive(true);
        //pauseButtons.SetActive(true);
        //pauseText.SetActive(true);
        AudioListener.pause = true;
        yield return new WaitForSeconds(0.1f/wait);
        Time.timeScale = 0;
        busy = false;
    }
    IEnumerator endPause()
    {
        firstpersonCamera.enabled = true;
        Screen.lockCursor = true;
        pauseCanvas.SetActive(false);
        AudioListener.pause = false;
        Time.timeScale = 1;
        yield return null;
        busy = false;
    }

    public void Resume()
    {
        if (!busy)
        {
            busy = true;
            paused = !paused;
            StartCoroutine(endPause());
        }
    }

    public void Quit()
    {
        if (!busy)
        {
            busy = true;
            Application.Quit();
            //Does this work? All it does is freeze the test scene...
        }
    }

    public void Options()
    {
        if (!busy)
        {
            pauseCanvas.SetActive(false);
            optionCanvas.SetActive(true);
            busy = true;
        }
    }

    public void closeOptions()
    {
        busy = false;
        optionCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void adjustVolume(float volumectrl)
    {
        AudioListener.volume = volumectrl;
    }

    public void adjustResolution(int choice)
    {
        
        switch (choice)
        {
            case 0: //640 x 480
                Screen.SetResolution(640, 480, false);
                break;
            case 1: //1280 x 1024
                Screen.SetResolution(1280, 1024, false);
                break;
            case 2: //1600 x 900
                Screen.SetResolution(1600, 900, false);
                break;
            default:
                break;
        }

    }
}
