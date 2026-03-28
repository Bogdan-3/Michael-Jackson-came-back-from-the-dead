using UnityEngine;

public class movement : MonoBehaviour
{
    Player inputActions;
    Vector3 Move;
    float horizontal,vertical;
    public float speed,jumpForce;
    public GameObject direction;
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
        Vector3 flatForward = direction.transform.forward;
        flatForward.y = 0;
        flatForward.Normalize();

        Vector3 flatRight = direction.transform.right;
        flatRight.y = 0;
        flatRight.Normalize();

        horizontal=inputActions.Moving.Orizontal.ReadValue<float>();
        vertical=inputActions.Moving.Vertical.ReadValue<float>();
        
        Move=(horizontal*flatRight)+(vertical*flatForward);

        //jumping
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.25f))
        {
            if (inputActions.Moving.Jump.WasPressedThisFrame())
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    //noclip fixed update
    void FixedUpdate()
    {
            if (Move.magnitude > 1f)
        Move.Normalize();
        rb.MovePosition(rb.position + speed * Time.deltaTime * Move);
    }
}