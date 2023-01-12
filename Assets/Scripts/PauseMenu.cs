using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  [SerializeField] GameObject pauseMenu;
  [SerializeField] PlayerMovement playerReset;
  [SerializeField] PanelScript panelReset;
  [SerializeField] PressurePlate plateReset;
  [SerializeField] DoorScript doorReset;
  private GameObject music;
  public AudioSource pause;
  public AudioSource unPause;
  

    public void Pause(){
      pause.Play();
      pauseMenu.SetActive(true);
      Time.timeScale = 0f;
    }

    public void Resume(){
      unPause.Play();
      pauseMenu.SetActive(false);
      Time.timeScale = 1f;
    }

    public void Home(){
      Time.timeScale = 1f;
      music = GameObject.FindGameObjectWithTag("Music");
      music.GetComponent<StartMusic>().StopMusic();
      music.GetComponent<StartMusic>().RestartMusic();
      SceneManager.LoadScene(1);
    }

    public void Reset(){
      unPause.Play();
      //reset variables
      playerReset.Respawn("reset");
      if(panelReset != null){
        panelReset.Reset();
      }
      if(plateReset != null){
        plateReset.Reset("all");
      }
      if(doorReset != null){
        doorReset.Reset();
      }
      //exit pause menu
      pauseMenu.SetActive(false);
      //reset time
      Time.timeScale = 1f;
    }
}
