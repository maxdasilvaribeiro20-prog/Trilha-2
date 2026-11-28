using System.Collections;
using TMPro;
using UnityEngine;

public class MensagensIniciais : MonoBehaviour
{
    [Header("Mensagem grande (centro da tela)")]
    [SerializeField] private TMP_Text textoGrande;
    [SerializeField] private string mensagemGrande = "Fuja da Comadre";
    [SerializeField] private float tempoMensagemGrande = 5f;

    [Header("Controles (canto da tela)")]
    [SerializeField] private TMP_Text textoControles;
    [SerializeField, TextArea(3, 6)] private string controles =
        "WASD - andar\nEspaço - pular\nShift - correr\nE - interagir";
    [SerializeField] private float tempoControles = 0f; // 0 = fica sempre na tela

    IEnumerator Start()
    {
        if (textoControles != null)
        {
            textoControles.text = controles;
            textoControles.gameObject.SetActive(true);
        }

        if (textoGrande != null)
        {
            textoGrande.text = mensagemGrande;
            textoGrande.gameObject.SetActive(true);

            yield return new WaitForSeconds(tempoMensagemGrande);

            textoGrande.gameObject.SetActive(false);
        }

        if (textoControles != null && tempoControles > 0f)
        {
            // o tempo dos controles conta a partir do início da cena
            float falta = tempoControles - tempoMensagemGrande;
            if (falta > 0f) yield return new WaitForSeconds(falta);

            textoControles.gameObject.SetActive(false);
        }
    }
}   