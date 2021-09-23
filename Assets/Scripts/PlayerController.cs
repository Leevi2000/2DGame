using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float axle;
    public int playerSpeed;
    public int amountToDecrease = 0;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
      
    }
    void MovePlayer()
    {
        axle = Input.GetAxis("Horizontal");


        if (axle < 0)
        {
            rb.AddForce(new Vector3(-playerSpeed, 0, 0), ForceMode.Impulse);
        }
        else if (axle > 0)
        {
            rb.AddForce(new Vector3(playerSpeed, 0, 0), ForceMode.Impulse);
        }
        else if (axle == 0)
        {

            if (rb.velocity.x > 0)
            {
                amountToDecrease--;
            }
            else if (rb.velocity.x < 0)
            {
                amountToDecrease++;
            }
            rb.AddForce(new Vector3(amountToDecrease, 0, 0), ForceMode.Force);
        }
    }
}
