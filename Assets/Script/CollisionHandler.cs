using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip CrashSFX;
    [SerializeField] AudioClip SuccessSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audiosource;

    bool isControllable = true;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (!isControllable) {return;}

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
        isControllable = false;
        audiosource.Stop();
        audiosource.PlayOneShot(SuccessSFX);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audiosource.Stop();
        audiosource.PlayOneShot(CrashSFX);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke ("ReloadLevel", levelLoadDelay);
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
