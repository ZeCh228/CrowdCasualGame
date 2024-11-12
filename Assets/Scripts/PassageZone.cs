using TMPro;
using UnityEngine;

public class PassageZone : MonoBehaviour
{
    public TextMeshProUGUI counterText; 
    public int maxPeople = 30;
    public GameObject obstacle;

    private int currentPeople = 0;

    private void Start()
    {
        UpdateCounterText();
        obstacle.SetActive(true);
    }

 
    private void UpdateCounterText()
    {
        counterText.text = currentPeople + " / " + maxPeople;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentPeople++;
            UpdateCounterText();

            
            if (currentPeople >= maxPeople)
            {
                AllowPassage();
            }
        }
    }

  
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentPeople--;
            UpdateCounterText();
        }
    }

    private void AllowPassage()
    {
        Debug.Log("ƒостигнуто требуемое количество людей");
        obstacle.SetActive(false);
    }
}
