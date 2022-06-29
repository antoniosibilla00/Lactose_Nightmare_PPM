using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{

    public GameObject localPos;
    private float positionUpdate;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
 
    
    void changePosition()
    {
        Debug.Log("sium22Sono entrato ");
        Debug.Log("sium22positionUpdate = " + positionUpdate);
        Debug.Log("sium22localPos.transform.position = " + localPos.transform.position);
        Debug.Log("sium22transform.position = " + transform.position);
        
        positionUpdate = localPos.transform.position.x;
        transform.position = new Vector3(positionUpdate ,transform.position.y, 0);
        
        Debug.Log("sium22transform.position1  = " + transform.position);


    }
}
