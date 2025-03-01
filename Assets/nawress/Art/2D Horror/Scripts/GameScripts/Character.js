#pragma strict

private var characterAnimator : Animator;
var speed : float = 3;
private var grounded = false;
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



function Start () 
{
    jumpHeight = jumpHeight * 100;

}


function Update () 
{
    if (Input.GetKey(KeyCode.A))
    {
        gameObject.transform.rotation.y = 180;
        gameObject.transform.position.x -= speed * Time.deltaTime;
        characterAnimator.SetBool("walking", true);
    }

   else if (Input.GetKey(KeyCode.D))
   {
       gameObject.transform.rotation.y = 0;
       gameObject.transform.position.x += speed * Time.deltaTime;
       characterAnimator.SetBool("walking", true);
   }

   else
   {
       gameObject.transform.position.x += 0 * Time.deltaTime;
       characterAnimator.SetBool("walking", false);
   }

   if (Input.GetKey(KeyCode.Space))
   {

               gameObject.GetComponent.<Rigidbody2D>().AddForce(Vector2.up * jumpHeight);
               characterAnimator.SetBool("flying", true);
   }
   else
   {
       gameObject.transform.position.x += 0 * Time.deltaTime;
       characterAnimator.SetBool("flying", false);
   }
}
