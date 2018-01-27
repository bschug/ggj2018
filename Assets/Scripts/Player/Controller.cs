using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : RayCastController {




	public float maxAngle = 80;
	public float maxAngleDown = 70;
	public float dirX=1;
	public Vector2 controllerVelocity;
	Vector3 characterSize;
	Vector3 newCharacterSize;
	Vector3 oldCharacterSize;
	public bool bouncing;
	public bool icy;
	public bool obstacleColLeft = false;
	public bool obstacleColRight = false;
	public Vector2 size;
	public BoxCollider2D thisCollider;
	public Vector3 oldCharacterPos;

	// Use this for initialization
	public override void Start () {

		base.Start ();
		oldCharacterSize = this.gameObject.transform.localScale;
		newCharacterSize = new Vector3 (oldCharacterSize.x,oldCharacterSize.y/1.3f,oldCharacterSize.z);


	}
	public void crouch(){
		


		if (characterSize == newCharacterSize) {
			
			transform.Translate(Vector2.up*2*this.transform.localScale.y/3);
			characterSize = oldCharacterSize;
			this.transform.localScale = characterSize;

		
			RaySpacing();
		} else {
			
			characterSize=newCharacterSize;
			this.transform.localScale = characterSize;

		
		
			RaySpacing();
		
		}

	
	
	}
	

	public void move(Vector2 velocity){
		RaycastUpdateOrigin ();
		collisions.collisionReset ();



		if (velocity.x != 0) {

			horizontalCollision (ref velocity);

		}
		if (velocity.y != 0) {

			verticalCollision (ref velocity);

		}

		transform.Translate (velocity);


	}

	public void verticalCollision(ref Vector2 velocity){

		float directionY = Mathf.Sign (velocity.y);
		float raylength = Mathf.Abs (velocity.y) + skinWidth;

		for(int i=0;i<verticalRayCount;i++){
			
			LayerMask vertMask=(directionY==-1)?groundMask:ceilingMask;
			Vector2 rayOrigin=(directionY==-1)?raycastOrigin.bottomLeft:raycastOrigin.topLeft;
			rayOrigin+=Vector2.right *(horizontalRaySpacing*i+velocity.x);


			RaycastHit2D hit = Physics2D.Raycast(rayOrigin,Vector2.up*directionY,raylength,vertMask); 
			Debug.DrawRay (rayOrigin, Vector2.up * directionY, Color.green);
			if (hit) {
				if (hit.transform.gameObject.tag == "icy") {

					icy = true;
				
				} 


				velocity.y = (hit.distance-skinWidth) *directionY;
				raylength = hit.distance;

				if (collisions.isClimbing) {

					velocity.x = velocity.y/Mathf.Tan (collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign (velocity.x);

				}

				collisions.above = directionY == 1;
				collisions.below = directionY == -1;
			}else {

				icy = false;

			}
					}
		if (collisions.isClimbing) {

			float directionX = Mathf.Sign (velocity.x);
			raylength = Mathf.Abs (velocity.x) + skinWidth;
			Vector2 rayOrigin =((directionX==-1)?raycastOrigin.bottomLeft:raycastOrigin.bottomRight)+Vector2.up*velocity.y;
			RaycastHit2D hit =Physics2D.Raycast(rayOrigin,Vector2.right*directionX,raylength,wallMask);

			if(hit){
				float slopeAngle=Vector2.Angle(hit.normal,Vector2.up);

				if(collisions.slopeAngle!= slopeAngle){

					velocity.x=(hit.distance-skinWidth)*directionX;
					raylength = hit.distance;
				}

			}
		}
	}





	
	


	public void horizontalCollision(ref Vector2 velocity){

		float directionX = Mathf.Sign (velocity.x);
		float raylength = Mathf.Abs (velocity.x) + skinWidth;

		for(int i=0;i<verticalRayCount;i++){

			Vector2 rayOrigin=(directionX==-1)?raycastOrigin.bottomLeft:raycastOrigin.bottomRight;
			rayOrigin += (Vector2.up * verticalRaySpacing * i);

			RaycastHit2D hit = Physics2D.Raycast(rayOrigin,Vector2.right*directionX,raylength,wallMask); 
			Debug.DrawRay (rayOrigin, Vector2.right * directionX, Color.green);
			if (hit) {
				Vector2 refVector = i==0? Vector2.up:Vector2.down;
				float slopeAngle = Vector2.Angle (hit.normal, refVector);

				if (i == 0 && slopeAngle <= maxAngle) {
					
					ClimbingSlope (ref velocity, slopeAngle,1);

				}
				if (i == verticalRayCount-1&& slopeAngle <= maxAngle) {
					
					ClimbingSlope (ref velocity, slopeAngle,-1);

				}
				if (!collisions.isClimbing || slopeAngle > maxAngle) {
					
					velocity.x = (hit.distance - skinWidth) * directionX;
					raylength = hit.distance;

					if (collisions.isClimbing) {
					
						velocity.y = Mathf.Tan (collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs (velocity.x);
					
					}

					collisions.right = directionX == 1;
					collisions.left = directionX == -1;
					
				}

			}
		}
	}
	public void DescendingSlope(ref Vector2 velocity){

		float directionX = Mathf.Sign (velocity.x);


		Vector2 rayOrigin = (directionX == -1)?raycastOrigin.bottomRight:raycastOrigin.bottomLeft;

		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, -Vector2.up, Mathf.Infinity, groundMask);
		Debug.DrawRay (rayOrigin, Vector2.down*5, Color.red);

		if (hit) {

			float slopeAngle = Vector2.Angle (hit.normal, Vector2.up);
			if (slopeAngle != 0 && slopeAngle <= maxAngleDown) {
				if(Mathf.Sign(hit.normal.x) == directionX){
					if(hit.distance-skinWidth<=Mathf.Tan(slopeAngle*Mathf.Deg2Rad)*Mathf.Abs(velocity.x)){

						float moveDirection = Mathf.Abs (velocity.x);
						float descendVelocity = Mathf.Sin (slopeAngle * Mathf.Deg2Rad) * moveDirection;
						velocity.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDirection * Mathf.Sign (velocity.x);
						velocity.y -= descendVelocity;

						collisions.slopeAngle = slopeAngle;
						collisions.isDescending = true;
						collisions.below = true;

					}
				}
			}
		}
	}
	public void ClimbingSlope(ref Vector2 velocity, float slopeAngle, int Direction){

		float moveDirection = Mathf.Abs (velocity.x);
		float climbingVelocitiy = Mathf.Sin (slopeAngle * Mathf.Deg2Rad) * moveDirection;

		if(velocity.y<=climbingVelocitiy){
			velocity.y = climbingVelocitiy* Direction;
			velocity.x = Mathf.Cos (slopeAngle * Mathf.Deg2Rad) * moveDirection * Mathf.Sign (velocity.x);
			collisions.below = true;
		}

		collisions.isClimbing = true;

		collisions.slopeAngle = slopeAngle;

	}



	public void PlatformHorizontalCollision(ref Vector2 velocity){

		float directionX = Mathf.Sign (velocity.x);
		float raylength = Mathf.Abs (velocity.x) + skinWidth;

		for (int i = 0; i < verticalRayCount; i++) {

			Vector2 rayOrigin = (directionX == -1) ? raycastOrigin.bottomLeft : raycastOrigin.bottomRight;
			rayOrigin += (Vector2.up * verticalRaySpacing * i);

			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, raylength, wallMask); 
			Debug.DrawRay (rayOrigin, Vector2.right * directionX, Color.red);
			if (hit) {
				obstacleColLeft=directionX==-1;
				obstacleColRight=directionX==1 ;
			} 
				

		}
	}
	public void DirectionChange(Vector2  velocity){

		RaycastUpdateOrigin ();
		float directionX = Mathf.Sign (velocity.x);
		Vector2 rayOrigin = (directionX == -1) ? raycastOrigin.bottomLeft : raycastOrigin.bottomRight;
		RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.down, 1, groundMask);
		if (collisions.right || collisions.left) {
		
			dirX = -dirX;
		
		}
		if (!hit) {
		
			dirX = -dirX;
		
		}

			}
}
