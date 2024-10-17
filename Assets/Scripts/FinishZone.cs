using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FinishZone : MonoBehaviour
{
    public float suckSpeed = 5f; // Скорость засасывания персонажей
    public Transform suckTarget; // Точка, куда персонажи будут засасываться (центр финиша)

    private List<Transform> charactersInZone = new List<Transform>(); // Список всех персонажей в зоне

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            charactersInZone.Add(other.transform); // Добавляем персонажа в список
            StartCoroutine(SuckCharacter(other.transform));
        }
    }

    // Засасываем персонажа к центру
    IEnumerator SuckCharacter(Transform character)
    {
        while (Vector3.Distance(character.position, suckTarget.position) > 0.1f)
        {
            character.position = Vector3.MoveTowards(character.position, suckTarget.position, suckSpeed * Time.deltaTime);
            yield return null; // Ждем следующий кадр
        }

        // Деактивируем персонажа, когда он достиг цели
        character.gameObject.SetActive(false);
    }

    // Метод для проверки, засосало ли всех персонажей
    public bool AreAllCharactersSucked()
    {
        foreach (Transform character in charactersInZone)
        {
            if (character.gameObject.activeSelf)
            {
                return false; // Если хотя бы один персонаж активен, возвращаем false
            }
        }
        return true; // Все персонажи засосаны
    }
}
