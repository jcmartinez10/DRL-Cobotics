using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;


public class MatrixCalc : MonoBehaviour {

	// Use this for initialization
	const float a2 = -0.24355f;
	const float a3 = -0.2132f;
	const float d1 = 0.15185f;
	const float d4 = 0.13105f;
	const float d5 = 0.08535f;
	const float d6 = 0.0921f;

	public float x_target;
	public float y_target;
	public float z_target;

	public TorquedMovement movScript;

	public LayerMask IgnoreMe;

	public float[] joints;

	public Transform tarObject;

	GameObject target;
	bool isMouseDragging;
	Vector3 screenPosition;
	Vector3 offset;
	int textIndex = 1;

	public Transform markers;

	void Start () {
		float [] wewe=GetIK(tarObject.rotation,tarObject.position);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown("space"))
        {
            writeEuler();
        }
		if (Input.GetKeyDown("a"))
        {
            readjoints();
        }

		if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            target = ReturnClickedObject(out hitInfo);
            if (target != null)
            {	
				tarObject=target.transform;
                isMouseDragging = true;
                Debug.Log("our target position :" + target.transform.position);
                //Here we Convert world position to screen position.
                screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
                offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
			readSequential();
			retrievePosition();
			
			//writeEuler();
        }

        if (isMouseDragging)
        {
            //tracking mouse position.
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);

            //convert screen position to world position with offset changes.
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;

            //It will update target gameobject's current postion.
            target.transform.position = currentPosition;
        }

	}


	void SolveGeometrically(){
		bool zIsPerp=false;
		

	}


	float[] GetIK(Quaternion tarRotation, Vector3 tarPosition){
		float[] targetJoints = new float[]{0f,0f,0f,0f,0f,0f};
		Vector3 relPos=(transform.position-tarPosition);
		float x_rel=relPos.x;
		float y_rel=relPos.y;
		float z_rel=relPos.z;
		float hipo=Mathf.Sqrt(x_rel*x_rel+z_rel*z_rel);

		targetJoints[0]=Mathf.Atan2(relPos.z, relPos.x) * Mathf.Rad2Deg-15.18f;
		Debug.Log(targetJoints[0]);
		Debug.Log(relPos.sqrMagnitude);
		
		targetJoints[2]=-Mathf.Acos((relPos.sqrMagnitude-0.24355f*0.24355f-0.2132f*0.2132f)/(2*0.24355f*0.2132f)) * Mathf.Rad2Deg-25f;
		Debug.Log(targetJoints[2]);	

		targetJoints[1]=Mathf.Asin(0.2132f*Mathf.Sin(targetJoints[2]*Mathf.Deg2Rad)/relPos.magnitude)* Mathf.Rad2Deg+90f;
		Debug.Log(targetJoints[1]);


		return targetJoints;

	}

	void writeEuler(){

		string rutaEuler = @"c:\UR3\Eulers"+textIndex+".txt";
		string rutaPosi = @"c:\UR3\Position"+textIndex+".txt";
		string eulers = tarObject.rotation.ToString("0.000");
		string posi = tarObject.position.x.ToString("0.000")+","+tarObject.position.z.ToString("0.000")
		+","+tarObject.position.y.ToString("0.000");
		File.WriteAllText(rutaEuler, eulers);
		File.WriteAllText(rutaPosi, posi);
		textIndex+=1;
		Debug.Log("written"); 
		
	}


	void readjoints(){
		string text = File.ReadAllText(@"c:\UR3\Joints.txt"); 
		string[] words = text.Split('\n');

		movScript.q1 = float.Parse(words[0]);
		movScript.q2 = clampRotation(float.Parse(words[1]));
		movScript.q3 = float.Parse(words[2]);
		movScript.q4 = clampRotation(float.Parse(words[3]));
		movScript.q5 = float.Parse(words[4]);
		movScript.q6 = float.Parse(words[5]);

		Debug.Log(words[0]); 
	}

	void readSequential(){
		string text = File.ReadAllText(@"c:\UR3\Joints"+textIndex+".txt");   
		string[] words = text.Split('\n');

		movScript.q1 = float.Parse(words[0]);
		movScript.q2 = clampRotation(float.Parse(words[1]));
		movScript.q3 = float.Parse(words[2]);
		movScript.q4 = clampRotation(float.Parse(words[3]));
		movScript.q5 = float.Parse(words[4]);
		movScript.q6 = float.Parse(words[5]);

		Debug.Log(words[0]); 
	}
	void retrievePosition(){
		Vector3[] positionArray = new Vector3[3];
		positionArray[0]=new Vector3(0.174f,0.219f,-0.327f);
		positionArray[1]=new Vector3(0.156f,0.338f,-0.327f);
		positionArray[2]=new Vector3(0.203f,0.210f,-0.327f);

		markers.position=positionArray[textIndex-1];
		textIndex+=1;
		Debug.Log("Located"); 

	}

	float clampRotation(float rotParam){
		float converted=rotParam+90;

		if (converted>180){
			converted =converted-360;
		}

		return converted;
	}

	GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject targetObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            targetObject = hit.collider.gameObject;
        }
        return targetObject;
    }



}
