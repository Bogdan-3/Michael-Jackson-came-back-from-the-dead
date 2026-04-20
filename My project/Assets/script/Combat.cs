using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Movement movementScript;
    Player inputActions;

    public AnimationClip SpinKick;
    public Animator animator;
    public TextMeshProUGUI DancePowerText;

    bool spini = true;
    float DancePower=0;
    float counter = 0;
    public float DancePowerMax;

    private void Awake()
    {
        inputActions = new Player();
        inputActions.Combats.Enable();
    }
    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(DancePower < DancePowerMax)
        {
            if(counter >= 1)
            {
                DancePower += 10;
                counter = 0;
            }
        }

        DancePowerText.text = "Dance Power: " + DancePower.ToString();

        if (inputActions.Combats.SpinKicks.WasPressedThisFrame() && spini == true && DancePower >= 50)
        {
            DancePower -= 50;
            StartCoroutine(Spinin_Bitchin());
        }
    }

    IEnumerator Spinin_Bitchin()
    {
        animator.Play(SpinKick.name);
        spini = false;
        Movement.inputActions.Moving.Disable();
        yield return new WaitForSeconds(SpinKick.length);
        animator.Play(movementScript.IdleAnimation.name);
        spini = true;
        Movement.inputActions.Moving.Enable();
    }
}
