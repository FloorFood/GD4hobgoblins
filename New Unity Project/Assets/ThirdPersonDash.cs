using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonDash : MonoBehaviour
{
    // Start is called before the first frame update
    ThirdPersonMovement movement;

    public float dashSpeed;
    public float dashTime;
    void Start()
    {
        movement = GetComponent<ThirdPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Dash());
        }
    }
    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            movement.controller.Move(movement.moveDir * dashSpeed * Time.deltaTime); // + movement.velocity * Time.deltaTime); // ! moveDir * speed * Time.deltaTime + velocity * Time.deltaTime
            yield return null;
        }
    }
}
