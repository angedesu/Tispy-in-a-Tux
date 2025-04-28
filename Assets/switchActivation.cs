using UnityEngine;

public class switchActivation : MonoBehaviour
{
    [SerializeField] GameObject gameObject1;
    [SerializeField] GameObject gameObject2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveSwitch() { //only 1 GameObject should be active at a time
        if (gameObject1.activeSelf) //gameObject1 is active
        {
            gameObject1.SetActive(false); //deactivate gameObject1
            gameObject2.SetActive(true); //activate the other game object (gameObject2)
        }
        else {
            gameObject2.SetActive(false); //deactivate gameObject2
            gameObject1.SetActive(true); //activate the other game object (gameObject1)
        }
    }

    public void DeactivateBoth() { // Deactivate both GameObjects
        gameObject1.SetActive(false);
        gameObject2.SetActive(false);
    }
}
