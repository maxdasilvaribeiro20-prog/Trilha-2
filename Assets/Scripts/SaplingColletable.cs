using UnityEngine;

public class SaplingColletable : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 60f;
    [SerializeField] private float floatAmplitude = 0.25f;
    [SerializeField] private float floatSpeed = 2f;


    Vector3 startPos;
    bool playerNear;
    void Start()
    {
        startPos = transform.position;
    }

    
    void Update()
    {
        transform.Rotate(0f, rotateSpeed* Time.deltaTime,0f,Space.World);
        float y = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
         transform.position = startPos + Vector3.up * y;
    }
        void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
         GameManager.Instance.AddSeed();  
        GameManager.Instance.HidePrompt();
        Destroy(gameObject);
        
    }
}
