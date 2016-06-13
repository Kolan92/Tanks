using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TankView : MonoBehaviour {
    public Slider Slider;
    public Image FillImage;
    public Canvas Canvas;
    [HideInInspector]
    public float StartingHealth;

    private readonly Color FullHealthColor = Color.green;
    private readonly Color ZeroHealthColor = Color.red;


    public void UpdateHealth(float hitPoints) {
        Slider.value = hitPoints;
        FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, hitPoints / StartingHealth);
    }

    public void Disable() {
        Slider.enabled = false;
        FillImage.enabled = false;
        Canvas.enabled = false;
    }
}
