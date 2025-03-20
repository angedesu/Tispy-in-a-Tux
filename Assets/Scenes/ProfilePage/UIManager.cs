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

    //Functions to change the login screen UI
    public void LoginScreen() //Back button
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
        forgotPasswordUI.SetActive(false);
    }

    public void RegisterScreen() // Regester button
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    }

    public void ForgotScreen() //
    {
        loginUI.SetActive(false);
        forgotPasswordUI.SetActive(true);
    }
	
	public void Exit() //Back button
    {
        SceneManager.LoadScene("profile");
    	if (dimBackground != null)
    	{
        	dimBackground.SetActive(false); // Hide dim background
    	}
    }
}