using TMPro;
using UnityEngine;

public class EntityCounter : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    private void Start()
    {
        UpdateEntityCount();
    }

    public void UpdateEntityCount()
    {
        if (CrowdManager.Instance != null)
        {
            int count = CrowdManager.Instance.crowdMembers.Count;
            counterText.text = count.ToString();
        }
    }
}
