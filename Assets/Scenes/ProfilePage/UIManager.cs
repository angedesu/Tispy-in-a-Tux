using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject forgotPasswordUI;
	public GameObject dimBackground;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
	// shows log in screen
    public void LoginScreen()
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
        forgotPasswordUI.SetActive(false);
    }
	// shows sign in screen
    public void RegisterScreen()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    }
	// shows forgot password screen
    public void ForgotScreen()
    {
        loginUI.SetActive(false);
        forgotPasswordUI.SetActive(true);
    }
	//exit out from sign in screen to profile screen
	public void Exit()
    {
        SceneManager.LoadScene("profile");
    	if (dimBackground != null)
    	{
        	dimBackground.SetActive(false);
    	}
    }
}