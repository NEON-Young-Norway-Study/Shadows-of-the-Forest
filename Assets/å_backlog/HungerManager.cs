using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class HungerManager : MonoBehaviour
{

    public Slider hungerbar; // the display for the food meter

    public float hunger; // current % food
    public float maxHunger = 100f; // 100%
    public float foodRefill = 10f; // how much % to add when eating, need change or transfer when food elements are added
    public float hungerPerAct = 10f; // food % going down when action
    public float foodReserves = 30f; // how many time you can refill food, need change later

    void Start()
    {
        hunger = maxHunger;
    }

    void Update()
    {
        hungerbar.value = hunger;
;
        if (Input.GetKeyUp(KeyCode.Q)) // hunger goes down on action, might need seperate method
        {
            Debug.Log("ActionHaveHungry");
            hunger -= hungerPerAct;
        }

        if (Input.GetKeyUp(KeyCode.L) && foodReserves > 0f) // eat some food, need change and/or seperate method
        {
            Debug.Log("Food!");
            foodReserves -= 1f;
            hunger += foodRefill;
            hunger = Mathf.Clamp(hunger, 0f, maxHunger); // keeps food within 0-100 range
        }

        if (hunger <= 0)
        {
            Debug.Log("starving");
        }
    } 
}
