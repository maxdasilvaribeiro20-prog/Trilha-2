using UnityEngine;

public class BirdColletable : MonoBehaviour
{
    public string id;

    void OnTriggerEnter(Collider other)
    {
        bool ehPlayer = other.CompareTag("Player")
                        || other.GetComponentInParent<CharacterController>() != null;

        if (ehPlayer)
            GameManager.Instance.CapturarPassaro(this);
    }
}