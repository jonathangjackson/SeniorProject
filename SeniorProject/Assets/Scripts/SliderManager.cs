using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour, IPointerUpHandler
{
    private Slider armSlider;
    private void Start()
    {
        armSlider = this.gameObject.GetComponent<Slider>();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(armSlider.value < 0.5)
        {
            armSlider.value = 0.0f;
        }
        else
        {
            armSlider.value = 1.0f;
        }
    }
}
