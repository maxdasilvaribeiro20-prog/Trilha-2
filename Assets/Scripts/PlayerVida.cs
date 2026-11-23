using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerVida : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private int vidaMaxima = 3;                // igual ao Max Health do HealthController
    [SerializeField] private float tempoInvulneravel = 1.5f;    // tempo sem levar dano depois de ser atingido
    [SerializeField] private float tempoParaReiniciar = 1.5f;

    [Header("Heart System (ligue o HealthController aqui)")]
    public UnityEvent<float> aoMudarVida;                       // avisa a vida atual toda vez que ela muda

    private int vida;
    private bool invulneravel;

    void Start()
    {
        vida = vidaMaxima;
        aoMudarVida.Invoke(vida);
    }

    public void LevarDano(int dano)
    {
        if (invulneravel || vida <= 0) return;

        vida = Mathf.Max(0, vida - dano);
        aoMudarVida.Invoke(vida);

        if (vida <= 0)
            StartCoroutine(Morrer());
        else
            StartCoroutine(Invulnerabilidade());
    }

    IEnumerator Invulnerabilidade()
    {
        invulneravel = true;
        yield return new WaitForSeconds(tempoInvulneravel);
        invulneravel = false;
    }

    IEnumerator Morrer()
    {
        invulneravel = true;
        Debug.Log("O jogador morreu");
        yield return new WaitForSeconds(tempoParaReiniciar);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}