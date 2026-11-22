using UnityEngine;

public class GolemProximidade : MonoBehaviour
{
    private Animator animator;
    private bool jaAcenou = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>(); //Para pegar o componente animator de Golem
    }
    void OnTriggerEnter(Collider other)
    {
        if (!jaAcenou && other.CompareTag("Player"))
        {
            jaAcenou = true;
            animator.SetTrigger("Acenar");
        }
    }
}