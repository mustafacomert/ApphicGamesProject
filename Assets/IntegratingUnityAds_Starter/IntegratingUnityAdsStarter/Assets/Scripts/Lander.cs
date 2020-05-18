using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lander : MonoBehaviour
{
    public Transform bottomThruster;

    public Transform leftThruster;

    public Transform rightThruster;

    public float mainThrustPower;

    public float sideThrustPower;

    public GameObject explosionPrefab;

    public GameObject landerFeet;

    public bool allowThrust;

    public float fuel;

    public float collisionVelocityForDestroy = 3f;

	public float thrusterAngle;

	public float thrusterAngleDeg;

	public GameObject thrusterHandle;

	public GameObject thrusterHandleSpriteObject;

	public GameObject ringSprite;

    public Text fuelText;

    public Animator mainThrusterAnim;

	public Animator thrusterHandleAnim;

    public Animator leftThrusterAnim;

    public Animator rightThrusterAnim;

    public AudioSource thrusterAudio;

    private GameObject landerObjective;

    private Rigidbody2D landerRigidBody2D;

    private bool canDeployFeet;
    
    private bool shouldPlayThrustSfx;

    private HingeJoint2D feetJoint;

    private Button watchVideoAdBtn;

    private Button fuelRewardBtn;
    
	// Use this for initialization
	void Start ()
	{
	    fuelText = GameObject.Find("FuelText").GetComponent<Text>();
	    landerRigidBody2D = GetComponent<Rigidbody2D>();
	    landerObjective = GameObject.Find("LanderObjective");
	    feetJoint = transform.Find("LanderFeet").GetComponent<HingeJoint2D>();
        watchVideoAdBtn = GameObject.Find("WatchVideoAdBtn").GetComponent<Button>();
        fuelRewardBtn = GameObject.Find("MoreFuelRewardAdBtn").GetComponent<Button>();

	    if (GameManager.instance.extraFuel > 0f) {
	        fuel += GameManager.instance.extraFuel;
            fuelText.text = "Fuel " + Mathf.Round(fuel);
	        GameManager.instance.extraFuel = 0f;
	    }
	}

	// Update is called once per frame
	void Update () {
        if (landerObjective == null) landerObjective = GameObject.Find("LanderObjective");

	    var objectiveDistance = Vector2.Distance(transform.position, landerObjective.transform.position);
	    if (objectiveDistance <= 1f && canDeployFeet == false) {
	        canDeployFeet = true;
	    }

	    if (canDeployFeet == true) {
            var landerFeetYPos = feetJoint.anchor.y + Time.deltaTime / 3;

            if (landerFeetYPos < 0.38f) {
                feetJoint.anchor = new Vector2(0f, landerFeetYPos);
	        }
	        else {
	            canDeployFeet = false;
	        }
	    }

        if (shouldPlayThrustSfx == true) {
            PlayThrusterSfx();
        }
        else {
            thrusterAudio.Pause();
        }
	}

    void FixedUpdate() {

		if (Input.GetMouseButton (0) && allowThrust) {
			var location = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			var thrusterDirection = location - transform.position;
			thrusterAngle = Mathf.Atan2 (thrusterDirection.y, thrusterDirection.x);
			if (thrusterAngle < 0f) {
				thrusterAngle = Mathf.PI * 2 + thrusterAngle;
			}

			thrusterAngleDeg = thrusterAngle * Mathf.Rad2Deg;

			var x = transform.position.x + 0.75f * Mathf.Cos (thrusterAngle);
			var y = transform.position.y + 0.75f * Mathf.Sin (thrusterAngle);

			var thrusterHandlePos = new Vector3 (x, y, 0);
			thrusterHandle.transform.position = thrusterHandlePos;

			ApplyThrusterForceFromTransformDirection (thrusterHandle.transform, mainThrustPower);
			shouldPlayThrustSfx = true;
			thrusterHandleAnim.SetBool ("ApplyingThrust", true);
			thrusterHandleSpriteObject.transform.eulerAngles = new Vector3 (0f, 0f, thrusterAngleDeg + 270f);
			ringSprite.transform.eulerAngles = new Vector3 (0f, 0f, thrusterAngleDeg);
		} else {
			thrusterHandleAnim.SetBool("ApplyingThrust", false);
			shouldPlayThrustSfx = false;
		}
    }

    private void PlayThrusterSfx() {
        if (thrusterAudio.isPlaying || Time.timeScale <= 0f) {
            return;
        }

        thrusterAudio.Play();
    }
    
    void ApplyForce(Transform thrusterTransform, float thrustPower) {
        if (allowThrust && fuel > 0f) {
            Vector3 direction = transform.position - thrusterTransform.position;
            landerRigidBody2D.AddForceAtPosition(direction.normalized*thrustPower, thrusterTransform.position);

            fuel -= 0.01f;
            fuelText.text = "Fuel " + Mathf.Round(fuel);
        }
    }

	void ApplyThrusterForceFromTransformDirection(Transform thrusterTransform, float thrustPower) {
		if (allowThrust && fuel > 0f) {
			Vector3 direction = transform.position - thrusterTransform.position;
			landerRigidBody2D.AddForceAtPosition(direction.normalized*thrustPower, thrusterTransform.position);

			fuel -= 0.01f;
			fuelText.text = "Fuel " + Mathf.Round(fuel);
		}
	}

    private void OnCollisionEnter2D(Collision2D hitInfo) {
        if (hitInfo.relativeVelocity.magnitude > collisionVelocityForDestroy) {
            HandleLanderDestroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Fuel") {
            fuel += 10f;
            Destroy(collider.gameObject);
        }
    }

    private void HandleLanderDestroy() {
        if (explosionPrefab != null) {
            var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
            Destroy(explosion, 1f);
        }

        Destroy(gameObject);
        GameManager.instance.ToggleGameOverPanel(true, false);
    }
}
