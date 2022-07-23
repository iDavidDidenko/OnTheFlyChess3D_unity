using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{

    [Header("MovementSpeed")]
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float moveSpeed2 = 0.5f;
    [SerializeField] private float moveScroll = 8.0f;

    [SerializeField] private KeyCode moveM = KeyCode.Mouse2;
    [SerializeField] private KeyCode moveR = KeyCode.Mouse1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;

        float Y = Input.GetAxis("Mouse X");
        float X = Input.GetAxis("Mouse Y");

        if(Input.GetMouseButton(2))
        {
            move += Vector3.up * X * -moveSpeed2;
            move += Vector3.right * Y * -moveSpeed2;
        }
        if(Input.GetMouseButton(1))
        {
            transform.RotateAround(transform.position, transform.right, X * -moveSpeed);
            transform.RotateAround(transform.position, Vector3.up, Y * moveSpeed);
        }
    
        transform.Translate(move);
    }
    void LateUpdate(){
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.forward * mouseScroll * moveScroll);
    }
}
