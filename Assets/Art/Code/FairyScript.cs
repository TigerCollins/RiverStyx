using UnityEngine;

public class FairyScript : MonoBehaviour
{
    public RockScript targetRock;
    public float moveForce, forceFalloff;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
       
        float forceMultiplier = Mathf.Max(0f, moveForce - forceFalloff * Vector3.Distance(targetRock.transform.position, transform.position));
        Vector3 hoverPos = targetRock.transform.position + Vector3.up * 2.25f;
        rb.AddForce((hoverPos - transform.position) * forceMultiplier);
    }
}