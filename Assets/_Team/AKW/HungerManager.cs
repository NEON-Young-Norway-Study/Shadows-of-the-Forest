using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HungerManager : MonoBehaviour
{

    public Slider hungerbar;

    public float hunger;
    public float maxHunger = 100f;
    public float foodRefill = 10f;
    public float hungerPerAct = 10f;
    public float foodReserves = 3f;

    void Start()
    {
        hunger = maxHunger;
    }

    void Update()
    {
        hungerbar.value = hunger;
;
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Debug.Log("ActionHaveHungry");
            hunger -= hungerPerAct;
        }

        if (Input.GetKeyUp(KeyCode.L) && foodReserves > 0f)
        {
            Debug.Log("Food!");
            foodReserves -= 1f;
            hunger += foodRefill;
            hunger = Mathf.Clamp(hunger, 0f, maxHunger);
        }

        if (hunger <= 0)
        {
            Debug.Log("starving");
        }
    } 
}
