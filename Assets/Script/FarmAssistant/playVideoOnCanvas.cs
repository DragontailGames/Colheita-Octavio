using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class playVideoOnCanvas : MonoBehaviour 
{
	public RawImage videoRaw;
	private VideoPlayer videoPlayer;
	private AudioSource audioSource;

  	void Start () 
  	{
		videoPlayer = GetComponent<VideoPlayer>();
		audioSource = GetComponent<AudioSource>();
		StartCoroutine(PlayVideo());

  	}

	public void StartVideo(VideoClip clipAux)
	{
		videoPlayer.clip = clipAux;
		videoRaw.transform.gameObject.SetActive(true);
		Debug.Log("Teste 1 - " + clipAux.name);
		videoRaw.color = Color.white;
	}
	  
  	IEnumerator PlayVideo()
	{
		videoPlayer.Prepare();
		yield return new WaitForSeconds(0.6f);
		videoRaw.texture = videoPlayer.texture;
		Debug.Log(" --- " + videoPlayer.clip.name);
		videoPlayer.Play();
		audioSource.Play();
	}
}
