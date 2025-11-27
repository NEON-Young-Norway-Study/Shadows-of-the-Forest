using UnityEngine;

public class Salmon_Dialog_InputDisabler : MonoBehaviour
{
    [SerializeField] private ClawCharge clawChargeScript;
    [SerializeField] private GameObject dialogUI;

    void Start()
    {
        clawChargeScript.OnDisable();
    }

    private void Update()
    {
        if(!dialogUI.activeSelf)
        {
            clawChargeScript.OnEnable();
        }
    }
}
