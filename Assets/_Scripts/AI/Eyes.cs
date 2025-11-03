using UnityEngine;
using UnityEngine.Events;

public class Eyes : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    Transform playerHeadPosition;

    ConeCollider coneCollider;

    public UnityEvent<bool> PlayerIsVisible;

    // Awake is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        coneCollider = GetComponent<ConeCollider>();
    }

    void CheckVisibility(Collider other)
    {
        if (other.gameObject == player)
        {
            Vector3 position = transform.position;
            Vector3 positionPlayer = playerHeadPosition.transform.position;
            //positionPlayer.y = positionPlayer.y + 2;
            Vector3 direction = (positionPlayer - position).normalized;

            Ray ray = new Ray(position, direction);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, coneCollider.Distance, ~(LayerMask.GetMask("Eyes")));
            Debug.DrawRay(position, direction * coneCollider.Distance, Color.yellow);

            if (hit.collider?.gameObject == player)
            {
                PlayerIsVisible?.Invoke(true);
            }
            else
            {
                PlayerIsVisible?.Invoke(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckVisibility(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckVisibility(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            PlayerIsVisible?.Invoke(false);
        }
    }
}
