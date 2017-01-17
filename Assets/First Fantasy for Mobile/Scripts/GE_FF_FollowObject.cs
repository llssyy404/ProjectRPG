// First Fantasy for Mobile
// Version: 1.4.0
// Compatilble: Unity 5.4.0 or higher, see more info in Readme.txt file.
//
// Developer:			Gold Experience Team (https://www.assetstore.unity3d.com/en/#!/search/page=1/sortby=popularity/query=publisher:4162)
// Unity Asset Store:	https://www.assetstore.unity3d.com/en/#!/content/10822
// GE Store:			https://www.ge-team.com/en/products/first-fantasy-for-mobile/
//
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces

using UnityEngine;
using System.Collections;

#endregion // Namespaces

// ######################################################################
// GE_FF_FollowObject class
// Moves the gameObject follow target object in the scene.
// ######################################################################

public class GE_FF_FollowObject : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################

	#region Variables

	// Target object to follow
	public Transform m_Target = null;

	// Directions to enable/disable
	public bool m_TranslateXPosition = true;
	public bool m_TranslateYPosition = false;
	public bool m_TranslateZPosition = true;

	Bounds m_Bounds;
	Vector3 m_DifCenter;

	#endregion // Variables

	// ########################################
	// MonoBehaviour Functions
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	// ########################################

	#region MonoBehaviour

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start()
	{
		if (m_Target != null)
		{
			// Target object has renderer mesh
			if (m_Target.GetComponent<Renderer>() != null)
			{
				// Find a center of bounds
				m_Bounds = m_Target.GetComponent<Renderer>().bounds;
				foreach (Transform child in m_Target)
				{
					m_Bounds.Encapsulate(child.gameObject.GetComponent<Renderer>().bounds);
				}
			}
			// No renderer mesh in target object
			else
			{
				// Find a center of bounds
				Vector3 center = Vector3.zero;
				foreach (Transform child in m_Target)
				{
					center += child.gameObject.GetComponent<Renderer>().bounds.center;
				}

				// Center is average center of children
				center /= m_Target.childCount;

				// Calculate the bounds by creating a zero sized 'Bounds'
				m_Bounds = new Bounds(center, Vector3.zero);
				foreach (Transform child in m_Target)
				{
					m_Bounds.Encapsulate(child.gameObject.GetComponent<Renderer>().bounds);
				}
			}

			// Set m_DifCenter
			m_DifCenter = m_Bounds.center - m_Target.transform.position;
		}
	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update()
	{
		// Make a move if any TranslatePosition is set
		if (m_TranslateXPosition == true || m_TranslateYPosition == true || m_TranslateZPosition == true)
		{
			// Keep current position
			float x = transform.position.x;
			float y = transform.position.y;
			float z = transform.position.z;

			// Set position to target object
			if (m_TranslateXPosition == true)
				x = m_Target.transform.position.x + m_DifCenter.x;
			if (m_TranslateYPosition == true)
				y = m_Target.transform.position.y + m_DifCenter.y;
			if (m_TranslateZPosition == true)
				z = m_Target.transform.position.z + m_DifCenter.z;

			// Move this object
			transform.position = new Vector3(x, y, z);
		}
	}

	#endregion // MonoBehaviour
}


