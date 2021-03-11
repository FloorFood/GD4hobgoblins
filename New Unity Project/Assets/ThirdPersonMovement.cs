using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 6;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform cam;

    //Jump Stuff
    public Vector3 velocity;
    public float gravity = -0.01f;
    public Transform groundCheck;
    public float groundDist;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 3;

    //Dash & Movement
    public Vector3 moveDir;
    public float h;
    public float v;
    bool isDashing = false;
    public float dashSpeed;
    public float dashTime;

    float timer;
    bool canDash;
    public Vector3 StartDashPos;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded ) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if(Input.GetMouseButtonDown(0) && !isDashing && canDash)
        {
            isDashing = true;
            timer = 0;
            canDash = false;
            StartDashPos = transform.position;
            gameObject.layer = 10;
        }
    }
    void FixedUpdate()
    {
        Vector3 dir = new Vector3(h, 0, v).normalized;

         //Jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = gravity;
            canDash = true;
        }

        moveDir = Vector3.zero;
        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        }
        velocity.y += gravity;

        if(!isDashing){  controller.Move(moveDir * speed + velocity);}
        else{ Dash(); }
    }

    void Dash()
    {
        if(timer < dashTime)
        {
            controller.Move(moveDir * dashSpeed); 
            timer++;
        }
        if(timer >= dashTime)
        {
            isDashing = false;
            float two_r = controller.radius * 2;
            float h = controller.height;
            Vector3 tp = transform.position;

            Vector3 start = new Vector3(tp.x, tp.y + (h - two_r)/2.0f, tp.z);
            Vector3 end = new Vector3(tp.x, tp.y - (h - two_r)/2.0f, tp.z);

            if(Physics.CheckCapsule(start, end, controller.radius, groundMask))
            {
                transform.position = StartDashPos;
                Debug.Log("teleported");
            }
            gameObject.layer = 9;
        }
    }
}
