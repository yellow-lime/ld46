using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OverhearBehavior : MonoBehaviour
{

    public float textHoverSpacing = 1;
    public int defaultFontSize = 10;
    public TextMeshPro text;

    public string[] textsToDisplay;

    // Start is called before the first frame update
    void Start()
    {
        this.text = initTextMeshPro(createEmptyChild());
        text.text = textsToDisplay[0];
        text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && this.tag == "Flower" && text.gameObject.activeSelf){
            Debug.Log("picked up a flower.");
            GameController gc = (GameController)FindObjectsOfType(typeof(GameController))[0];
            gc.addFlowerToInventory();
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            text.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            text.gameObject.SetActive(false);
        }
    }


    public GameObject createEmptyChild(){
        GameObject emptyChild = new GameObject("overhearText");
        emptyChild.transform.parent = this.gameObject.transform;
        emptyChild.transform.localPosition = Vector3.up * textHoverSpacing;
        emptyChild.layer = 5; // UI layer is 5.
        return emptyChild;
    }

    public TextMeshPro initTextMeshPro(GameObject gameObject)
    {
        TextMeshPro m_textMeshPro = gameObject.AddComponent<TextMeshPro>();
        m_textMeshPro.autoSizeTextContainer = true;

        m_textMeshPro.fontSize = defaultFontSize;

        m_textMeshPro.alignment = TextAlignmentOptions.Center;
        m_textMeshPro.enableWordWrapping = false;
        m_textMeshPro.material = new Material(Shader.Find("Sprites/Default"));

        return m_textMeshPro;
    }
}
