using UnityEngine;

public class LastRockScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fairy"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
