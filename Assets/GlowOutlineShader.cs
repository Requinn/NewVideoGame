using UnityEngine;
using System.Collections;

public class GlowOutlineShader : MonoBehaviour {

    public GameObject MeshOutline;
    public Material outline;
    public Material nooutline;
    public Texture texture;

    public float sig;
    // Use this for initialization
    void Start() {
        MeshOutline.SetActive(false);
    }

    void Update()
    {

    }

    void activate(float signal)
    {
        /**GetComponent<Renderer>().material.shader = outline;
        //float signal = 1.0f;
        sig = signal;
        if (signal == 1.0f) //message sent when an object is being looked at
        {
            //Graphics.Blit(texture, nooutline);
            MeshOutline.SetActive(true);
            Debug.Log("inspecting");

        }
        else
        {
            MeshOutline.SetActive(false);
            //Graphics.Blit(texture, nooutline);
        }**/
        Destroy(MeshOutline);
    }

    void OnMouseEnter()
    {
        if (MeshOutline != null && Time.timeScale == 1.0f)
        {
            MeshOutline.SetActive(true);
        }
    }
    void OnMouseExit()
    {
        if (MeshOutline != null && Time.timeScale == 1.0f)
        {
            //GetComponent<Renderer>().material.shader = nooutline;
            MeshOutline.SetActive(false);
        }
    }
}
