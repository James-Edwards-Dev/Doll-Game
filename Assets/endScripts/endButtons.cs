using UnityEngine;
using UnityEngine.SceneManagement;

public class endButtons : MonoBehaviour
{
    public string mainScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tryButtonClick()
    {
        SceneManager.LoadScene(mainScene, LoadSceneMode.Single);
    }
}
