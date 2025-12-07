using UnityEngine;

public class Enemy_Visual : MonoBehaviour
{
    [SerializeField] private Transform visual;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private LayerMask whatIsGround;

    private void Update()
    {
        AlignWhithSlope();
    }

    private void AlignWhithSlope()
    {
        if(visual == null) return;

        if(Physics.Raycast(visual.position, Vector3.down, out RaycastHit hit , Mathf.Infinity, whatIsGround))
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            visual.rotation = Quaternion.Slerp(visual.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }


}
