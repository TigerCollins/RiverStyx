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
        Vector3 randomTargetPos = targetRock.transform.position;
        randomTargetPos.x += Random.Range(-0.25f, 0.25f);
        randomTargetPos.z += Random.Range(-0.25f, 0.25f);
        float forceMultiplier = Mathf.Max(0f, moveForce - forceFalloff * Vector3.Distance(targetRock.transform.position, transform.position));

        rb.AddForce((randomTargetPos - transform.position) * forceMultiplier);
    }
}