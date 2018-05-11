using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour {

    //The texture that's going to replace the current cursor  
    public Texture2D cursorTexture;
    public Texture2D canGrappleTex;
    public bool canGrapple = false;
  
    //This variable flags whether the custom cursor is active or not  
    public bool ccEnabled = false;  
  
    void Start()  
    {
        //Call the 'SetCustomCursor' (see below) with a delay of 2 seconds.   
        //Invoke("SetCustomCursor",0);
        Cursor.SetCursor(this.cursorTexture, Vector2.zero, CursorMode.Auto);
        ccEnabled = true;
    }  
  
    public void OnDisable()   
    {  
        //Resets the cursor to the default  
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  
        //Set the _ccEnabled variable to false  
        ccEnabled = false;  
    }  
  
    public void SetCustomCursor()  
    {  
        
        if (canGrapple) {
            Cursor.SetCursor(this.canGrappleTex, Vector2.zero, CursorMode.Auto);
         }
            
        else {
                
            Cursor.SetCursor(this.cursorTexture, Vector2.zero, CursorMode.Auto);
            
         }
            //Replace the 'cursorTexture' with the cursor    
            //Cursor.SetCursor(this.cursorTexture, Vector2.zero, CursorMode.Auto);
            //Set the ccEnabled variable to true  
            
        
        

    }  
}
