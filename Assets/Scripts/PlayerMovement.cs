using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{

  private Rigidbody2D rb;
  private BoxCollider2D coll;
  private BoxCollider2D door;
  private GameObject plate;
  private GameObject corpseTouch;
  [SerializeField] private LayerMask jumpableGround;
    public Tilemap tilemap;

  private GameObject[] doorsDown;
  private GameObject[] corpses;

  public SpriteRenderer spriteRenderer;
  private Animator anim;
  public Sprite idle;
  public Sprite kbotR;
  public GameObject spawn;
  public AudioSource jump;
  public AudioSource respawn;

  public GameObject bodyPrefab;
  public SpriteRenderer battery;
  public Sprite battery0;
  public Sprite battery1;
  public Sprite battery2;
  public Sprite battery3;
  public SpriteRenderer wrench1;
  public SpriteRenderer wrench2;
  private bool interactCorpse = false;
    private bool playerMove = true;
  private int chargeNum;
  public int charges = 3;
//private float jumpHeight = Screen.height / 3;
  private int converters = 0;
  
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      coll = GetComponent<BoxCollider2D>();
      anim = GetComponent<Animator>();
        playerMove = true;

      //add specific charge amount here
      chargeNum = charges;
    }

    // Update is called once per frame
    void Update()
    {
        //-------- PLAYER CONTROL -----------
        if (playerMove)
        {
            float axisX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(axisX * 7f, rb.velocity.y);
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                //spriteRenderer.sprite = kbotR;
                // anim.SetBool("IsRunning", false);
                // anim.SetBool("IsJumping", true);
                rb.velocity = new Vector2(rb.velocity.x, tilemap.localBounds.extents.y + 1);
                jump.Play();
            }
            //-------- WALK CHANGE PIC -----------
            if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //spriteRenderer.sprite = kbotR;
                spriteRenderer.flipX = true;
                anim.SetBool("IsRunning", true);
                anim.SetBool("IsJumping", false);
                //left
            }
            else if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))
            {
                //spriteRenderer.sprite = kbotR;
                spriteRenderer.flipX = false;
                anim.SetBool("IsRunning", true);
                anim.SetBool("IsJumping", false);
                //right
            }
            else if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
            {
                spriteRenderer.sprite = idle;
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsJumping", true);
            }
            else if (!Input.anyKey)
            {
                spriteRenderer.sprite = idle;
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsJumping", false);
            }
        } else
        {
            rb.velocity = new Vector2(0, 0);
            spriteRenderer.sprite = idle;
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsJumping", false);
        }
      //-------- CORPSE CONTROL ------------
      //create new Corpse
      if(Input.GetKeyDown("return") && charges >= 1){
        //finds player position
        float playerX = transform.position.x;
        float playerY = transform.position.y;
        //spawns body prefab in players position
        Instantiate(bodyPrefab, new Vector2(playerX, playerY), Quaternion.identity);
        charges -= 1;
        //respawns player
        Respawn("respawn");
      }
      //--------- CONVERTER CONTROL -----------
      if(Input.GetKeyDown("e") && converters >= 1 && interactCorpse){
        if(corpseTouch.GetComponent<CorpseControl>().plate != null){
          GameObject resetPlate = corpseTouch.GetComponent<CorpseControl>().plate;
          resetPlate.GetComponent<PressurePlate>().Reset("single");
        }
        Destroy (corpseTouch);
        charges += 1;
        if(converters == 2){
          wrench2.color = Color.black;
        } else if(converters == 1){
          wrench1.color = Color.black;
        }
        converters -= 1;
      }
      //change number Text
      if(charges == 0){
        battery.sprite = battery0;
      } else if(charges == 1){
        battery.sprite = battery1;
      } else if(charges == 2){
        battery.sprite = battery2;
      } else if(charges == 3){
        battery.sprite = battery3;
      }
    }

    private bool IsGrounded(){
      //creates a box slightly below the player to detect if touching ground
      return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    //check for any collisions
    void OnCollisionEnter2D(Collision2D target){
      //touches corpse
      switch(target.gameObject.tag){
        case "corpse": interactCorpse = true;
          corpseTouch = target.gameObject;
          break;
        case "door": door = target.gameObject.GetComponent<BoxCollider2D>();
          break;
        case "converter": converters += 1;
          if(converters == 1){
            wrench1.color = Color.white;
          } else if (converters == 2){
            wrench2.color = Color.white;
          }
          target.gameObject.GetComponent<BoxCollider2D>().enabled = false;
          target.gameObject.GetComponent<SpriteRenderer>().enabled = false;
          break;
      }
    }
    //check when collisions stop (for interactions)
    void OnCollisionExit2D(Collision2D target){
      //leaves corpse
      switch(target.gameObject.tag){
        case "corpse": interactCorpse = false;
          break;
      }
    }

    public void Respawn(string state){
      //tps player to starting position
      transform.position = new Vector2(spawn.transform.position.x, spawn.transform.position.y);
      respawn.Play();

      if(state == "reset"){
        //remove all corpses
        corpses = GameObject.FindGameObjectsWithTag("corpse");
        foreach (GameObject corpse in corpses){
          Destroy (corpse);
        }
        //show all converters again
        GameObject[] convertersObjs = GameObject.FindGameObjectsWithTag("converter");
        foreach (GameObject converterObj in convertersObjs){
          converterObj.GetComponent<BoxCollider2D>().enabled = true;
          converterObj.GetComponent<SpriteRenderer>().enabled = true;
        }
        //reset wrench overlay
        if(GameObject.Find("wrench_1") != null){
          wrench1.color = Color.black;
        }
        if(GameObject.Find("wrench_2") != null){
          wrench2.color = Color.black;
        }
        //reset converter amount
        converters = 0;
        //reset Charges
        charges = chargeNum;
      }
    }

    public void playerMovement()
    {
        playerMove = !playerMove;
    }
}
