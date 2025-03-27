using System;
using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;    
    public FirebaseUser User;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;
    
    // Password Reset variables
    [Header("Password Reset")]
    public TMP_InputField resetEmailField;
    public TMP_Text resetFeedbackText;
    
    [System.Serializable]
    public class UserData
    {
        public string _id;
        public string uid;
        public string username;
        public int level;
        public int xp;
        public int wins;
        public int profile_icon;
        public int streak_count;
        public string[] achievements;
        public int __v;
    }

    [System.Serializable]
    public class UserWrapper
    {
        public UserData user;
    }
    [System.Serializable]
    private class UsernameCheckResult
    {
        public bool exists;
    }
    
    //have firebase running
    void Awake()
    {
        //check that all necessary dependencies for Firebase are there on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }
    private void InitializeFirebase()
    {
        // set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }
    //Function for the login button
    public void LoginButton()
    {
        // log in button with email and password fields
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    // register button with email password and username fields
    public void RegisterButton()
    {
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }
    //Function to send password reset to email
    public void SendPasswordResetEmail()
    {
        string email = resetEmailField.text;

        if (string.IsNullOrEmpty(email))
        {
            resetFeedbackText.text = "Please enter your email.";
            return;
        }
		// sends reset email to email
        auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error sending password reset email: " + task.Exception);
                resetFeedbackText.text = "Not A Valid Email";
            }
            Debug.Log("Password reset email sent successfully.");
            resetFeedbackText.text = "Password reset email sent! Check your inbox.";
        });
    }
    //Function for user to log in
    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        Task<AuthResult> LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result.User;
            Debug.Log($"User signed in: {User.Email}");
            
            // retrieve Firebase ID Token
            Task<string> tokenTask = User.TokenAsync(true);
            yield return new WaitUntil(predicate: () => tokenTask.IsCompleted);

            if (tokenTask.Exception != null)
            {
                Debug.LogError("Failed to get ID Token: " + tokenTask.Exception);
            }
            else
            {
                string idToken = tokenTask.Result;
                Debug.Log("Firebase ID Token: " + idToken);

                // send this token to MongoDB Backend
                StartCoroutine(SendTokenToBackend(idToken));
            }
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";
        }
    }
    // send the ID token to server.js to authenticate it from firebase
    private IEnumerator SendTokenToBackend(string idToken)
    {
        string apiUrl = "http://localhost:3000/userdata";

        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        //  properly set the Authorization header
        request.SetRequestHeader("Authorization", "Bearer " + idToken);
        request.SetRequestHeader("Content-Type", "application/json"); // Ensures correct format

        Debug.Log("Sending token to backend: " + idToken);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching user data: " + request.error);
            Debug.LogError("Server Response: " + request.downloadHandler.text);
        }
        else
        {
            UserWrapper userWrapper = JsonUtility.FromJson<UserWrapper>(request.downloadHandler.text);
            UserData user = userWrapper.user;

            // Save user data to static class in usersession.cs
            UserSession.Username = user.username;
            UserSession.Level = user.level;
            UserSession.XP = user.xp;

            // Load profile scene
            SceneManager.LoadScene("profile");
        }
    }
    //Function for users to register
    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            // if username is blank
            warningRegisterText.text = "Missing Username";
        }
        else if(passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            // if passwords dont match
            warningRegisterText.text = "Password Does Not Match!";
        }
        else 
        {
            // checks if username is taken in mongodb
            bool usernameTaken = true;
            yield return StartCoroutine(CheckUsernameExists(_username, result => usernameTaken = result));

            if (usernameTaken)
            {
                warningRegisterText.text = "Username Already Taken!";
                yield break;
            }
            //Call the Firebase auth signin function passing the email and password
            Task<AuthResult> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                // checks for user errors
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Invalid Email";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has been created
                //Now get the result
                User = RegisterTask.Result.User;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile{DisplayName = _username};

                    //Call the Firebase auth update user profile function passing the profile with the username
                    Task ProfileTask = User.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        // debug code for errors
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        // return to sign in screen
                        UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                        warningLoginText.text = "";
                        confirmLoginText.text = "Registered!";
                    }
                    
                    // Retrieve Firebase ID Token to register the user in mongodb
                    Task<string> tokenTask = User.TokenAsync(true);
                    yield return new WaitUntil(() => tokenTask.IsCompleted);

                    if (tokenTask.Exception != null)
                    {
                        Debug.LogError("Failed to get ID Token: " + tokenTask.Exception);
                        yield break;
                    }

                    string idToken = tokenTask.Result;
                    Debug.Log("Firebase ID Token: " + idToken);

                    // Send username + token to backend/mongodb
                    StartCoroutine(SendUserDataToBackend(idToken, _username));
                }
            }
        }
    }
    //Sends user data from unity to mongodb
    private IEnumerator SendUserDataToBackend(string idToken, string username)
    {
        string apiUrl = "http://localhost:3000/register";

        // JSON Serialization
        UserData userData = new UserData { username = username };
        string jsonData = JsonUtility.ToJson(userData);

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Authorization", "Bearer " + idToken);
        request.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("Sending user data to backend...");
        Debug.Log("JSON Payload: " + jsonData);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error saving user: " + request.error);
            Debug.LogError("Server Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("User saved successfully: " + request.downloadHandler.text);
        }
    }
    // checks if username exists
    private IEnumerator CheckUsernameExists(string username, Action<bool> callback)
    {
        string uri = $"http://localhost:3000/check-username?username={UnityWebRequest.EscapeURL(username)}";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error checking username: " + webRequest.error);
                callback(true); // fallback to "exists" to be safe
            }
            else
            {
                // Parse the JSON response
                string json = webRequest.downloadHandler.text;
                var result = JsonUtility.FromJson<UsernameCheckResult>(json);
                callback(result.exists);
            }
        }
    }
}