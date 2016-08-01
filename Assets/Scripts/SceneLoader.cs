using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MovementEffects;
using System.Collections;
using System;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour {
    

    public bool loadscene = false;
    private AsyncOperation async = null;
    [SerializeField]
    private int scene;
    [SerializeField]
    private Text loadingText;
	
	// Update is called once per frame
	void Update () {
	    if(loadscene == true)
        {
            loadscene = false;
            Timing.RunCoroutine(LoadNewScene());
        }
        if(loadscene == false)
        {

        }
	}

    private IEnumerator<float> LoadNewScene()
    {
        async = SceneManager.LoadSceneAsync(scene);
        //prevents individual game objects running the moment they load in
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
            //async.progress will show its progress (duh)
            yield return 0f;
            //90% because our secene never fully loads when we have async activation false
            if(async.progress == 0.9f)
            {
                loadingText.text = "COMPLETE.\nPress any key to continue!";
                if (Input.anyKeyDown)
                {
                    async.allowSceneActivation = true;
                }
            }
        } 
        yield return 0f;
    }
}
