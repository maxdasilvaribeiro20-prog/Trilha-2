using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlantingSpot : MonoBehaviour
{
    public GameObject tree;        
    public GameObject marker;      
    public float growTime = 3f;

    bool playerNear, planted;
    Vector3 finalScale;

    void Start()
    {
        finalScale = tree.transform.localScale;
        tree.SetActive(false);
    }

    void Update()
    {
        if (!playerNear || planted) return;

        bool temMuda = GameManager.Instance.seeds > 0;

        if (temMuda)
            GameManager.Instance.ShowPrompt("Pressione E para plantar");
        else
            GameManager.Instance.ShowPrompt("Você precisa de uma muda");

        if (temMuda && Keyboard.current.eKey.wasPressedThisFrame)
            Plant();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerNear = false;
        GameManager.Instance.HidePrompt();
    }

    void Plant()
    {
        GameManager.Instance.UseSeed();
        planted = true;
        GameManager.Instance.HidePrompt();
        if (marker != null) marker.SetActive(false);
        StartCoroutine(Grow());
        GameManager.Instance.RegistrarArvorePlantada();
    }

    IEnumerator Grow()
    {
        tree.SetActive(true);
        float t = 0f;
        while (t < growTime)
        {
            t += Time.deltaTime;
            tree.transform.localScale = Vector3.Lerp(Vector3.zero, finalScale, Mathf.SmoothStep(0f, 1f, t / growTime));
            yield return null;
        }
        tree.transform.localScale = finalScale;
    }
}