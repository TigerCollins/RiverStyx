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
        Debug.Log(forceMultiplier);
        rb.AddForce((targetRock.transform.position - transform.position) * forceMultiplier);
    }
}
////y = mx + b
//var forceMultiplier = Mathf.Max(0f, MAX_FORCE - FALLOFF_RATE * Vector3.Distance(posA, posB);