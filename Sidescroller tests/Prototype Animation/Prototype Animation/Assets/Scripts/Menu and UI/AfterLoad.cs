using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterLoad : MonoBehaviour
{
    [SerializeField] private string nextScene = "CutScene";
    [SerializeField] private string folder = "v0.3";

    [SerializeField] private float time = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Load());
    }

    // loads the next scene
    IEnumerator Load()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Scenes/"+ folder + "/" + nextScene, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
