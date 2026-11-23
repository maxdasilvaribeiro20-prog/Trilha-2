using UnityEngine;
using UnityEngine.AI;

public class ComadreScript : MonoBehaviour
{
    [SerializeField] private int vida = 50;
    [SerializeField] private float distanciaAtaque = 1.5f;

    private NavMeshAgent agente;
    private Transform player;
    private PlayerVida vidaJogador;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        vidaJogador = player.GetComponent<PlayerVida>();
    }

    void Update()
    {
        if (!agente.isOnNavMesh) return;

        agente.SetDestination(player.position);

        if (Vector3.Distance(transform.position, player.position) <= distanciaAtaque)
            vidaJogador.LevarDano(1);
    }
}