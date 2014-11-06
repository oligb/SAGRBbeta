using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {
	public bool MoveVertically = false;
	public bool MirroredMovement = false;

	public GameObject hipCenter;
	public GameObject head;
	public GameObject leftShoulder;
	public GameObject leftHand;
	public GameObject rightShoulder;
	public GameObject rightHand;


	private Vector3 initialPosition;
	private Quaternion initialRotation;
	private Vector3 initialPosOffset = Vector3.zero;
	private uint initialPosUserID = 0;

	public Vector3 fixShoulderVector= Vector3.zero;
	public Vector3 hipOffset=Vector3.zero;
	public Vector3 fixGuyRotate=Vector3.zero;
	public float scaleX;
	public float scaleY;
	public float scaleZ;

	public GameObject[] relevantJoints;
	
	public int[] jointIndexCorrespondence;

	// Use this for initialization
	void Start () {
		relevantJoints = new GameObject[6] {
			hipCenter,head,leftShoulder,leftHand,rightShoulder,rightHand
		};
		jointIndexCorrespondence = new  int[6]{
			0,3,4,7,8,11
		};

	}
	
	// Update is called once per frame
	void Update () {
		// get 1st player
		uint playerID = KinectManager.Instance != null ? KinectManager.Instance.GetPlayer1ID() : 0;
		/*
		if(playerID <= 0)
		{
			// reset the pointman position and rotation
			if(transform.position != initialPosition)
				transform.position = initialPosition;
			
			if(transform.rotation != initialRotation)
				transform.rotation = initialRotation;
			
			return;
		}
		
*/		
		Vector3 posPointMan = KinectManager.Instance.GetUserPosition(playerID);


		Quaternion rotateWholeGuy = KinectManager.Instance.GetJointOrientation(playerID, 2, !MirroredMovement);
		rotateWholeGuy.eulerAngles+=fixGuyRotate;
		transform.localRotation=rotateWholeGuy;


		for(int i =0;i<relevantJoints.Length;i++){
			if(relevantJoints[i] != null)
			{
				if(KinectManager.Instance.IsJointTracked(playerID, jointIndexCorrespondence[i]))
				{
					relevantJoints[i].gameObject.SetActive(true);
					
					if(jointIndexCorrespondence[i]==4){
						//Vector3 posHand = KinectManager.Instance.GetJointPosition(playerID, 7);
						Quaternion rotJoint = KinectManager.Instance.GetJointOrientation(playerID, 4, !MirroredMovement);
						rotJoint.eulerAngles+=fixShoulderVector;
						relevantJoints[i].transform.localPosition=new Vector3(-1f,0f,0f);
						relevantJoints[i].transform.localRotation=rotJoint;
					}
					else if(jointIndexCorrespondence[i]==8){
						//Vector3 posHand = KinectManager.Instance.GetJointPosition(playerID, 11);
						Quaternion rotJoint = KinectManager.Instance.GetJointOrientation(playerID, 8, !MirroredMovement);
						rotJoint.eulerAngles+=fixShoulderVector;
						relevantJoints[i].transform.localPosition= new Vector3(1f,0f,0f);
						relevantJoints[i].transform.localRotation=rotJoint;
					}
					else if(jointIndexCorrespondence[i]==0){
						Vector3 posHip =  KinectManager.Instance.GetJointPosition(playerID, 0);
						posHip.x *=scaleX;
						posHip.y *=scaleY;
						posHip.z *=scaleZ;
						relevantJoints[i].transform.localPosition= posHip;
					}
					else{
					Vector3 posJoint = KinectManager.Instance.GetJointPosition(playerID, jointIndexCorrespondence[i]);
					relevantJoints[i].transform.localPosition=posJoint;
					posJoint -= posPointMan;
					posJoint.z = -posJoint.z;
					}
				}
				else{

				//relevantJoints[i].gameObject.SetActive(false);
				}
			}
		}



		/*
		// update the local positions of the bones
		int jointsCount = (int)KinectWrapper.NuiSkeletonPositionIndex.Count;
		
		for(int i = 0; i < jointsCount; i++) 
		{
			if(relevantJoints] != null)
			{
				if(KinectManager.Instance.IsJointTracked(playerID, i))
				{
					//_bones[i].gameObject.SetActive(true);
					
					int joint = MirroredMovement ? KinectWrapper.GetSkeletonMirroredJoint(i): i;
					Vector3 posJoint = KinectManager.Instance.GetJointPosition(playerID, joint);
					posJoint.z = !MirroredMovement ? -posJoint.z : posJoint.z;
					Quaternion rotJoint = KinectManager.Instance.GetJointOrientation(playerID, joint, !MirroredMovement);
					
					posJoint -= posPointMan;
					posJoint.z = -posJoint.z;
					
					if(MirroredMovement)
					{
						posJoint.x = -posJoint.x;
					}
					
					
					if(i==4){
						
						//Vector3 posLeftShoulder = KinectManager.Instance.GetJointPosition(playerID, 4);
						//	Vector3 posLeftHand = KinectManager.Instance.GetJointPosition(playerID, 7);
						//	rotJoint=Vector3.Angle(
						rotJoint.eulerAngles+=mirrorShoulderVector;
					}
					
					if(i==8){
						rotJoint.eulerAngles-=mirrorShoulderVector;
					}
					
					
					_bones[i].transform.localPosition = posJoint;
					_bones[i].transform.localRotation = rotJoint;
				}
				
				else
				{
					_bones[i].gameObject.SetActive(false);
				}
			}	
		}
		
		//		Vector3 direction = target - transform.position;
		//transform.rotation = Quaternion.LookRotation(direction);
		
		*/
		
		
	}

}
