using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class longPress : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

    [SerializeField] Text texto;

    IPointerEvent pointerEventImplementation;

    // Start is called before the first frame update
    void Start()
    {
        texto.text = "up";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown( PointerEventData eventData )
         {
             texto.text = "down";
         }
     
         public void OnPointerUp( PointerEventData eventData )
         {
             texto.text = "up";
         }
}
