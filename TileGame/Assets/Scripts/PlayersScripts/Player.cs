using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    

    public Player()
    {
        

    }

    // Start is called before the first frame update
    void Start()
    {

        GameObject ThePlayer = new GameObject("ThePlayer");
        ThePlayer.AddComponent<Camera>();
        ThePlayer.GetComponent<Camera>().transform.Translate(new Vector3(0, 0, -10));
        ThePlayer.AddComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPlayer(GameObject ThePlayer)
    {
        CreatePlayer(ThePlayer);
        SetPlayerLocation();
    }

    void CreatePlayer(GameObject ThePlayer)
    {
        Camera cam = gameObject.GetComponent<Camera>();
        ThePlayer.AddComponent<Camera>();
        ThePlayer.GetComponent<Camera>().transform.Translate(new Vector3(0, 0, -10));
        
    }

    public void SetPlayerLocation()
    {
        
    }
}
