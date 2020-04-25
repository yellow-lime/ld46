using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourWayMove : MonoBehaviour
{
    public AudioSource audioSource;
    public LineRenderer lineRenderer;

    public int trailLength = 20;
    private int trailIndex = 0;
    
    public float speed = 5f;

    void Start(){
        // audioSource = this.gameObject.AddComponent<AudioSource>();
        startLineRenderer();
    }

    void startLineRenderer() {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.2f;
        lineRenderer.positionCount = trailLength;
    }

    int incrementTrailIndex() {
        trailIndex = trailIndex == trailLength ? 0 : trailLength+1;
        return trailIndex;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 oldPos = this.gameObject.transform.position;
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        this.gameObject.GetComponent<Rigidbody2D>().MovePosition(oldPos + new Vector3(x, y, 0f));
        // Vector3 movement = new Vector3(0.0f, x2, y2);
        Debug.DrawLine(this.gameObject.transform.position, new Vector3(this.gameObject.transform.position.x + x, this.gameObject.transform.position.y + y, 0), Color.white, 0.1f);
        // incrementTrailIndex();
        // lineRenderer.SetPosition(trailIndex, this.gameObject.transform.position);
        if(x !=0 || y != 0){
            // walkOnGrass.Play();
        }

        // float x2 = Input.GetAxis("Horizontal");
        // float y2 = Input.GetAxis("Vertical");

        // Vector3 movement = new Vector3(x2, 0.0f, y2);
        // Vector3 movement = new Vector3(y2, 0.0f, x2);
        // Vector3 movement = new Vector3(x2, y2, 0.0f);
        // Vector3 movement = new Vector3(0.0f, x2, y2);
        // Vector3 movement = new Vector3(x2, 0.0f, 0.0f);
        // Vector3 movement = new Vector3(x2, 0.0f, y2);
        // transform.rotation = Quaternion.LookRotation(movement);
    }
}
