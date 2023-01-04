using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] TargetingType targetingType = TargetingType.Nearest;
    [SerializeField] List<AimAt> cannons;
    [SerializeField] Range range;

    [SerializeField] SpriteRenderer rangeMesh;

    private void Awake()
    {
        rangeMesh = range.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Enemy target = range.GetTarget(targetingType);
        foreach (AimAt cannon in cannons) cannon.SetTarget(target);
    }



    public void OnMouseDown()
    {
        rangeMesh.enabled = true;
        //Open upgrades
    }
    void OnMouseExit()
    {
        rangeMesh.enabled=false;
    }

}

public enum TargetingType
{
    First,
    Farthest,
    Nearest,
    Strongest,
}