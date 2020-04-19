using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourWayMove : MonoBehaviour
{
    public AudioSource audioSource;
    
    public float speed = 5f;

    void Start(){
        audioSource = this.gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        this.gameObject.transform.position += new Vector3(x, y, 0f);
        if(x !=0 || y != 0){
            // walkOnGrass.Play();
        }
    }
}
