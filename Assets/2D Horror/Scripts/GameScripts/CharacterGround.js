#pragma strict

private var characterAnimator : Animator;
var speed : float = 3;
var grounded = false;
var jumpHeight : float = 3;

function OnTriggerEnter2D(colenter : Collider2D)
{

	grounded = true;
}

function OnTriggerExit2D(colexit : Collider2D)
{
	grounded = false;
}

function Awake()
{
	characterAnimator = gameObject.GetComponent("Animator");
}

function Start()
{
	jumpHeight = jumpHeight * 100;
}

function Update()
{
	if (Input.GetKey(KeyCode.A))
	{
		gameObject.transform.position.x -= speed * Time.deltaTime;
		gameObject.transform.rotation.y = 180;
		characterAnimator.SetBool("walking", true);
	}
	else if (Input.GetKey(KeyCode.D))
	{
		gameObject.transform.position.x += speed * Time.deltaTime;
		gameObject.transform.rotation.y = 0;
		characterAnimator.SetBool("walking", true);
	}
	else
	{
		gameObject.transform.position.x += 0 * Time.deltaTime;
		characterAnimator.SetBool("walking", false);
	}
	
	if (Input.GetKeyDown(KeyCode.Space))
	{
		if (grounded)
		{
			gameObject.GetComponent.<Rigidbody2D>().AddForce(Vector2.up * jumpHeight);
			grounded = false;
		}
	}
}