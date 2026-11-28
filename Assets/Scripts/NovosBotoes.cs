using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NovosBotoes : MonoBehaviour
{
    public Button botaoContinuar;
    [SerializeField] int proximaCena;

    private bool carregando;

    void Start()
    {
        // liga o botão por código (não precisa do On Click no Inspector)
        if (botaoContinuar != null)
            botaoContinuar.onClick.AddListener(Continuar);
    }

    void Update()
    {
        Keyboard teclado = Keyboard.current;
        if (teclado == null) return;

        // Enter (principal ou do teclado numérico) faz o mesmo que clicar no botão
        if (teclado.enterKey.wasPressedThisFrame || teclado.numpadEnterKey.wasPressedThisFrame)
            Continuar();
    }

    public void Continuar()
    {
        if (carregando) return; // evita carregar a cena duas vezes
        carregando = true;
        SceneManager.LoadScene(proximaCena);
    }
}