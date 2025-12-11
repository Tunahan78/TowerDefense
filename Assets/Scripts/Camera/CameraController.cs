using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] private float movementSpeed = 120f;

   [Header("Camera Rotation Details")]
   [SerializeField] private Transform focusPoint;
   [SerializeField] private float rotationSpeed = 200f;
   [SerializeField] private float maxDistance = 15f;
   [Space]
   private float pitch;
   [SerializeField] private float maxPitch = 85f;
   [SerializeField] private float minPitch = 5f;

   [Header("Camera Zoom Details")]
   [SerializeField] private float zoomSpeed = 15f;
   [SerializeField] private float minZoom = 3f;
   [SerializeField] private float maxZoom = 15f;
   private Vector3 zoomVelocity = Vector3.zero;
   
   [Header("Camera Boundary")]
   [SerializeField] private Transform mapCenter; // ✅ HARITA MERKEZİ
   [SerializeField] private float boundaryRadius = 25f;
   
   private float smoothTime = 0.1f;
   private Vector3 movementVelocity = Vector3.zero;

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
        
        focusPoint.position = transform.position + transform.forward * GetFocusPointDistance();
    }

    private void HandleZoom()
    {
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        Vector3 targetDirection = transform.forward * zoomInput * zoomSpeed;
        Vector3 targetPosition = transform.position + targetDirection;
        
        if(transform.position.y < minZoom && zoomInput > 0)
          return;
        if(transform.position.y > maxZoom && zoomInput < 0)
          return;
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref zoomVelocity, smoothTime);
    }

    private float GetFocusPointDistance()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance))
        {
            return hit.distance;
        }
        return maxDistance;
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButton(1))
        {
          float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
          float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

          pitch = Mathf.Clamp(pitch - verticalRotation, minPitch, maxPitch);

          transform.RotateAround(focusPoint.position, Vector3.up, horizontalRotation);
          transform.RotateAround(focusPoint.position, transform.right, pitch - transform.eulerAngles.x);
          transform.LookAt(focusPoint);
        }
    }

    private void HandleMovement()
    {
       Vector3 targetPosition = transform.position;
       float Inputx = Input.GetAxisRaw("Horizontal");
       float Inputz = Input.GetAxisRaw("Vertical");

       Vector3 floatForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;

       if(Inputz < 0 )
         targetPosition -= floatForward * movementSpeed * Time.deltaTime;
       if(Inputz > 0)
         targetPosition += floatForward * movementSpeed * Time.deltaTime;

        if(Inputx < 0)
         targetPosition -= transform.right * movementSpeed * Time.deltaTime;
        if(Inputx > 0)
         targetPosition += transform.right * movementSpeed * Time.deltaTime;

        // ✅ SINIR KONTROLÜ: Harita merkezinden boundaryRadius dışına çıkmayı engelle
        targetPosition = ClampPositionToBoundary(targetPosition);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref movementVelocity, smoothTime);
    }
     
    // Kameranın merkez noktadan belirli bir yarı çap içinde olmasını sağlar
    private Vector3 ClampPositionToBoundary(Vector3 targetPosition)
    {
        
        Vector3 direction = targetPosition - mapCenter.position;
        float distance = direction.magnitude;

        if (distance > boundaryRadius)
        {
            targetPosition = mapCenter.position + direction.normalized * boundaryRadius;
        }

        return targetPosition;
    }
}