using UnityEngine;

public class FatalObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CrowdManager.Instance.RemoveFromCrowd(other.gameObject);
        }
    }
}
