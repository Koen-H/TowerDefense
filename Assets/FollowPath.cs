using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowPath : MonoBehaviour
{
    [SerializeField] bool moving;
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] int targetNode = 0;
    [SerializeField] Vector2 targetNodePos;

    List<Path> path;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) Move();
       
    }

    void Move()
    {
        CheckNodeArrival();
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, targetNodePos, step);

        float angle = Mathf.Atan2(targetNodePos.y - transform.position.y, targetNodePos.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    }
    void CheckNodeArrival()
    {
        if (path == null) path = GameManager.Instance.pathGenerator.GetPath();
        Vector2 distvec = targetNodePos - new Vector2(transform.position.x, transform.position.y);
        if(distvec.magnitude < 0.1f) targetNode++;
           
        targetNodePos = path[targetNode].transform.position;
    }
}