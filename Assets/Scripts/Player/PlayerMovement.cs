using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private const float WALKING_SPEED = 3.5f;
    private const float RUNNING_SPEED = 10f;
    private const float DASHING_SPEED = 300f;
    private const float DASH_DURATION = 1f;

    public float Stamina = 100;

    [SerializeField]
    private NavMeshAgent playerNavMesh;
    [SerializeField]
    private LayerMask clickableLayers;
    
    private Vector3 direction;
    Quaternion lookRotation;

    
    private float navRotationSpeed = 5;
    private bool isRunning=false;

    public void ClickToMove()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100, clickableLayers))
        {
            playerNavMesh.destination = hit.point;
        }

    }
    public void Dash()
    {
        if (Stamina >= 25)
        {
            Stamina -= 25;
            StartCoroutine(Dashing());
        }
        else
        {
            Debug.Log("Not enough stamina! "+Stamina);
        }
    }
    public void Teleport()
    {
        RaycastHit hit;
        if (Stamina >= 75)
        {
            Stamina -= 75;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
            {
                transform.position = hit.point + (Vector3.up*1.5f);
            }
                
        }
        else
        {
            Debug.Log("Not enough stamina! "+Stamina);
        }
    }
    public void Run()
    {
        if (isRunning==true)
        {
            isRunning = false;
        }
        else
        {
            isRunning = true;
        }
    }
    public void FaceTarget()
    {
        direction = (playerNavMesh.destination - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navRotationSpeed);
    }
    public void UpdateStamina()
    {
        if(Stamina < 100)
        {
            Stamina += 15*Time.deltaTime;
        }
    }
    public void UpdateSpeed()
    {
        if (isRunning)
        {
            playerNavMesh.speed = RUNNING_SPEED;
            Stamina -= 25 * Time.deltaTime;
        }
        else
        {
            playerNavMesh.speed = WALKING_SPEED;
        }

        if (Stamina < 0)
        {
            Run();
            Debug.Log("Not enough stamina! " + Stamina);
        }
    }

    private IEnumerator Dashing()
    {
        playerNavMesh.speed = DASHING_SPEED;
        yield return new WaitForSeconds(DASH_DURATION);
        playerNavMesh.speed = WALKING_SPEED;
    }
}
