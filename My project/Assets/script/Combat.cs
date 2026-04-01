using UnityEngine;

public class Combat : MonoBehaviour
{
    Player inputActions;

    public AnimationClip SpinKick;
    public Animator animator;
    

    // Update is called once per frame
    void Update()
    {
        if (inputActions.Combats.SpinKicks.WasPressedThisFrame())
        {
            animator.Play(SpinKick.name);
        }
    }
}
