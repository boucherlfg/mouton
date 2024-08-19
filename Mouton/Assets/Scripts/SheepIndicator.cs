using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepIndicator : MonoBehaviour
{
    public float top;
    public float bottom;
    public float left;
    public float right;

    public TMPro.TMP_Text label;
    public Transform tip;
    public Transform arrow;
    public GameObject visuals;
    private MoveScript player;
    private SheepScript sheep;
    public SpriteRenderer bubbleImage;
    public GameObject bubbleObject;
    private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        player = FindObjectOfType<MoveScript>();
        sheep = FindObjectOfType<SheepScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(sheep.GetComponentInChildren<SpriteRenderer>().isVisible) {
            visuals.SetActive(false);
            return;
        }
        var min = mainCam.ScreenToWorldPoint(Vector2.zero);
        var max = mainCam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        var rect = new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
        
        visuals.SetActive(true);

        Vector2 delta = sheep.transform.position - player.transform.position;
        var direction = delta.normalized;
        var distance = delta.magnitude;

        label.text = Mathf.Round(distance) + " m";
        label.GetComponent<RectTransform>().localPosition = new Vector3(0, -Mathf.Sign(delta.y) * 1.4f, -2);
        bubbleImage.flipX = delta.x > 0;
        bubbleObject.transform.localPosition = new Vector3(1.85f * -Mathf.Sign(delta.x), 0, 0);
        arrow.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x));

        Vector2 pos = mainCam.transform.position;
        var increments = direction / 100f;
        
        for(int i = 0; i < 100000; i++){
            if(pos.x + increments.x < rect.xMin + left) break;
            if(pos.x + increments.x > rect.xMax + right) break;
            if(pos.y + increments.y < rect.yMin + bottom) break;
            if(pos.y + increments.y > rect.yMax + top) break;
            pos += increments;
        }
        transform.position = pos + (Vector2)tip.localPosition;
    }
}