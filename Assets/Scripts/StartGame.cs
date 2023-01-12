using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
  public int levelNum = 0;
  public AudioSource click;
  public AudioSource hover;
    // Update is called once per frame

    public void IncreaseLevel(){
      levelNum += 1;
    }

    public void StartTheGame(){
      click.Play();
      IncreaseLevel();
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitTheGame(){
      click.Play();
      Application.Quit();
    }

    public void Hover(){
      hover.Play();
    }
}
