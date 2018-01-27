using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastController : MonoBehaviour {

	public LayerMask groundMask;
	public LayerMask wallMask;
	public LayerMask ceilingMask;

	public BoxCollider2D collider;
	public float skinWidth= 0.015f;

	public Raycastorigin raycastOrigin;
	public CollisionInfo collisions;

	public int verticalRayCount =4;
	public int horizontalRayCount =4;

	public float horizontalRaySpacing;
	public float verticalRaySpacing;



	// Use this for initialization

	public virtual void Start(){

		collider = GetComponent<BoxCollider2D>();
		RaySpacing ();


	}
	public void RaycastUpdateOrigin(){
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -3);

		raycastOrigin.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigin.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigin.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigin.topRight = new Vector2 (bounds.max.x, bounds.max.y);

	}
	public void RaySpacing(){
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -3);

		horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp (verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.x / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.y / (verticalRayCount - 1);

	}

	public struct Raycastorigin{

		public Vector2 bottomLeft,bottomRight;
		public Vector2 topLeft,topRight;

	}

	public struct CollisionInfo{

		public bool above, below;
		public bool right,left;
		public bool isClimbing;
		public bool isDescending;
		public float slopeAngle, slopeAngleOld;
		public bool colliding;

		public void collisionReset(){

			above = below = false;
			right = left = false;
			isClimbing = false;
			isDescending = false;


			slopeAngleOld = slopeAngle;
			slopeAngle = 0;

		}

	}
}
