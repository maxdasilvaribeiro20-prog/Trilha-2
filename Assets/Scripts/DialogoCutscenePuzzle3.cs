using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogoCutscenePuzzle2 : MonoBehaviour
{
    public TMP_Text textoHistoria;
    public string cenaDepois;

    [TextArea(3, 5)]
    public string[] falas;

    private int indice = 0;

    void Start()
    {
        if (falas == null || falas.Length == 0)
        {
            Debug.LogError("Nenhuma fala configurada!");
            return;
        }

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
            SceneManager.LoadScene(cenaDepois);
        }
    }

    void MostrarTexto()
    {
        textoHistoria.text = falas[indice];
    }
}