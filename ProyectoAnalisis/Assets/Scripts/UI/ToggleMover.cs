using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMover : MonoBehaviour {

    private Toggle toggle;

	void Start () {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate { setToggle(); });
        setToggle();
	}
	
	// Update is called once per frame
	public void setToggle()
    {
        Button[] btns = FindObjectsOfType<Button>();

        foreach (var item in btns)
        {
            item.interactable =!toggle.isOn;
        }
    }
}
