using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TestShader : MonoBehaviour {

    public Material EffectMaterial;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, EffectMaterial);
    }
}
