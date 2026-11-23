
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TMP_Text alertText;
    [SerializeField] private TMP_Text seedText;
    [SerializeField] private int arvoresNecessarias = 3;
    [SerializeField] private int arvoresPlantadas;
    //passaros
    [SerializeField] private TMP_Text birdText;
    [SerializeField] private TMP_Text winText;


    [SerializeField] private string[] ordemPassaros = { "azul", "vermelho", "amarelo" };

    [SerializeField] private int birds; // quantos já foram capturados na ordem certa

    private readonly List<BirdColletable> capturados = new List<BirdColletable>();
    private Coroutine rotinaMensagem;

    //Golem
    [SerializeField] private TMP_Text golemText;

    [SerializeField] private int totalGolens = 3;
    [SerializeField] private int golens;
    [SerializeField] private int cenaAposGolens = -1;
    public int seeds;

    void Awake()
    {
        Instance = this;
        HidePrompt();
        UpdateSeedsUI();

        UpdateBirdUI();
        UpdateGolemUI();
        if (winText != null)
        {
            winText.gameObject.SetActive(false);
        }
    }

    // ---------- Mudas ----------
    public void AddSeed()
    {
        seeds++;
        UpdateSeedsUI();
    }

    public bool UseSeed()
    {
        if (seeds <= 0) return false;
        seeds--;
        UpdateSeedsUI();
        return true;
    }

    void UpdateSeedsUI()
    {
        if (seedText != null) seedText.text = "Mudas: " + seeds;
    }


    public void ShowPrompt(string message)
    {
        if (alertText == null) return;
        alertText.text = message;
        alertText.gameObject.SetActive(true);
    }

    public void HidePrompt()
    {
        if (alertText == null) return;
        alertText.gameObject.SetActive(false);
    }

    public void RegistrarArvorePlantada()
    {
        arvoresPlantadas++;


        if (arvoresPlantadas >= arvoresNecessarias)
        {

            SceneManager.LoadScene(4);
        }
    }


    //passaro

    public void CapturarPassaro(BirdColletable p)
    {
        if (string.Equals(p.id.Trim(), ordemPassaros[birds].Trim(),
                System.StringComparison.OrdinalIgnoreCase))
        {
            birds++;
            capturados.Add(p);
            p.gameObject.SetActive(false);
            UpdateBirdUI();

            if (birds >= ordemPassaros.Length)
            {
                MostrarMensagemPassaro("Você encontrou todos os pássaros na ordem certa!", 0f);

                SceneManager.LoadScene(3);
            }
            else
            {
                MostrarMensagemPassaro("Certo! " + birds + "/" + ordemPassaros.Length, 2f);
            }
        }
        else
        {
            ResetarPassaros();
            MostrarMensagemPassaro("Ordem errada! Recomeçando...", 2.5f);
        }
    }

    void ResetarPassaros()
    {
        birds = 0;

        foreach (BirdColletable p in capturados)
            p.gameObject.SetActive(true);

        capturados.Clear();
        UpdateBirdUI();
    }

    void UpdateBirdUI()
    {
        if (birdText == null) return;

        if (birds >= ordemPassaros.Length)
            birdText.text = "Pássaros: " + birds + "/" + ordemPassaros.Length;
        else
            birdText.text = "Pássaros: " + birds + "/" + ordemPassaros.Length
                        + "\nPróximo: " + ordemPassaros[birds];
    }

    //golem
    public void AddGolem(GolemColletable g)
    {
        if (golens >= totalGolens) return; // já encontrou todos

        golens++;
        UpdateGolemUI();

        if (golens >= totalGolens)
        {
            MostrarMensagemPassaro("Você encontrou todos os golens!", 0f);

                
               
               //yield return new WaitForSeconds(3);
                //SceneManager.LoadScene(indice);
        }
        else
        {
            MostrarMensagemPassaro("Golem encontrado! " + golens + "/" + totalGolens, 2f);
        }
    }

    void UpdateGolemUI()
    {
        if (golemText == null) return;
        golemText.text = "Golens: " + golens + "/" + totalGolens;
    }

    void MostrarMensagemPassaro(string msg, float segundos)
    {
        if (rotinaMensagem != null) StopCoroutine(rotinaMensagem);

        winText.text = msg;
        winText.gameObject.SetActive(true);

        if (segundos > 0f)
            rotinaMensagem = StartCoroutine(EsconderMensagem(segundos));
    }

    IEnumerator EsconderMensagem(float segundos)
    {
        if (winText != null)
        {
            yield return new WaitForSeconds(segundos);
            winText.gameObject.SetActive(false);
        }
    }
}