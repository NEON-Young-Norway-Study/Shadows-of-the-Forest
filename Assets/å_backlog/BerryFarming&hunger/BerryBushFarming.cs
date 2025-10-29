using Unity.VisualScripting;
using UnityEngine;

public class BerryBushFarming : MonoBehaviour
{
    // need refference do day-night cycle

    [Header("plant seed")]
    public bool isPlanted = false;

    [Header("Water the plant?")]
    public bool isWatered = false;

    [Header("Bush Stages")]
    public bool isStage0 = false; // just planted seed/sapling
    public bool isStage1 = false; // medium growing bush
    public bool isStage2 = false; // fulgrown Berry bush
    public bool isGrowingBerries = false;

    [Header("bushMaterials")] // may change to sprites or models
    public Material noBushMaterial;
    public Material stage0Material;
    public Material stage1Material;
    public Material stage2Material;
    public Material stage2WBerriesMaterial;

    private Renderer bushRenderer;

    private void Start()
    {
        bushRenderer = GetComponent<Renderer>();
        UpdateBushMaterial();
    }
    
    private void OnMouseDown()
    {
        if (!isPlanted)
        {
            Debug.Log("nothing but dirt");
            isStage0 = false;
            isStage1 = false;
            isStage2 = false;
            isGrowingBerries = false;
            UpdateBushMaterial();
        }
        else if (isPlanted)
        {
            Debug.Log("bush clicked");
            if (!isStage0 && !isStage1 && !isStage2)
            {
                isStage0 = true;
                UpdateBushMaterial();
            }
            else if (isStage0 && isWatered)
            {
                Debug.Log("Growing Stage1");
                isStage0 = false;
                isWatered = false;
                isStage1 = true;
                UpdateBushMaterial();
            }
            else if (isStage1 && isWatered)
            {
                Debug.Log("Growing Stage2");
                isStage1 = false;
                isWatered = false;
                isStage2 = true;
                UpdateBushMaterial();
            }
            else if (isStage2 && isWatered && !isGrowingBerries)
            {
                Debug.Log("Growing Berries");
                isWatered = false;
                isGrowingBerries = true;
                UpdateBushMaterial();
            }
            else if (isStage2 && isGrowingBerries)
            {
                Debug.Log("berries Plucked");
                isGrowingBerries = false;
                UpdateBushMaterial();
            }
        }
        
    }
    private void UpdateBushMaterial()
    {
        if (bushRenderer == null) return;

        if (!isPlanted)
            bushRenderer.material = noBushMaterial;
        else if (isStage0)
            bushRenderer.material = stage0Material;
        else if (isStage1)
            bushRenderer.material = stage1Material;
        else if (isStage2 && !isGrowingBerries)
            bushRenderer.material = stage2Material;
        else if (isStage2 && isGrowingBerries)
            bushRenderer.material = stage2WBerriesMaterial;
    }
}
