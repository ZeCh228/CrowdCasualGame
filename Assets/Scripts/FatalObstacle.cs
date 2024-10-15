using UnityEngine;

public class FatalObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ���������, ��� ������, �������� � �������, ����� ��� Player
        if (other.CompareTag("Player"))
        {
            // ������� ��������� �� �����
            RemoveFromCrowd(other.gameObject);
        }
    }

    void RemoveFromCrowd(GameObject character)
    {
        // ������� ��������� �� ������ ����� � CrowdManager
        if (CrowdManager.Instance.crowdMembers.Contains(character.GetComponent<Rigidbody>()))
        {
            CrowdManager.Instance.crowdMembers.Remove(character.GetComponent<Rigidbody>());
        }

        // ���������� ��������� (����� �������� �������� ������������ ��� ������)
        Destroy(character);
    }
}
