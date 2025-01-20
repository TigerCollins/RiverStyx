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
    public RockScript connectedRock;

    private void Awake()
    {
        defaultMat = GetComponent<Renderer>().material;
    }
    public void ChangeMaterial(Material newMaterial)
    {
        GetComponent<Renderer>().material = newMaterial;
    }
    
}

