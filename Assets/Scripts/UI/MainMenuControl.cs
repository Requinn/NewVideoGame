using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour {
    public GameObject MainMenuCanvas;
    public GameObject QuitConfirmCanvas;
    public GameObject OptionsCanvas;

    public SceneLoader loader;
    private bool loadScene = false;
    

	// Use this for initialization
	void Awake () {
        MainMenuCanvas = GameObject.Find("MainMenuCanvas");
        QuitConfirmCanvas = GameObject.Find("QuitConfirm");
        OptionsCanvas = GameObject.Find("OptionMenu");

        QuitConfirmCanvas.SetActive(false);
        OptionsCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OpenOptions()
    {
        MainMenuCanvas.SetActive(false);
        OptionsCanvas.SetActive(true);
    }
    public void CloseOption()
    {
        OptionsCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }
    public void LoadScene(string name)
    {
        loader.loadscene = true;
        /**
        SceneManager.LoadSceneAsync(name);
        MainMenuCanvas.SetActive(false);
        SceneManager.LoadScene(name);
    **/

    }
    public void ConfirmQuit()
    {
        MainMenuCanvas.SetActive(false);
        QuitConfirmCanvas.SetActive(true);
    }
    public void CloseConfirmQuit()
    {
        QuitConfirmCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
