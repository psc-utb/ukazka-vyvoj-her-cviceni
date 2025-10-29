using UnityEngine;

public class GhoulAI : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    GameObject hands;

    [SerializeField]
    GameObject player;

    // Awake is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        AttackSMB attack = animator.GetBehaviour<AttackSMB>();
        attack.Hands = hands;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        animator.SetFloat("PlayerDistance", distance);
    }

    public void PlayerIsVisible(bool isVisible)
    {
        animator.SetBool("PlayerVisible", isVisible);
    }
}
