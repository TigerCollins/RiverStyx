using UnityEngine;

public class RiverFlowScript : MonoBehaviour
{
    Material riverMat;
    private void Awake()
    {
        riverMat = GetComponentInChildren<Renderer>().material;
    }
    private void Update()
    {
        riverMat.mainTextureOffset += new Vector2(0, 0.1f) * Time.deltaTime;
    }
}
