using System.Collections;
using UnityEngine;

public class GolemColletable : MonoBehaviour
{
    [SerializeField] private float tempoAntesDeSumir = 2f; // tempo para o aceno aparecer antes de o golem sumir

    private Animator animator;
    private bool coletado;

    void Start()
    {
        animator = GetComponentInChildren<Animator>(); // pega o Animator do golem
    }

    void OnTriggerEnter(Collider other)
    {
        if (coletado) return;

        bool ehPlayer = other.CompareTag("Player")
                        || other.GetComponentInParent<CharacterController>() != null;

        if (!ehPlayer) return;

        coletado = true;

        if (animator != null)
            animator.SetTrigger("Acenar");

        GameManager.Instance.AddGolem(this);
        StartCoroutine(SumirDepois());
    }

    IEnumerator SumirDepois()
    {
        yield return new WaitForSeconds(tempoAntesDeSumir);
        gameObject.SetActive(false);
    }
}