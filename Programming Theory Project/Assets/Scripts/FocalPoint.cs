using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocalPoint : MonoBehaviour
{
    float angleX;
    float mouseX;
    [SerializeField] float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Track();
        Rotate();
    }

    void Rotate()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        Vector3 focalAngle = transform.rotation.eulerAngles;
        mouseX = Input.GetAxis("Mouse X");
        float x = focalAngle.x - mouseInput.y;
        if(x < 180)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        transform.rotation = Quaternion.Euler(x, focalAngle.y + mouseInput.x ,focalAngle.z);
    }
    void Track()
    {
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + (transform.parent.lossyScale.y), transform.parent.position.z);
    }
}
