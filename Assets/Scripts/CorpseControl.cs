using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseControl : MonoBehaviour
{
  private Rigidbody2D rb;
  private bool interactOn = false;
  [SerializeField] public GameObject plate;
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    //check for any collisions
    void OnCollisionEnter2D(Collision2D target){
      //when the corpse hits the floor, turn body to Static
      if(target.gameObject.tag == "floor" || target.gameObject.tag == "plate" || target.gameObject.tag == "corpse"){
        rb.bodyType = RigidbodyType2D.Static;
      }
      if(target.gameObject.tag == "plate"){
        plate = target.gameObject;
      }
    }
}
