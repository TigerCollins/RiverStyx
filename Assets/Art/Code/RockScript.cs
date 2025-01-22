using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class RockScript : MonoBehaviour
{
    public List<GameObject> validNextRocks;
    public Material defaultMat, activeMaterial, readyMaterial;
    public int rockState = 0;
    public RockScript lastRock, nextRock;
    public List<GameObject> allLogs;
    public bool isLastRock;
    public bool endGameWhenReached;
    bool endGameStarted;
    public int fairiesThatHavePassed, maxFairiesAllowed;
    RockControllerScript rockController;

    private void Awake()
    {
        defaultMat = GetComponentInChildren<Renderer>().material;
        rockController = FindFirstObjectByType<RockControllerScript>();
    }
    public void ChangeMaterial(Material newMaterial)
    {
        GetComponent<Renderer>().material = newMaterial;
    }
    public void ActivateLog(GameObject incomingRock)
    {
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
        allLogs[whichLog].GetComponentInChildren<MeshRenderer>().enabled = true;
    }
    public void DeactivateLog()
    {
        foreach (GameObject log in allLogs)
        {
           // log.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fairy"))
        {
            fairiesThatHavePassed++;
            if (nextRock != null)
            {
                other.GetComponent<FairyScript>().targetRock = nextRock;
            }
            else if (isLastRock)
            {
                other.GetComponent<FairyScript>().targetRock = validNextRocks[0].GetComponent<RockScript>();
                if(MetaScript.metaInstance != null)
                {
                    MetaScript.metaInstance.SetScore(MetaScript.metaInstance.GetScore() + 1);
                }
                    
            }

            if(endGameWhenReached & !endGameStarted)
            {
                StartCoroutine(TryEndLevel());
            }
        }
        if (fairiesThatHavePassed >= maxFairiesAllowed && lastRock)
        {
            fairiesThatHavePassed = 0;
            lastRock.DeactivateLog();
            //rockController.stickPile.AddStick();
            lastRock = null;
            
        }
    }

    IEnumerator TryEndLevel()
    {
        if(!endGameStarted)
        {
            endGameStarted = true;
            yield return new WaitForSeconds(1);

            if (fairiesThatHavePassed >= maxFairiesAllowed && MetaScript.metaInstance != null && RockControllerScript.rockInstance != null)
            {
                    Debug.Log("ahhh");
                    if (RockControllerScript.rockInstance.NextLevelName == "")
                    {
                        MetaScript.metaInstance.LoadLevel("GameLevel");
                    }

                    else
                    {
                        MetaScript.metaInstance.LoadLevel(RockControllerScript.rockInstance.NextLevelName);
                    }
            }

            else
            {
                Debug.Log("hmmm");
            }
        }
        //yield on a new YieldInstruction that waits for 5 seconds.
        //yield break;

    }
}

