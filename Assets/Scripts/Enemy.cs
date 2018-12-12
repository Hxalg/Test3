using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour 
{
	public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    Vector2 hotSpot = new Vector2(16.0f,16.0f);
	public bool targeted=false;

    //public float MoveSpeed;

    private NavMeshAgent Agent;
    public Transform Target;
    private bool contact;
    float radius = 0.5f;

    // Use this for initialization
    void Start () 
	{
        Agent = this.GetComponent<NavMeshAgent>();
        //MoveSpeed = 0.05f;
    }
	
	// Update is called once per frame
	void Update () 
	{
        if(Agent != null)
        {
            Agent.SetDestination(Target.position);
            //transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
        }

        //collider
        contact = false;

        foreach (Collider col in Physics.OverlapSphere(transform.position, radius, 1 << LayerMask.NameToLayer("Col")))
        {
            Vector3 contactPoint = col.ClosestPointOnBounds(transform.position);

            Vector3 v = transform.position - contactPoint;

            transform.position += Vector3.ClampMagnitude(v, Mathf.Clamp(radius - v.magnitude, 0, radius));

            contact = true;
        }
    }
	void OnMouseEnter() 
	{
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		targeted = true;
    }
    void OnMouseExit() 
	{
        Cursor.SetCursor(null, new Vector2(16.0f,16.0f), cursorMode);
		targeted = false;
    }
	
}
