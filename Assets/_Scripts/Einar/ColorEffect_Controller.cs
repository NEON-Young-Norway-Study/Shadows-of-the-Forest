using UnityEngine;

public class ColorEffect_Controller : MonoBehaviour
{
    public Material postProcessMaterial; // assign in inspector
    [Range(0f, 1f)]
    public float strength = 1f;

    void Start()
    {
        Debug.Log("ColorEffect_Controller script active");
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Debug.Log("OnRenderImage called");
        if (postProcessMaterial != null)
        {
            postProcessMaterial.SetFloat("_Strength", strength);
            Graphics.Blit(src, dest, postProcessMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
