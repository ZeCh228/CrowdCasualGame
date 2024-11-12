using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    public float speed = 5f; 
    public float followDistance = 1.5f;
    public List<Rigidbody> crowdMembers = new List<Rigidbody>(); 
    public static CrowdManager Instance;

    [SerializeField] private float repuls;
    [SerializeField] private ParticleSystem deathParticles;
    private EntityCounter entityCounter;
    Vector3 target = Vector3.zero;

    bool isNeedToStopCrowd = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        entityCounter = FindObjectOfType<EntityCounter>();
    }

    public bool IsLeader(GameObject member)
    {
        return false;
    }

   private void Update()
    {
        if (crowdMembers.Count == 0) return;

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                target = hit.point;
                target = new Vector3(target.x, 0, target.z);

                if (crowdMembers.Count > 0)
                {
                    MoveToTarget(crowdMembers[0].gameObject, target);
                }

                if (isNeedToStopCrowd) return;

                for (int i = 1; i < crowdMembers.Count; i++)
                {
                    MoveToTarget(crowdMembers[i].gameObject, target);
                }
            }
        }
        else if (target != Vector3.zero)
        {
            if (crowdMembers.Count > 0)
            {
                MoveToTarget(crowdMembers[0].gameObject, target);

                for (int i = 1; i < crowdMembers.Count; i++)
                {
                    MoveToTarget(crowdMembers[i].gameObject, target);
                }
            }
        }


    }

    private void MoveToTarget(GameObject follower, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - follower.transform.position);
        direction.y = 0;
        float distance = Vector3.Distance(follower.transform.position, new Vector3(targetPosition.x, follower.transform.position.y, targetPosition.z));

        if (distance > followDistance)
        {
            isNeedToStopCrowd = false;
            follower.GetComponent<Rigidbody>().velocity = direction.magnitude > 1 ? direction.normalized * speed : direction * speed;
        }
        else if (follower == crowdMembers[0] && distance <= 0.1f)
        {
            follower.GetComponent<Rigidbody>().velocity = Vector3.zero;
            follower.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        else if (follower != crowdMembers[0] && distance <= CalculateDistance())
        {
            follower.GetComponent<Rigidbody>().velocity = Vector3.zero;
            follower.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    private float CalculateDistance()
    {
        if (crowdMembers.Count <= 7) return 1.5f;
        else if (crowdMembers.Count <= 14) return 30f;
        else if (crowdMembers.Count <= 35) return 80f;

        return 0.1f;
    }

    private void AvoidOthers(GameObject follower)
    {
        foreach (Rigidbody member in crowdMembers)
        {
            if (member.gameObject != follower)
            {
                float distance = Vector3.Distance(follower.transform.position, member.transform.position);
                if (distance < followDistance)
                {
                    Vector3 repulsion = (follower.transform.position - member.transform.position).normalized;
                    Vector3 repulsionForce = new Vector3(repulsion.x, 0, repulsion.z) * repuls;
                    follower.GetComponent<Rigidbody>().velocity += repulsionForce;
                }
            }
        }
    }

    public void AddToCrowd(Rigidbody newMember)
    {
        crowdMembers.Add(newMember);
        entityCounter.UpdateEntityCount();
    }

    public void RemoveFromCrowd(GameObject character)
    {
        Rigidbody rb = character.GetComponent<Rigidbody>();

        if (crowdMembers.Contains(rb))
        {
            crowdMembers.Remove(rb);
            entityCounter.UpdateEntityCount();
            Debug.Log("Персонаж удален. Осталось персонажей: " + crowdMembers.Count);

            GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
            if (gameOverManager != null)
            {
                gameOverManager.CheckForGameOver();
            }
        }
        deathParticles.transform.position = rb.transform.position;
        deathParticles.Play();
        character.SetActive(false);  
    }
}

