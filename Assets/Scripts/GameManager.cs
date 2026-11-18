
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

        // ordem em que os pássaros devem ser capturados (use o mesmo "id" no script Passaro)
        [SerializeField] private string[] ordemPassaros = { "azul", "vermelho", "amarelo" };

        [SerializeField] private int birds; // quantos já foram capturados na ordem certa

        private readonly List<BirdColletable> capturados = new List<BirdColletable>();
        private Coroutine rotinaMensagem;

        public int seeds;

        void Awake()
        {
            Instance = this;
            HidePrompt();
            UpdateSeedsUI();

            UpdateBirdUI();
            winText.gameObject.SetActive(false);
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
            alertText.text = message;
            alertText.gameObject.SetActive(true);
        }

        public void HidePrompt()
        {
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
            yield return new WaitForSeconds(segundos);
            winText.gameObject.SetActive(false);
        }
    }