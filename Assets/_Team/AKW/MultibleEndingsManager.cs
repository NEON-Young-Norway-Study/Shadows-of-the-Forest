using UnityEngine;
using UnityEngine.UI;

public class MultibleEndingsManager : MonoBehaviour
{
    [SerializeField] private HungerManager hungerManager;

    public Image EndingDisplay;

    public Sprite badEnd;
    public Sprite nuteralEnd;
    public Sprite goodEnd;

    void Update()
    {
        if (hungerManager.hunger <= 20)
        {
            Debug.Log("bad ending");
            EndingDisplay.sprite = badEnd;
        }

        if (hungerManager.hunger > 20 &&  hungerManager.hunger <= 80)
        {
            Debug.Log("nuteral ending");
            EndingDisplay.sprite = nuteralEnd;
        }

        if (hungerManager.hunger > 80)
        {
            Debug.Log("good ending");
            EndingDisplay.sprite = goodEnd;
        }
    }
        
}
