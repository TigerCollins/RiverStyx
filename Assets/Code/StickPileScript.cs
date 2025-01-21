using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StickPileScript : MonoBehaviour
{
    public List<GameObject> sticks = new List<GameObject>();
    public int sticksRemaining;
    public void RemoveStick()
    {
        sticks[sticksRemaining-1].SetActive(false);
        sticksRemaining--;
    }
    public void AddStick()
    {
        sticks[sticksRemaining].SetActive(true);
        if(sticksRemaining < sticks.Count)
        {
            sticksRemaining++;
        }
    }
}
