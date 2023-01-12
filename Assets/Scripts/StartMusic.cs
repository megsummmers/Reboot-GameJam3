using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMusic : MonoBehaviour
{
  public AudioSource bgMusic;
    //------ AUDIO CONTROL --------
    private void Awake(){
      DontDestroyOnLoad(transform.gameObject);
      bgMusic.Play();
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StopMusic(){
      bgMusic.Stop();
    }

    public void PauseMusic(){
      bgMusic.Pause();
    }

    public void PlayMusic(){
      bgMusic.UnPause();
    }

    public void RestartMusic(){
      bgMusic.Play();
    }
}
