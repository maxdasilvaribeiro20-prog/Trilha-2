using UnityEngine;

public class BirdCircleFlight : MonoBehaviour
{
    public Transform center;          
    public float radius = 10f;
    public float speed = 5f;          
    public bool clockwise = true;
    public float bobAmount = 0.5f;    
    public float bobSpeed = 1f;
    public float bankAngle = 15f;     

    Vector3 centerPos;
    float angle, startY;

    void Start()
    {
        centerPos = center != null ? center.position : transform.position;
        startY = transform.position.y;
        angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
    }

    void Update()
    {
        float dir = clockwise ? -1f : 1f;
        angle += dir * (speed / radius) * Time.deltaTime;

        float y = startY + Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        transform.position = new Vector3(
            centerPos.x + Mathf.Cos(angle) * radius,
            y,
            centerPos.z + Mathf.Sin(angle) * radius);

        Vector3 tangent = new Vector3(-Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * dir;
        transform.rotation = Quaternion.LookRotation(tangent) * Quaternion.Euler(0f, 0f, dir * bankAngle);
    }
}