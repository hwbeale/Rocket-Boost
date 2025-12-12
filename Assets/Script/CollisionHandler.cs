using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float delay = 2f;
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
            Debug.Log("Friendly");
            break;

            case "Finish":
            FinishSequence();
            break;

            default:
            StartCrashSequence();
            break;
        }
    }

    private void FinishSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", delay);
    }

    void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke ("ReloadLevel", delay);
    }

    void ReloadLevel()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene);
    }
    void NextLevel()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = CurrentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
        }
}
