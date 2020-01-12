using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class MarbleSkins : MonoBehaviour, IPointerDownHandler 
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

	public virtual void OnPointerDown(PointerEventData ped)
	{
		ToggleSkin();
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
