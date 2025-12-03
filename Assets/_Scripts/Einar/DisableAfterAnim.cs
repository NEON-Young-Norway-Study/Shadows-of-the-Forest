using UnityEngine;

public class DisableAfterAnim : MonoBehaviour
{
    [SerializeField] Animator animator;
    private string targetStateName = "CrossFade_Inn";
    private bool hasDisabled = false;

    void Update()
    {
        if (!hasDisabled)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(targetStateName) && stateInfo.normalizedTime >= 1.0f)
            {

                gameObject.SetActive(false);
                hasDisabled = true;
            }
        }
    }
}
