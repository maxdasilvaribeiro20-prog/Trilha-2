using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class BotaoContinuar : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip somTiro;
    public TMP_Text textoHistoria;
    public Button botaoContinuar;


    [TextArea(3, 5)]
    public string[] falas;

    private int indice = 0;
    private int frameDoUltimoAvanco = -1;

    void Start()
    {
        MostrarTexto();
    }

    void Update()
    {
        Keyboard teclado = Keyboard.current;
        if (teclado == null) return;

        // Enter (principal ou do teclado numérico) faz o mesmo que o botão Continuar
        if (teclado.enterKey.wasPressedThisFrame || teclado.numpadEnterKey.wasPressedThisFrame)
            AvancarTexto();
    }

    public void AvancarTexto()
    {
        // evita avançar duas vezes no mesmo frame (por exemplo, Enter com o botão selecionado)
        if (Time.frameCount == frameDoUltimoAvanco) return;
        frameDoUltimoAvanco = Time.frameCount;

        indice++;

        if (indice < falas.Length)
        {
            MostrarTexto();
        }
        else
        {
            SceneManager.LoadScene("Puzzle1");
        }
    }

    void MostrarTexto()
    {
        textoHistoria.text = falas[indice];

        if (indice == 6 && audioSource != null && somTiro != null)
        {
            audioSource.PlayOneShot(somTiro);
        }
    }
}