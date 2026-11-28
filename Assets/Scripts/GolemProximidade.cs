using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GolemProximo : MonoBehaviour
{
    [SerializeField] private float tempoAntesDeSumir = 2f; // tempo para o aceno aparecer antes de o golem sumir

    private Animator animator;
    private bool playerNear;
    private bool jaAcenou;
    private bool coletado;

    void Start()
    {
        animator = GetComponentInChildren<Animator>(); // pega o Animator do golem
    }

    void Update()
    {
        if (playerNear && !coletado && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            Coletar();
    }

    void OnTriggerEnter(Collider other)
    {
        if (coletado || !EhPlayer(other)) return;

        playerNear = true;
        GameManager.Instance.ShowPrompt("Pressione E para coletar o golem");

        // acena uma vez quando o jogador chega perto
        if (!jaAcenou && animator != null)
        {
            jaAcenou = true;
            animator.SetTrigger("Acenar");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!EhPlayer(other)) return;

        playerNear = false;
        if (!coletado) GameManager.Instance.HidePrompt();
    }

    void Coletar()
    {
        coletado = true;
        playerNear = false;
        GameManager.Instance.HidePrompt();
        GameManager.Instance.AddGolem(this);
        StartCoroutine(SumirDepois());
    }

    IEnumerator SumirDepois()
    {
        yield return new WaitForSeconds(tempoAntesDeSumir);
        gameObject.SetActive(false);
    }

    bool EhPlayer(Collider other)
    {
        return other.CompareTag("Player")
               || other.GetComponentInParent<CharacterController>() != null;
    }
}