using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class SliderText : MonoBehaviour {

	public void onSlide_Difficulty(float sliderDifficulty)
	{

	}

	public Slider mainSlider;
	public Text sliderText;
	
	public void Start()
	{
		//Adds a listener to the main slider and invokes a method when the value changes.
		mainSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
	}
	
	// Invoked when the value of the slider changes.
	public void ValueChangeCheck()
	{
		if (mainSlider.maxValue == 3000) {
			sliderText.text = "Difficulty: " + ((int)mainSlider.value / 100).ToString ();
		} else if (mainSlider.maxValue == 15) {
			sliderText.text = "Speed: " + ((int)mainSlider.value - 5).ToString ();

		}
	}




}
