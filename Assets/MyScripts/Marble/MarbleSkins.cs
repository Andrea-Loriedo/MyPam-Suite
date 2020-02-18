using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class MarbleSkins : MonoBehaviour 
{
    [SerializeField] Material[] skins = new Material[11];
    public Material selectedSkin;
    int skinID;

    void Awake()
    {
        skinID = 0;
        selectedSkin = skins[skinID];
        gameObject.GetComponent<Renderer>().material = selectedSkin;
    }

    void ToggleSkin()
    {
        skinID++;

        if (skinID >= 11)
            skinID = 0;

        selectedSkin = skins[skinID];
        gameObject.GetComponent<Renderer>().material = selectedSkin;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ToggleSkin();
        }    
    }
}
