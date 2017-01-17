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
using UnityEngine.UI;

#endregion // Namespaces

// ######################################################################
// GE_FF_TestParticles class
// Handles user key inputs, instantiate and destroy the particle effects.
//
// User Keys:
//	Up/Down key to switch Element; Fire, Water, Wind, Earth, Light, Darkness.
//	Left/Right key to switch particle in current Element.
//
// Note this class is attached with "Canvas (Particles)" object in "FF Demo 08 Particle Test (960x600px)" scene.
// ######################################################################

public class GE_FF_TestParticles : MonoBehaviour
{
	// ########################################
	// Variables
	// ########################################

	#region Variables

	// Prefabs of particles for each element
	public GameObject[] m_PrefabListFire;
	public GameObject[] m_PrefabListWater;
	public GameObject[] m_PrefabListWind;
	public GameObject[] m_PrefabListEarth;
	public GameObject[] m_PrefabListLight;
	public GameObject[] m_PrefabListDarkness;

	// Current element index
	int m_CurrentElementIndex = -1;

	// Current particle index
	int m_CurrentParticleIndex = -1;

	// Name of current element
	string m_ElementName = "";

	// Name of particle
	string m_ParticleName = "";

	// Current particle list
	GameObject[] m_CurrentElementList = null;

	// GameObject of current particle that is showing in the scene
	GameObject m_CurrentParticle = null;

	Text m_TextCategoryHeader = null;
	Text m_TextParticleHeader = null;

	Text m_TextCategory = null;
	Text m_TextParticle = null;

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

		GameObject go = GameObject.Find("Text Category");
		if (go)
			m_TextCategoryHeader = go.GetComponent<Text>();
		go = GameObject.Find("Text Category_Name");
		if (go)
			m_TextCategory = go.GetComponent<Text>();

		go = GameObject.Find("Text Particle");
		if (go)
			m_TextParticleHeader = go.GetComponent<Text>();
		go = GameObject.Find("Text Particle_Name");
		if (go)
			m_TextParticle = go.GetComponent<Text>();

		// Check if there is any particle in prefab list
		if (m_PrefabListFire.Length > 0 ||
			m_PrefabListWater.Length > 0 ||
			m_PrefabListWind.Length > 0 ||
			m_PrefabListEarth.Length > 0 ||
			m_PrefabListLight.Length > 0 ||
			m_PrefabListDarkness.Length > 0)
		{
			// reset indices of element and particle
			m_CurrentElementIndex = 0;
			m_CurrentParticleIndex = 0;

			// Show particle
			ShowParticle();
		}
	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update()
	{

		// Check if there is any particle in prefab list
		if (m_CurrentElementIndex != -1 && m_CurrentParticleIndex != -1)
		{
			// User released Up arrow key
			if (Input.GetKeyUp(KeyCode.UpArrow))
			{
				OnPreviousCategory();
			}
			// User released Down arrow key
			else if (Input.GetKeyUp(KeyCode.DownArrow))
			{
				OnNextCategory();
			}
			// User released Left arrow key
			else if (Input.GetKeyUp(KeyCode.LeftArrow))
			{
				OnPreviousParticle();
			}
			// User released Right arrow key
			else if (Input.GetKeyUp(KeyCode.RightArrow))
			{
				OnNextParticle();
			}
		}
	}

	#endregion // MonoBehaviour

	// ########################################
	// Switch Category functions
	// ########################################

	#region Switch Category functions

	public void OnPreviousCategory()
	{
		m_CurrentElementIndex--;
		m_CurrentParticleIndex = 0;
		ShowParticle();
	}

	public void OnNextCategory()
	{
		m_CurrentElementIndex++;
		m_CurrentParticleIndex = 0;
		ShowParticle();
	}

	#endregion // Switch Category functions

	// ########################################
	// Switch Particle functions
	// ########################################

	#region Switch Particle functions

	public void OnPreviousParticle()
	{
		m_CurrentParticleIndex--;
		ShowParticle();
	}

	public void OnNextParticle()
	{
		m_CurrentParticleIndex++;
		ShowParticle();
	}

	#endregion // Switch Particle functions

	// ########################################
	// Show particle functions
	// ########################################

	#region Show particle functions

	// Remove old Particle and do Create new Particle GameObject
	void ShowParticle()
	{
		// Make m_CurrentElementIndex be rounded
		if (m_CurrentElementIndex > 5)
		{
			m_CurrentElementIndex = 0;
		}
		else if (m_CurrentElementIndex < 0)
		{
			m_CurrentElementIndex = 5;
		}

		// update current m_CurrentElementList and m_ElementName
		if (m_CurrentElementIndex == 0)
		{
			m_CurrentElementList = m_PrefabListFire;
			m_ElementName = "FIRE";
		}
		else if (m_CurrentElementIndex == 1)
		{
			m_CurrentElementList = m_PrefabListWater;
			m_ElementName = "WATER";
		}
		else if (m_CurrentElementIndex == 2)
		{
			m_CurrentElementList = m_PrefabListWind;
			m_ElementName = "WIND";
		}
		else if (m_CurrentElementIndex == 3)
		{
			m_CurrentElementList = m_PrefabListEarth;
			m_ElementName = "EARTH";
		}
		else if (m_CurrentElementIndex == 4)
		{
			m_CurrentElementList = m_PrefabListLight;
			m_ElementName = "LIGHT";
		}
		else if (m_CurrentElementIndex == 5)
		{
			m_CurrentElementList = m_PrefabListDarkness;
			m_ElementName = "DARKNESS";
		}

		if (m_TextCategoryHeader)
			m_TextCategoryHeader.text = "C A T E G O R Y (" + (m_CurrentElementIndex + 1).ToString() + "/6)";

		if (m_TextCategory)
			m_TextCategory.text = m_ElementName;

		// Make m_CurrentParticleIndex be rounded
		if (m_CurrentParticleIndex >= m_CurrentElementList.Length)
		{
			m_CurrentParticleIndex = 0;
		}
		else if (m_CurrentParticleIndex < 0)
		{
			m_CurrentParticleIndex = m_CurrentElementList.Length - 1;
		}

		// update current m_ParticleName
		m_ParticleName = m_CurrentElementList[m_CurrentParticleIndex].name;

		if (m_TextParticleHeader)
			m_TextParticleHeader.text = "P A R T I C L E (" + (m_CurrentParticleIndex + 1).ToString() + "/" + m_CurrentElementList.Length.ToString() + ")";

		if (m_TextParticle)
			m_TextParticle.text = m_ParticleName;

		// Remove Old particle
		if (m_CurrentParticle != null)
		{
			DestroyObject(m_CurrentParticle);
		}

		// Create new particle
		m_CurrentParticle = (GameObject)Instantiate(m_CurrentElementList[m_CurrentParticleIndex]);
	}

	#endregion // Show particle functions
}
