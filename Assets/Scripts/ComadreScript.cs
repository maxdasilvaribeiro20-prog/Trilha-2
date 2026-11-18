using UnityEngine;
using UnityEngine.AI;

public class ComadreScript : MonoBehaviour
{
    [SerializeField] private int vida = 50;

    private NavMeshAgent agente;
    private Transform player;

    void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
        else
            Debug.LogWarning("Nenhum objeto com a tag Player na cena.");
    }

    void Update()
    {
        if (player == null || !agente.isOnNavMesh) return;
        agente.SetDestination(player.position);
    }
}