using System.Collections;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Movement movementScript;
    Player inputActions;

    public AnimationClip SpinKick;
    public Animator animator;

    bool spini = true;
    private void Awake()
    {
        inputActions = new Player();
        inputActions.Combats.Enable();
    }
    // Update is called once per frame
    void Update()
    {
        if (inputActions.Combats.SpinKicks.WasPressedThisFrame() && spini == true)
        {
            StartCoroutine(Spinin_Bitchin());
        }
    }

    IEnumerator Spinin_Bitchin()
    {
        animator.Play(SpinKick.name);
        spini = false;
        yield return new WaitForSeconds(SpinKick.length);
        animator.Play(movementScript.IdleAnimation.name);
        spini = true;
    }
}
