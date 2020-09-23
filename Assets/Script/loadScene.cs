using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour 
{

    private bool isLoadScene = false;

    [SerializeField]
    private string scene;
    [SerializeField]
    private Text loadingText;


    // Updates once per frame
    void Update() 
	{
        // If the new scene has started loading...
        if (isLoadScene == true) {

            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));

        }

    }

	public void gotoLoadScene(string sceneName)
	{
		scene = sceneName;

		isLoadScene = true;

		// ...change the instruction text to read "Loading..."
		loadingText.text = "Loading...";

		// ...and start a coroutine that will load the desired scene.
		StartCoroutine("LoadNewScene");
	}
    public void loadWithDelay(string sceneName)
	{
        scene = sceneName;
		Invoke("gotoLoadSceneS",0.5f);
	}
    
    void gotoLoadSceneS()
	{
		isLoadScene = true;

		// ...and start a coroutine that will load the desired scene.
		StartCoroutine("LoadNewScene");
	}



    


    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene() 
	{
		

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone) 
		{
            yield return null;
        }

    }
}
