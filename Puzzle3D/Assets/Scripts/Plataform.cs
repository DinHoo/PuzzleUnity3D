using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Platform : MonoBehaviour
{
    private enum MovementType
    {
        Circular,
        Straight,
        RotateAround
    }

    

    

    public Vector3 lastPos, deltaPos;

    [SerializeField]
    private Transform rotationCenter;

    private Vector3 rotationPosition;

    [SerializeField]
    private float rotationRadius = 2f, angularSpeed = 2f;

    private float angle = 0f;

    [SerializeField]
    private MovementType movementType_;

    // Start is called before the first frame update
    private void Start()
    {
        rotationPosition = rotationCenter.position;
        
        lastPos = transform.position;
    }

    // Update is called once per frame

    private void Update()
    {
        switch (movementType_)
        {
            case MovementType.RotateAround:
                rotateAround();
                break;

            case MovementType.Circular:
                circularMovement();
                break;

            default:
                circularMovement();
                break;
        }

        
    }

    private void LateUpdate()
    {

    }

    private void circularMovement()
    {
        float posX, posY = 0f;

        posX = rotationPosition.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationPosition.y + Mathf.Sin(angle) * rotationRadius;
        deltaPos = new Vector3(posX, posY, transform.position.z) - lastPos;

        transform.position = new Vector3(posX, posY, transform.position.z);
        angle += Time.deltaTime * angularSpeed;



        lastPos = transform.position;

        if (angle >= 360f)
            angle = 0f;
    }

    private void rotateAround()
    {
        transform.RotateAround(new Vector3(0, 0, 0), Vector3.up, 20 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
       
    }

}