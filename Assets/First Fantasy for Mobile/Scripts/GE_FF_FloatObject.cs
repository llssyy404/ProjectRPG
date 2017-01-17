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
// GE_FF_FloatObject class
// Floats object up/down on the water.
// ######################################################################

public class GE_FF_FloatObject : MonoBehaviour
{
	// ########################################
	// Variables
	// ########################################

	#region Variables

	public float m_Time = 2.0f;
	public float m_TimeSpread = 0.25f;
	public float m_HorizontalSpread = 0.25f;
	public float m_VerticalSpread = 0.25f;

	float m_TimeRound = 1;
	float m_TimeCount = 0;
	Vector3 m_StartPosition;
	Vector3 m_EndPosition;

	enum statMove
	{
		statMoveBegin,
		statMoveAway,
		statMoveBack
	};
	statMove m_statMove = statMove.statMoveBegin;

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

		// Keep original position for floating forward/backward
		m_StartPosition = this.transform.localPosition;

		// Setup for first move
		SetupNewMove();
	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update()
	{
		if (m_TimeCount >= m_TimeRound)
		{
			// Setup next move
			SetupNewMove();
		}
		else
		{
			float CalTime = m_TimeCount / m_TimeRound;
			// Floating forward
			if (m_statMove == statMove.statMoveAway)
			{
				transform.localPosition = Vector3.Lerp(m_StartPosition, m_EndPosition, CalTime);
			}
			// Floating backward
			else
			{
				transform.localPosition = Vector3.Lerp(m_EndPosition, m_StartPosition, CalTime);
			}
			m_TimeCount += Time.deltaTime;
		}
	}

	#endregion // MonoBehaviour

	// ########################################
	// Utilities functions
	// ########################################

	#region Utilities functions

	void SetupNewMove()
	{
		// Random round time
		m_TimeRound = m_Time + Random.Range(-m_TimeSpread, m_TimeSpread);
		m_TimeCount = 0;

		// Check for update float state and random next position
		if (m_statMove == statMove.statMoveAway)
		{
			m_statMove = statMove.statMoveBack;
		}
		else if (m_statMove == statMove.statMoveBack || m_statMove == statMove.statMoveBegin)
		{
			// Random next position
			m_EndPosition = m_StartPosition + new Vector3(Random.Range(-m_HorizontalSpread, m_HorizontalSpread), Random.Range(-m_VerticalSpread, m_VerticalSpread), Random.Range(-m_HorizontalSpread, m_HorizontalSpread));
			m_statMove = statMove.statMoveAway;
		}
	}

	#endregion // Utilities functions
}


