using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
  public GameObject door;
  public SpriteRenderer spriteRenderer;
  public Sprite plateDown;
  public Sprite plateUp;
  public Sprite doorOpenSprite;
  public Sprite doorClosedSprite;
  private BoxCollider2D plateColl;
  private Rigidbody2D plateRB;

  private GameObject[] doorMoved;

  public AudioSource plateOn;
  public AudioSource plateOff;

  private bool doorOpen;
    // Start is called before the first frame update
    void Start()
    {
      plateColl = GetComponent<BoxCollider2D>();
      plateRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      if(doorOpen){
        door.GetComponent<SpriteRenderer>().sprite = doorOpenSprite;
      }
    }

    void OnCollisionEnter2D(Collision2D target){
      //touches corpse
      if(target.gameObject.tag == "corpse"){
        door.GetComponent<SpriteRenderer>().sprite = doorOpenSprite;
        door.GetComponent<BoxCollider2D>().enabled = false;
        door.gameObject.tag = "PMoved";
        gameObject.tag = "plateDown";
        spriteRenderer.sprite = plateDown;
        plateOn.Play();
        plateColl.enabled = false;
      }
      if(target.gameObject.tag == "player"){
        doorOpen = true;
        spriteRenderer.sprite = plateDown;
        plateOn.Play();
      }
    }
    //check when collisions stop (for interactions)
    void OnCollisionExit2D(Collision2D target){
      if(target.gameObject.tag == "player"){
        doorOpen = false;
        door.GetComponent<SpriteRenderer>().sprite = doorClosedSprite;
        spriteRenderer.sprite = plateUp;
        plateOff.Play();
      }
    }

    public void Reset(string target){
      if(target == "single"){
        //door reset
        door.GetComponent<SpriteRenderer>().sprite = doorClosedSprite;
        door.GetComponent<BoxCollider2D>().enabled = true;
        door.gameObject.tag = "Pdoor";
        //plate reset
        spriteRenderer.sprite = plateUp;
        plateColl.enabled = true;
        gameObject.tag = "plate";
      } else if(target == "all"){
        GameObject[] movedDoors = GameObject.FindGameObjectsWithTag("PMoved");
        foreach (GameObject mDoor in movedDoors){
          mDoor.GetComponent<SpriteRenderer>().sprite = doorClosedSprite;
          mDoor.GetComponent<BoxCollider2D>().enabled = true;
          mDoor.gameObject.tag = "Pdoor";
        }
        GameObject[] movedPlates = GameObject.FindGameObjectsWithTag("plateDown");
        foreach (GameObject mPlate in movedPlates){
          mPlate.GetComponent<SpriteRenderer>().sprite = plateUp;
          mPlate.GetComponent<PressurePlate>().plateColl.enabled = true;
          mPlate.gameObject.tag = "plate";
        }
      }
    }
}
