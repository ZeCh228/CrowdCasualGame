using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FinishZone : MonoBehaviour
{
    public float suckSpeed = 5f; 
    public Transform suckTarget; 
    private List<Transform> charactersInZone = new List<Transform>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            charactersInZone.Add(other.transform); 
            StartCoroutine(SuckCharacter(other.transform));
        }
    }

    
    IEnumerator SuckCharacter(Transform character)
    {
        while (Vector3.Distance(character.position, suckTarget.position) > 0.1f)
        {
            character.position = Vector3.MoveTowards(character.position, suckTarget.position, suckSpeed * Time.deltaTime);
            yield return null; 
        }

        
        character.gameObject.SetActive(false);
    }

    
    public bool AreAllCharactersSucked()
    {
        foreach (Transform character in charactersInZone)
        {
            if (character.gameObject.activeSelf)
            {
                return false; 
            }
        }
        return true;
    }
}
