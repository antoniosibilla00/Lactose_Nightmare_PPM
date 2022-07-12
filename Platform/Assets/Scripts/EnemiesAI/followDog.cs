using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followDog : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private Transform dogToFollow;

    // Update is called once per frame
    void Update()
    {
       
        this.transform.position =(new Vector2(dogToFollow.position.x ,dogToFollow.position.y+0.85f));
  
        
        
    }
}
