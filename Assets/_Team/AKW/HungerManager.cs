using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HungerManager : MonoBehaviour
{

    public Slider hungerbar;

    public float hunger;
    public float maxHunger = 100f;
    public float foodRefillPerSec = 3f;

    void Start()
    {
        hunger = maxHunger;
    }

    void Update()
    {
        hungerbar.value = hunger;

        hunger -= 1f * Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.L))
        {
            hunger += foodRefillPerSec * Time.deltaTime;
        }
    }
}
