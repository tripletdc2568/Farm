using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    
    private float inputX;
    private float inputY;
    private Vector2 movementInput;

    private Animator[] animators;

    private bool isMoving;
     
    private void Update()
        {
            PlayerInput();
            switchAnimation();
        }

       
    private void Movement()
        {
            rb.MovePosition(rb.position + movementInput * speed * Time.deltaTime);
        }
    private void FixedUpdate()
        {
            Movement();
            Movement();
        }
        private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animators = GetComponentsInChildren<Animator>();
    }

    private void PlayerInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        movementInput = new Vector2(inputX, inputY);

        if (inputX != 0 && inputY != 0)
        {
            inputX = 0.6f * inputX;
            inputY = 0.6f * inputY;
        }

        isMoving = movementInput != Vector2.zero;
    }

    private void switchAnimation ()
    {
        foreach(var anim in animators){
            anim.SetBool("isMoving",isMoving);
            if(isMoving){
                anim.SetFloat("InputX",inputX);
                anim.SetFloat("InputY",inputY);
            }
        }
    }
}
