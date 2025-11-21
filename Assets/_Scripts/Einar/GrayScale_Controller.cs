using UnityEngine;
using UnityEngine.UI;

public class GrayScale_Controller : MonoBehaviour
{
    public RawImage overlayImage; // Assign in inspector
    [Range(0f, 1f)]
    public float initialBlend = 0f; // Set in inspector: 0 = color, 1 = black & white

    private Material overlayMaterial;

    void Start()
    {
        // Get the material from the RawImage
        overlayMaterial = overlayImage.material;
        // Set the initial blend value
        SetBlend(initialBlend);
    }

    public void SetBlend(float value)
    {
        overlayMaterial.SetFloat("_Blend", Mathf.Clamp01(value));
    }
}
