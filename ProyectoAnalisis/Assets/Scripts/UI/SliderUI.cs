using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class SliderUI : MonoBehaviour {

    private UnityEngine.UI.Slider slider;
    private TextMeshProUGUI value;
    private void Start()
    {
        slider = GetComponent<UnityEngine.UI.Slider>();
        value = transform.Find("Handle Slide Area/Handle/Value").GetComponent<TextMeshProUGUI>();
        slider.onValueChanged.AddListener(delegate { SetValue(); });
        SetValue();
    }

    public void SetValue()
    {
        value.text = slider.value.ToString();

    }
}
