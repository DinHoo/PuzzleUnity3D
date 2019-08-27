using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterControl : MonoBehaviour
{
    [SerializeField]
    float Speed;
    [SerializeField]
    [Range(1, 10)]
    float jumpVelocity;
    [SerializeField]
    bool correndo;
    [SerializeField]
    bool estaNoSolo;


    void Update ()
    {
        PlayerMovement();
        PlayerJump();
    }

    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");


        Vector3 playerMovement;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerMovement = new Vector3(hor, 0f, ver) * (Speed*2) * Time.deltaTime;
            correndo = true;
        }
        else
        {
            playerMovement = new Vector3(hor, 0f, ver) * Speed * Time.deltaTime;
            correndo = false;

        }
        transform.Translate(playerMovement, Space.Self);

    }

    void PlayerJump()
    {
        if(Input.GetButton("Jump") && estaNoSolo)
        {
            estaNoSolo = false;
            //Vector3 aux = GetComponent<Rigidbody>().velocity;
            //aux.y = 0;
            //GetComponent<Rigidbody>().velocity = aux;
            GetComponent<Rigidbody>().velocity += Vector3.up * jumpVelocity;
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Solo"))
        {
            estaNoSolo = true;
        }
    }

    
}