using UnityEngine;
using System.Collections;

public class PathWalker : MonoBehaviour
{
	public delegate void PathWalkerHandler(PathWalker walker);
	public event PathWalkerHandler ReachedEndEvent;

	public bool WalkActive { get; private set; }
	private Path pathUsing;

	private int goalWaypointIndex = 0;
	private float movementSpeed = 0;

	public void StartPathWalking()
	{
		WalkActive = true;
    }

	public void StopPathWalking()
	{
		WalkActive = false;
    }

	public void SetWalker(Path path, float speed)
	{
		pathUsing = path;
		movementSpeed = speed * pathUsing.SpeedModifier;
    }
	
	protected void Update ()
	{
		if(pathUsing != null && WalkActive)
		{
			Vector2 targetPos = pathUsing.PathWaypoints[goalWaypointIndex].transform.position;
            MoveTo(targetPos);
			GoalPointUpdate(targetPos);
        }
	}

	private void GoalPointUpdate(Vector2 currentPos)
	{
		if((currentPos.Vec3Substract(transform.position)).magnitude < 0.2f)
		{
			if(goalWaypointIndex < (pathUsing.PathWaypoints.Length - 1))
			{
				goalWaypointIndex++;
			}
			else
			{
				StopPathWalking();
				if (ReachedEndEvent != null)
				{
					ReachedEndEvent(this);
				}
			}
		}
	}

	private void MoveTo(Vector2 position)
	{
		Vector2 dir = position.Vec3Substract(transform.position).normalized;
		transform.Translate(dir * movementSpeed * Time.deltaTime);
	}
}
