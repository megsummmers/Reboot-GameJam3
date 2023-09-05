using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
  public AudioSource victory;
  public AudioSource spawn;
  private GameObject music;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D collision){
      if(collision.gameObject.tag == "player"){
        music = GameObject.FindGameObjectWithTag("Music");
        music.GetComponent<StartMusic>().PauseMusic();
        victory.Play();
        //Time.timeScale = 0f;
        Invoke("CompleteLevel", 2f);
            collision.gameObject.GetComponent<PlayerMovement>().playerMovement();
        }
    }

    private void CompleteLevel(){
        music = GameObject.FindGameObjectWithTag("Music");
        music.GetComponent<StartMusic>().PlayMusic();
        spawn.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
}
