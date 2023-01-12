using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
  public PlayerMovement player;
  public SpriteRenderer spriteRenderer;
  [SerializeField] private Rigidbody2D doorRB;
  [SerializeField] private BoxCollider2D doorBC;

  public Sprite doorOpen;
  public Sprite doorClosed;
  private bool doorInteract = false;

    // Start is called before the first frame update
    void Start()
    {
      //playerCharges = player.charges;
    }

    // Update is called once per frame
    void Update()
    {
      //use charge to open door
      if(Input.GetKeyDown("e") && doorInteract == true && player.charges >=1){
        //change sprites
        spriteRenderer.sprite = doorOpen;
        GetComponent<AudioSource>().Play();
        //remove interaction
        //doorRB.detectionCollisions = false;
        GetComponent<BoxCollider2D>().enabled = false;
        //removes charge and changes door to trigger so player can move through it
        player.charges -= 1;
        
        gameObject.tag = "Moved";
        doorInteract = false;
      }
    }

    void OnCollisionEnter2D(Collision2D target){
      if(target.gameObject.tag == "player"){
        doorInteract = true;
      }
    }
    //check when collisions stop (for interactions)
    void OnCollisionExit2D(Collision2D target){
      if(target.gameObject.tag == "player"){
        doorInteract = false;
      }
    }

    public void Reset(){
        GameObject[] movedDoors = GameObject.FindGameObjectsWithTag("Moved");
        foreach (GameObject mDoor in movedDoors){
          mDoor.GetComponent<SpriteRenderer>().sprite = doorClosed;
          mDoor.GetComponent<BoxCollider2D>().enabled = true;
          mDoor.gameObject.tag = "door";
        }
    }
}
