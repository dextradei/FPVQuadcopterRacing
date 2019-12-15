using UnityEngine;
using System.Collections;

public class QuadController : MonoBehaviour {
	
	public ValueMap PitchMap;
	public ValueMap RollMap;
	public ValueMap YawMap;
	public ValueMap PowerMap;
	public ValueMap AudioMap;
	
	public PID PitchPid;
	public PID RollPid;
	public PID YawPid;

	public AudioSource buzz;
	public AudioSource ding;

	void Update() {
		//buzz.pitch = AudioMap.Map(Mathf.Abs (Input.GetAxis ("Power")));
	}
	
	void FixedUpdate () {

        float dPitch = PitchMap.Map(Input.GetAxis("Pitch"));
		float dYaw = YawMap.Map(Input.GetAxis("Yaw"));
		float dRoll = RollMap.Map(Input.GetAxis("Roll"));

        Vector3 angles = transform.rotation.eulerAngles;
		float pitch = angles.x;
		float roll = angles.z;

        Vector3 angularVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().angularVelocity);
        float vYaw = angularVelocity.y;


        float ePitch = Mathf.DeltaAngle(pitch, dPitch);
		float eYaw = dYaw - vYaw;
		float eRoll = Mathf.DeltaAngle(roll, dRoll);

		float oPitch = PitchPid.Update (ePitch, 1);
		float oYaw = YawPid.Update (eYaw, 1);
		float oRoll = RollPid.Update (eRoll, 1);

        
		//float maxTorque = 0f;
		//float maxYaw = 0.5f;

		//oPitch = Mathf.Clamp (oPitch, -maxTorque, maxTorque);
		//oRoll = Mathf.Clamp (oRoll, -maxTorque, maxTorque);
		//oYaw = Mathf.Clamp (oYaw, -maxYaw, maxYaw);
        

        GetComponent<Rigidbody>().AddTorque(transform.right * oPitch * Time.fixedDeltaTime);
		GetComponent<Rigidbody>().AddTorque(transform.up * oYaw * Time.fixedDeltaTime);
		GetComponent<Rigidbody>().AddTorque(transform.forward * oRoll * Time.fixedDeltaTime);

		float dPower = PowerMap.Map(Input.GetAxis("Power"));
		GetComponent<Rigidbody>().AddForce(transform.up * dPower * Time.fixedDeltaTime);

	}

	public void Ding() {
		ding.Play ();
	}
}
