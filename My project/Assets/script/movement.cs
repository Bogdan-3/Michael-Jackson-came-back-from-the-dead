using System.Collections;
using UnityEngine;

public class movement : MonoBehaviour
{
    Player inputActions;
    Vector3 Move;
    float horizontal,vertical;
    public float speed,jumpForce;
    public GameObject direction;
    public GameObject Model;
    Vector3 flatRight,flatForward;

    public Animator animator;
    public AnimationClip walkAnimation,IdleAnimation,jumpAnimation;

    Rigidbody rb;

     void Awake()
    {
        rb=GetComponent<Rigidbody>();
        inputActions = new Player();
        inputActions.Moving.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(horizontal == 0 && vertical == 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName(jumpAnimation.name))
        {
            animator.Play(IdleAnimation.name);
        }
        flatForward = direction.transform.forward;
        flatForward.y = 0;
        flatForward.Normalize();

        flatRight = direction.transform.right;
        flatRight.y = 0;
        flatRight.Normalize();

        horizontal=inputActions.Moving.Orizontal.ReadValue<float>();
        vertical=inputActions.Moving.Vertical.ReadValue<float>();
        
        Move=(horizontal*flatRight)+(vertical*flatForward);

        //jumping
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.5f))
        {
            if (inputActions.Moving.Jump.WasPressedThisFrame())
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                StartCoroutine(JumpAnimation());
            }
        }
    }

    IEnumerator JumpAnimation()
    {
        animator.Play(jumpAnimation.name);
        yield return new WaitForSeconds(jumpAnimation.length);
        animator.Play(IdleAnimation.name);
    }

    //noclip fixed update
    void FixedUpdate()
    {
        if (Move.magnitude > 1f)
            Move.Normalize();
        
        if(horizontal != 0 || vertical != 0)
        {
            Model.transform.forward = Move;
            animator.Play(walkAnimation.name);
        }

        rb.MovePosition(rb.position + speed * Time.deltaTime * Move);
    }
}