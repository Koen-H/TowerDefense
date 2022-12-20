using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public PathGenerator pathGenerator;

    public static GameManager Instance { get {
            if (_instance == null) Debug.LogError("GameManager is null");
            return _instance; 
        } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }


        pathGenerator = this.GetComponent<PathGenerator>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pathGenerator.GeneratePath();
        }
    }
}
