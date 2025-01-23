using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

public class RockControllerScript : MonoBehaviour
{
    Camera mainCamera;
    Mouse mouse;
    public List<RockScript> allRocks;
    public GameObject logPrefab;
    public List<FairyScript> fairyList;
    public StickPileScript stickPile;
    public static RockControllerScript rockInstance;
    public string nextLevelName;
    public int scoreToWin;
    void Awake()
    {
        rockInstance = this;
    }
    void Start()
    {
        mainCamera = Camera.main;
        mouse = Mouse.current;
        allRocks = new List<RockScript>();
        foreach (GameObject fairy in GameObject.FindGameObjectsWithTag("Fairy"))
        {
            fairyList.Add(fairy.GetComponent<FairyScript>());
        }
        foreach (GameObject rock in GameObject.FindGameObjectsWithTag("Rock"))
        {
            allRocks.Add(rock.GetComponentInChildren<RockScript>());
        }
        foreach(FairyScript fairy in fairyList)
        {
            RockScript closestRock = allRocks.OrderBy(rock => Vector3.Distance(rock.transform.position, fairy.transform.position)).First();
            fairy.targetRock = closestRock;
        }
    }
    void Update()
    {
        if (mouse.leftButton.wasPressedThisFrame)
        {
            RockClicked();
        }
        if (mouse.rightButton.wasPressedThisFrame)
        {
            ResetRocks();
        }
    }
    public void RockClicked()
    {
        Vector3 mousePosition = mouse.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.CompareTag("Rock"))
        {
            //grab the rock script we clicked on
            RockScript hitRock = hit.collider.gameObject.GetComponent<RockScript>();
            //If the rock was orange as a valid option to place a bridge, we need to place the bridge
            if (hitRock.rockState == 2 && stickPile.sticksRemaining > 0)
            {
                for (int i = 0; i < allRocks.Count; i++)
                {
                    //find the rock that was clicked that made this rock valid as a building spot
                    if (allRocks[i].rockState == 1)
                    {
                        //tell the rock this rock is the one we came from
                        hitRock.lastRock = allRocks[i];
                    }
                }
                //activate the log bridge on hit rocks last rock
                hitRock.lastRock.ActivateLog(hitRock.gameObject);
                hitRock.lastRock.nextRock = hitRock;
                //then tell the fairies to go to the new rock
                /*foreach (FairyScript fairy in fairyList)
                { 
                    if(fairy.targetRock == hitRock.lastRock)
                    {
                        fairy.targetRock = hitRock;
                    }
                }*/
                ResetRocks();
                stickPile.RemoveStick();
            }
            //if we did hit a rock but we couldn't build a bridge
            else if(stickPile.sticksRemaining > 0)
            {
                //reset all rocks to default
                ResetRocks();
                //set the rock we clicked on to become the base of the next bridge
                hitRock.rockState = 1;
                hitRock.ChangeMaterial(hitRock.activeMaterial);
                //tell the rocks next valid rocks to become active
                foreach (GameObject rock in hitRock.validNextRocks)
                {
                    RockScript validRock = rock.GetComponent<RockScript>();
                    validRock.ChangeMaterial(validRock.readyMaterial);
                    validRock.rockState = 2;
                }
            }
        }
    }
    public void ResetRocks()
    {
        foreach (RockScript rock in allRocks)
        {
            rock.ChangeMaterial(rock.defaultMat);
            rock.rockState = 0;
        }
    }
}
