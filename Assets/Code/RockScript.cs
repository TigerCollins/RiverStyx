using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RockScript : MonoBehaviour
{
    public List<GameObject> validNextRocks;
    public Material defaultMat, activeMaterial, readyMaterial;
    public int rockState = 0;
    public RockScript lastRock, nextRock;
    public List<GameObject> allLogs;
    public bool isLastRock;

    private void Awake()
    {
        defaultMat = GetComponent<Renderer>().material;
    }
    public void ChangeMaterial(Material newMaterial)
    {
        GetComponent<Renderer>().material = newMaterial;
    }
    public void ActivateLog(GameObject incomingRock)
    {
        Debug.Log("We activatin'");
        float closestLog = float.PositiveInfinity;
        int whichLog = -1;
        for (int i = 0; i < allLogs.Count; i++)
        {
            if (Vector3.Distance(allLogs[i].transform.position, incomingRock.transform.position) < closestLog)
            {
                closestLog = Vector3.Distance(allLogs[i].transform.position, incomingRock.transform.position);
                whichLog = i;
            }
        }
        Debug.Log("Closest log is " + whichLog);
        Debug.Log("Distance was " + closestLog);
        allLogs[whichLog].GetComponent<MeshRenderer>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fairy") && nextRock != null)
        {
            other.GetComponent<FairyScript>().targetRock = nextRock;
            
        }
        else if (isLastRock)
        {
            other.GetComponent<FairyScript>().targetRock = validNextRocks[0].GetComponent<RockScript>();
        }
    }
}

