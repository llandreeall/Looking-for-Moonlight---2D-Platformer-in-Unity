using UnityEngine;
using System.Collections;

public class EnemyNotAINotStunnable : MonoBehaviour
{

	public float moveSpeed = 4f;  // enemy move speed when moving

	public GameObject[] myWaypoints; // to define the movement waypoints

	public float waitAtWaypointTime = 1f;   // how long to wait at a waypoint

	public bool loopWaypoints = true; // should it loop through the waypoints

	public int damage = 30;


	// store references to components on the gameObject
	Transform _transform;
	Rigidbody2D _rigidbody;
	Animator _animator;

	// movement tracking
	int _myWaypointIndex = 0; // used as index for My_Waypoints
	float _moveTime;
	float _vy = 0f;
	bool _moving = true;

	// store the layer number the enemy is on (setup in Awake)
	int _enemyLayer;

	public AudioSource source;

	void Awake()
	{
		// get a reference to the components we are going to be changing and store a reference for efficiency purposes
		_transform = GetComponent<Transform>();

		_rigidbody = GetComponent<Rigidbody2D>();
		if (_rigidbody == null) // if Rigidbody is missing
			Debug.LogError("Rigidbody2D component missing from this gameobject");

		_animator = GetComponent<Animator>();
		if (_animator == null) // if Animator is missing
			Debug.LogError("Animator component missing from this gameobject");

		// setup moving defaults
		_moveTime = 0f;
		_moving = true;

		// determine the enemies specified layer
		_enemyLayer = this.gameObject.layer;

		Physics2D.IgnoreLayerCollision(10, _enemyLayer, true);

	}

	// if not stunned then move the enemy when time is > _moveTime
	void Update()
	{
		
			if (Time.time >= _moveTime)
			{
				EnemyMovement();
				
		}
			else
			{
				_animator.SetBool("Moving", false);
				
			}
		
	}

	// Move the enemy through its rigidbody based on its waypoints
	void EnemyMovement()
	{
		// if there isn't anything in My_Waypoints
		if ((myWaypoints.Length != 0) && (_moving))
		{

			// make sure the enemy is facing the waypoint (based on previous movement)
			Flip(_vy);

			// determine distance between waypoint and enemy
			_vy = myWaypoints[_myWaypointIndex].transform.position.y - _transform.position.y;

			//Debug.Log(_myWaypointIndex + " " + myWaypoints.Length);
			// if the enemy is close enough to waypoint, make it's new target the next waypoint
			if (Mathf.Abs(_vy) <= 0.05f)
			{
				// At waypoint so stop moving
				_rigidbody.velocity = new Vector2(0, 0);

				// increment to next index in array
				_myWaypointIndex++;

				//Debug.Log(_myWaypointIndex + " " + myWaypoints.Length);
				// reset waypoint back to 0 for looping
				if (_myWaypointIndex >= myWaypoints.Length)
				{
					//Debug.Log(loopWaypoints);
					if (loopWaypoints)
						_myWaypointIndex = 0;
					else
                    {
						_moving = false;
						//Debug.Log(loopWaypoints);
                    }
						
				}
				Debug.Log("Merge");
				// setup wait time at current waypoint
				_moveTime = Time.time + waitAtWaypointTime;
				
			}
			else
			{
				// enemy is moving
				_animator.SetBool("Moving", true);
				
				// Set the enemy's velocity to moveSpeed in the x direction.
				_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _transform.localScale.y * moveSpeed);
			}

		}
	}

	// flip the enemy to face torward the direction he is moving in
	void Flip(float _vy)
	{
		//Debug.Log("hey");
		// get the current scale
		Vector3 localScale = _transform.localScale;

		if ((_vy > 0f) && (localScale.y < 0f))
		{
			localScale.y *= -1;

		}
		else if ((_vy < 0f) && (localScale.y > 0f))
		{
			localScale.y *= -1;
		}


		// update the scale
		_transform.localScale = localScale;
	}


	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Player")
		{
			Player _player = coll.gameObject.GetComponent<Player>();
			if (_player != null)
			{
				source.Play();
				_player.DamagePlayer(damage);
			}
		}

	}

}
