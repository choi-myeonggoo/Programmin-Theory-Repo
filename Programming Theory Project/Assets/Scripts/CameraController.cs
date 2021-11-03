using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField] private float rotateSpeed = 200;
    float mouseX;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Mouse X") * 8;
        mouseX += h * Time.deltaTime * 200;
        transform.eulerAngles = new Vector3(transform.rotation.y, mouseX);

    }
}
