using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
	public AudioMixer mainMixer;

	public Slider mainVolumeSlider;
	public Slider efxVolumeSlider;

	public Toggle mainVolumeMute;
	public Toggle efxVolumeMute;



	public void mainVolumeChange(float volume)
	{
		mainMixer.SetFloat("MainVolume", volume);
		if(volume != mainVolumeSlider.minValue)
		{
			mainVolumeMute.isOn = false;
		}
		if(volume == mainVolumeSlider.minValue && !mainVolumeMute.isOn)
		{
			mainVolumeMute.isOn = true;
		}
	}

	public void efxVolumeChange(float volume)
	{
		mainMixer.SetFloat("EFX", volume);
		if(volume != efxVolumeSlider.minValue)
		{
			efxVolumeMute.isOn = false;
		}
		if(volume == efxVolumeSlider.minValue && !efxVolumeMute.isOn)
		{
			efxVolumeMute.isOn = true;
		}
	}

	public void muteMain(bool toggle)
	{
		if(toggle == true)
		{
			mainVolumeSlider.value = mainVolumeSlider.minValue;
		}
		if(mainVolumeSlider.value == mainVolumeSlider.minValue && toggle == false)
		{
			mainVolumeSlider.value += 10;
		}
	}

	public void muteEFX(bool toggle)
	{
		if(toggle == true)
		{
			efxVolumeSlider.value = efxVolumeSlider.minValue;
		}
		if(efxVolumeSlider.value == efxVolumeSlider.minValue && toggle == false)
		{
			efxVolumeSlider.value += 10;
		}
	}

	public void setQuality(int qualityLevel)
	{
		QualitySettings.SetQualityLevel(qualityLevel);
	}

}
