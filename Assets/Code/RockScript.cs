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
    CapsuleCollider rockCollider;

    private void Awake()
    {
        defaultMat = GetComponent<Renderer>().material;
        rockCollider = GetComponent<CapsuleCollider>();
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
            if(Vector3.Distance(allLogs[i].transform.position, incomingRock.transform.position) < closestLog)
            {
                closestLog = Vector3.Distance(allLogs[i].transform.position, incomingRock.transform.position);
                whichLog = i;
            }
            Debug.Log("Closest log is " + whichLog);
            Debug.Log("Distance was " + closestLog);
            allLogs[whichLog].GetComponent<MeshRenderer>().enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fairy") && nextRock != null)
        {
            other.GetComponent<FairyScript>().targetRock = nextRock;
        }
    }
    public void DeactivateLog(int whichLog)
    {
        switch (whichLog)
        {
            default:
                break;
            case 1:
                allLogs[0].SetActive(false);
                break;
            case 2:
                allLogs[1].SetActive(false);
                break;
            case 3:
                allLogs[2].SetActive(false);
                break;
        }
    }
}

