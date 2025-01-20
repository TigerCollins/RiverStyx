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
    public FairyScript fairy;
    void Start()
    {
        mainCamera = Camera.main;
        mouse = Mouse.current;
        allRocks = new List<RockScript>();
        fairy = GameObject.FindGameObjectWithTag("Fairy").GetComponent<FairyScript>();
        foreach (GameObject rock in GameObject.FindGameObjectsWithTag("Rock"))
        {
            allRocks.Add(rock.GetComponent<RockScript>());
        }
        fairy.targetRock = allRocks.Last();
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
            RockScript hitRock = hit.collider.gameObject.GetComponent<RockScript>();
            if(hitRock.rockState == 1)
            {
                Vector3 logPosition = hitRock.transform.position;
                logPosition.x += 2.5f;
                logPosition.y += 0.25f;
                Instantiate(logPrefab, logPosition, Quaternion.identity);
                ResetRocks();
                //Hardcoded for now, if multiple valid rocks will have to refactor
                hitRock.connectedRock = hitRock.validNextRocks[0].GetComponent<RockScript>();
                if(fairy.targetRock == hitRock)
                {
                    fairy.targetRock = hitRock.connectedRock;
                }  
            }
            else
            {
                ResetRocks();
                hitRock.rockState = 1;
                hitRock.ChangeMaterial(hitRock.activeMaterial);
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
