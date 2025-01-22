using UnityEngine;

public class FairyAnimator : MonoBehaviour
{
    public Material wingDown, wingUp;
    public float timer, timeReset;
    public Renderer wingRenderer;
    public int wingState;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeReset)
        {
            timer = 0;
            ChangeWing();
        }
    }
    public void ChangeWing()
    {
        if (wingState == 0)
        {
            wingState = 1;
            wingRenderer.material = wingUp;
        }
        else
        {
            wingState = 0;
            wingRenderer.material = wingDown;
        }
        timeReset = Random.Range(0.25f, 0.45f);
    }
}
