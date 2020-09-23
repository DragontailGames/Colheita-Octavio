using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using UnityEngine.Events;
 
public class facebookManager : MonoBehaviour {
 
    public GameObject model;
 
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                    Debug.LogError("Couldn't initialize");
            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });
        }
        else
            FB.ActivateApp();
    }
 
    #region Login / Logout
    public void FacebookLogin()
    {
        var permissions = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permissions);
    }

    public void FacebookLogout()
    {
        FB.LogOut();
    }
    #endregion

    bool checkLogin = true;

    void Update()
    {
        if(checkLogin && FB.IsLoggedIn)
        {
            checkLogin = false;
            StartCoroutine ("hasRequest");
            Debug.Log("Logou");
        }
    }
 
    public void FacebookShare()
    {
        FB.ShareLink(new System.Uri("https://resocoder.com"), "Check it out!",
            "Good programming tutorials lol!",
            new System.Uri("https://resocoder.com/wp-content/uploads/2017/01/logoRound512.png"));
    }
 
    #region Inviting
    public void FacebookGameRequest()
    {
        FB.AppRequest("Hey! Come and play this awesome game!", title: "Reso Coder Tutorial");
    }
 
    public void FacebookInvite()
    {
        FB.Mobile.AppInvite(new System.Uri("https://play.google.com/store/apps/details?id=com.tappybyte.byteaway"));
    }
    #endregion

	int c = 1;

	public Transform canvas;

	private Dictionary<string, RawImage> friends = new Dictionary<string, RawImage>();
 
    public void GetFriendsPlayingThisGame()
    {

        string query = "/me/friends";
        FB.API(query, HttpMethod.GET, result =>
        {
            var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
            var friendsList = (List<object>)dictionary["data"];
            foreach (var dict in friendsList)
			{
				GameObject aux = Instantiate(model,Vector2.zero,Quaternion.identity);
				aux.transform.SetParent(canvas);
				aux.transform.position = new Vector2(60,170 - 50*c);
				aux.transform.localScale = Vector3.one;
				aux.GetComponentInChildren<Text>().text = ""+((Dictionary<string, object>)dict)["name"];
                UnityAction _event = new UnityAction(auxMethod);
                _event = new UnityAction(() => sendAid(((Dictionary<string, object>)dict)["id"].ToString()));
                aux.GetComponentInChildren<Button>().onClick.AddListener(_event);
				friends.Add(((Dictionary<string, object>)dict)["id"].ToString(),aux.GetComponentInChildren<RawImage>());
				aux.transform.name = "Teste_" + c; 				
				c++;
			}
			
			getProfilePicture();
        });	
    }

    private void auxMethod(){}

    public IEnumerator hasRequest()
    {
        Debug.Log("https://graph.facebook.com/me/apprequests?access_token="
        +AccessToken.CurrentAccessToken.TokenString);
        WWW www = new WWW ("https://graph.facebook.com/me/apprequests?access_token="
        +AccessToken.CurrentAccessToken.TokenString);
        yield return www;

        Debug.Log(www.text);

    }

	private void getProfilePicture()
	{

		foreach (var item in friends)
		{
			FB.API ("/"+item.Key+"/picture?type=square&height=50&width=50", HttpMethod.GET, UpdateProfileImage =>
			{
				if(UpdateProfileImage.Texture != null) 
				{
					item.Value.texture = UpdateProfileImage.Texture;
				}
			});
		}

	}

	public void sendAid(string FriendID)
	{
		List<string> recipient = null;
        string title, message, data = string.Empty;

        title = "Ajuda de um amigo!";
        message = "Você esta precisando de uma ajuda, te enviei um evento";
        recipient = new List<string>(){ FriendID };
        Debug.Log(FriendID);
        data = "{\"event_type\"}";

        FB.AppRequest(
            message,
            recipient,
            null,
            null,
            null,
            data,
            title,
            AppRequestCallback
            );
    }

    // Callback for FB.AppRequest
    private static void AppRequestCallback (IAppRequestResult result)
    {
        // Error checking
        Debug.Log("AppRequestCallback");
        if (result.Error != null)
        {
            Debug.Log("adowdoawh");
            Debug.LogError(result.Error);
            return;
        }
        Debug.Log(result.RawResult);

        // Check response for success - show user a success popup if so
        object obj;
        if (result.ResultDictionary.TryGetValue ("cancelled", out obj))
        {
            Debug.Log("Request cancelled");
        }
        else if (result.ResultDictionary.TryGetValue ("request", out obj))
        {
            Debug.Log("Request sent");
        }
    }
}