using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  
   [SerializeField] private float movementSpeed = 120f;
   private float smoothTime = 0.1f;
   private Vector3 movementVelocity = Vector3.zero;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
       Vector3 targetPosition = transform.position;
       float Inputx = Input.GetAxisRaw("Horizontal");
       float Inputz = Input.GetAxisRaw("Vertical");

       Vector3 floatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;

       if(Inputz < 0 )
         targetPosition -= floatForward * movementSpeed * Time.deltaTime;
       if(Inputz >0)
         targetPosition += floatForward * movementSpeed * Time.deltaTime;

        if(Inputx < 0)
         targetPosition -= transform.right * movementSpeed * Time.deltaTime;
        if(Inputx > 0)
         targetPosition += transform.right * movementSpeed * Time.deltaTime;

        transform.position = Vector3.SmoothDamp(transform.position,targetPosition, ref movementVelocity,smoothTime);
    }
}