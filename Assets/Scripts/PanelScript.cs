using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour
{
  public PlayerMovement player;
  public SpriteRenderer spriteRenderer;
  public Sprite panelOff;
  public Sprite panel1;
  public Sprite panel2;
  public Sprite panel3;
  private bool panelInteract = false;
  [SerializeField] public int panelChargeNum;
  [SerializeField] private int panelCharges;

    // Start is called before the first frame update
    void Start()
    {
      panelCharges = panelChargeNum;
    }

    // Update is called once per frame
    void Update()
    {
      //take a charge from the panel
      if(Input.GetKeyDown("e") && panelInteract == true){
        if(panelCharges >= 1 && player.charges < 3){
          player.charges += 1;
          panelCharges -= 1;
          GetComponent<AudioSource>().Play();
        }
        gameObject.tag = "panelChanged";
      }

      switch(panelCharges){
        case 0: spriteRenderer.sprite = panelOff;
        break;
        case 1: spriteRenderer.sprite = panel1;
        break;
        case 2: spriteRenderer.sprite = panel2;
        break;
        case 3: spriteRenderer.sprite = panel3;
        break;
        
      }
    }

    void OnTriggerEnter2D(Collider2D target){
      if(target.gameObject.tag == "player"){
        panelInteract = true;
      }
    }
    //check when collisions stop (for interactions)
    void OnTriggerExit2D(Collider2D target){
      if(target.gameObject.tag == "player"){
        panelInteract = false;
      }
    }

    public void Reset(){
      GameObject[] panels = GameObject.FindGameObjectsWithTag("panelChanged");
      foreach (GameObject panelObj in panels){
        panelObj.GetComponent<SpriteRenderer>().sprite = panel3;
        panelObj.GetComponent<PanelScript>().panelCharges = panelObj.GetComponent<PanelScript>().panelChargeNum;
        panelObj.gameObject.tag = "panel";
      }
    }
}
