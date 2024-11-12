using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; 
    public float distance = 10f;
    public float height = 5f;
    public float cameraSpeed = 2f;
    public float targetSwitchSpeed = 2f;

    private Vector3 targetPosition;

    private void LateUpdate()
    {
        if (target == null && CrowdManager.Instance.crowdMembers.Count > 0)
        {          
            target = CrowdManager.Instance.crowdMembers[0].transform;
        }

        if (target != null)
        {           
            Vector3 desiredPosition = target.position + new Vector3(0, height, -distance);

            transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);
        
            targetPosition = Vector3.Lerp(targetPosition, target.position, targetSwitchSpeed * Time.deltaTime);

            transform.LookAt(targetPosition);
        }
    }
}



