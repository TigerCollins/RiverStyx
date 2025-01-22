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
    public bool isLastRock, isFirstRock;
    public bool endGameWhenReached;
    bool endGameStarted;
    public int fairiesThatHavePassed, maxFairiesAllowed;
    public List<GameObject> fairyPrefabs;
    public List<FairyScript> fairyScripts;
    RockControllerScript rockController;
    public float timerCurrent, timerMax;
    int lastFairy;

    private void Awake()
    {
        defaultMat = GetComponentInChildren<Renderer>().material;
        rockController = FindFirstObjectByType<RockControllerScript>();
    }
    private void Update()
    {
        timerCurrent += Time.deltaTime;
        if (timerCurrent > timerMax)
        {
            if(nextRock && fairyScripts.Count > 0)
            {
                foreach (FairyScript fairy in fairyScripts)
                {
                    fairy.targetRock = nextRock;
                }
            }
            timerCurrent = 0f;
            if(isFirstRock && fairyScripts.Count <= 0)
            {
                int randNum = Random.Range(0, fairyPrefabs.Count);
                if(randNum == lastFairy)
                {
                    randNum = (randNum + 1) % fairyPrefabs.Count;
                }
                lastFairy = randNum;
                Vector3 spawnPos = transform.position;
                spawnPos.x -= 5f;
                spawnPos.y += 2.25f;
                GameObject newFairy = Instantiate(fairyPrefabs[randNum], spawnPos, Quaternion.identity);
               // newFairy.transform.GetChild(0).localScale = new Vector3(0.2f, 0.2f, 0.2f);
              //  newFairy.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
                newFairy.GetComponent<FairyScript>().targetRock = this;
                fairyScripts.Add(newFairy.GetComponent<FairyScript>());
            }
        }
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
            FairyScript newFairyScript = other.GetComponent<FairyScript>();
            if(!fairyScripts.Contains(newFairyScript))
            {
                fairyScripts.Add(newFairyScript);
            }
            if (nextRock != null)
            {
                newFairyScript.targetRock = nextRock;
            }
            else if (isLastRock)
            {
                newFairyScript.targetRock = validNextRocks[0].GetComponent<RockScript>();
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
    
    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Fairy left");
        if (other.CompareTag("Fairy"))
        {
            FairyScript newFairyScript = other.GetComponent<FairyScript>();
            fairyScripts.Remove(newFairyScript);
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
                    if (RockControllerScript.rockInstance.nextLevelName == "")
                    {
                        MetaScript.metaInstance.LoadLevel("GameLevel");
                    }

                    else
                    {
                        MetaScript.metaInstance.LoadLevel(RockControllerScript.rockInstance.nextLevelName);
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

