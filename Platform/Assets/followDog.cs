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
        if (GameObject.Find("SalamiDog"))
        {
            this.transform.position =(new Vector2(dogToFollow.position.x+-0.3f,transform.position.y));
        }
        else{
            Debug.Log("siumSino");
            Destroy(gameObject);
        }
       
        
        
    }
}
