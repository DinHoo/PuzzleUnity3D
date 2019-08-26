using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraControl : MonoBehaviour
{
    float rotationSpeed = 1;
    public Transform Target, Player;
    float mouseX, mouseY;

    public Transform Obstruction;
    float zoomSpeed = 2f;

    public Material obstructionMaterial;
    
    void Start()
    {
        Obstruction = Target;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        CamControl();
        ViewObstructed();
    }
    

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(Target);

        if (Input.GetMouseButton(1))
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            
        }
        else
        {
            Player.rotation = Quaternion.Euler(0, mouseX, 0);
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            
        }
    }
    

    void ViewObstructed()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f))
        {
            
            if (hit.collider.gameObject.tag != "Player")
            {
                if(Obstruction)
                {
                    if(Obstruction != hit.transform)
                    {
                        Obstruction = hit.transform;
                        obstructionMaterial = Obstruction.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
                        //Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                        print(obstructionMaterial.name + " " + obstructionMaterial.color);

                        Utillities.ChangeRenderMode(Obstruction.gameObject.GetComponent<MeshRenderer>().material, Utillities.BlendMode.Transparent);
                        Color transparent = new Color(0, 0, 0, 0);
                        Obstruction.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", transparent);

                        if (Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                            transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                    }
                }
                
            }
            else
            {
                if(Obstruction.gameObject.GetComponent<MeshRenderer>())
                {
                    //Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    Obstruction.gameObject.GetComponent<MeshRenderer>().material = obstructionMaterial;
                    if (Vector3.Distance(transform.position, Target.position) < 4.5f)
                        transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                    Obstruction = Target;

                }

            }
        }
    } 
}