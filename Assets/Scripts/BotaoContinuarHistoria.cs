
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogoHistoria : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip somTiro;
    public TMP_Text textoHistoria;
    public Button botaoContinuar;

    [TextArea(3, 5)]
    public string[] falas;

    private int indice = 0;

    void Start()
    {
        MostrarTexto();
    }

    public void AvancarTexto()
    {
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

        if (indice == 1)
    {
        audioSource.PlayOneShot(somTiro);
    }
    }
}
