using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OptionButton : MonoBehaviour
{
    [SerializeField] private string nextScene = "Options";
    [SerializeField] private string folder = "v0.3";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Options()
    {
        SceneManager.LoadScene("Scenes/"+ folder + "/" + nextScene, LoadSceneMode.Single);
    }
}
