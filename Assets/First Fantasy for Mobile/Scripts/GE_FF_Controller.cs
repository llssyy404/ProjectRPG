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
using UnityEngine.SceneManagement;  // Add since Unity 5.3, see http://docs.unity3d.com/Manual/UpgradeGuide53.html and http://docs.unity3d.com/530/Documentation/ScriptReference/SceneManagement.SceneManager.html

#endregion // Namespaces

// ######################################################################
// GE_FF_Controller class.
// Responds to load scene buttons.
// ######################################################################

public class GE_FF_Controller : MonoBehaviour
{

	// ########################################
	// MonoBehaviour Functions
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	// ########################################

	#region MonoBehaviour

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start()
	{
	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update()
	{
	}

	#endregion // MonoBehaviour

	// ########################################
	// Load scene functions
	// ########################################

	#region Load scene functions

	public void OnButtonLoad01()
	{
		// Unity 5.3 or higher uses SceneManager.LoadScene instead of Application.LoadLevel,
		// see http://docs.unity3d.com/Manual/UpgradeGuide53.html
		// and http://docs.unity3d.com/530/Documentation/ScriptReference/SceneManagement.SceneManager.html
		SceneManager.LoadScene("FF Demo 01 Forest (960x600px)");
	}

	public void OnButtonLoad02()
	{
		// Unity 5.3 or higher uses SceneManager.LoadScene instead of Application.LoadLevel,
		// see http://docs.unity3d.com/Manual/UpgradeGuide53.html
		// and http://docs.unity3d.com/530/Documentation/ScriptReference/SceneManagement.SceneManager.html
		SceneManager.LoadScene("FF Demo 02 Grassland (960x600px)");
	}

	public void OnButtonLoad03()
	{
		// Unity 5.3 or higher uses SceneManager.LoadScene instead of Application.LoadLevel,
		// see http://docs.unity3d.com/Manual/UpgradeGuide53.html
		// and http://docs.unity3d.com/530/Documentation/ScriptReference/SceneManagement.SceneManager.html
		SceneManager.LoadScene("FF Demo 03 Ruin (960x600px)");
	}

	public void OnButtonLoad04()
	{
		// Unity 5.3 or higher uses SceneManager.LoadScene instead of Application.LoadLevel,
		// see http://docs.unity3d.com/Manual/UpgradeGuide53.html
		// and http://docs.unity3d.com/530/Documentation/ScriptReference/SceneManagement.SceneManager.html
		SceneManager.LoadScene("FF Demo 04 Wasteland (960x600px)");
	}

	public void OnButtonLoad05()
	{
		// Unity 5.3 or higher uses SceneManager.LoadScene instead of Application.LoadLevel,
		// see http://docs.unity3d.com/Manual/UpgradeGuide53.html
		// and http://docs.unity3d.com/530/Documentation/ScriptReference/SceneManagement.SceneManager.html
		SceneManager.LoadScene("FF Demo 05 Lagoon (960x600px)");
	}

	public void OnButtonLoad06()
	{
		// Unity 5.3 or higher uses SceneManager.LoadScene instead of Application.LoadLevel,
		// see http://docs.unity3d.com/Manual/UpgradeGuide53.html
		// and http://docs.unity3d.com/530/Documentation/ScriptReference/SceneManagement.SceneManager.html
		SceneManager.LoadScene("FF Demo 06 Cave (960x600px)");
	}

	public void OnButtonLoad07()
	{
		// Unity 5.3 or higher uses SceneManager.LoadScene instead of Application.LoadLevel,
		// see http://docs.unity3d.com/Manual/UpgradeGuide53.html
		// and http://docs.unity3d.com/530/Documentation/ScriptReference/SceneManagement.SceneManager.html
		SceneManager.LoadScene("FF Demo 07 Crater (960x600px)");
	}

	public void OnButtonLoad08()
	{
		// Unity 5.3 or higher uses SceneManager.LoadScene instead of Application.LoadLevel,
		// see http://docs.unity3d.com/Manual/UpgradeGuide53.html
		// and http://docs.unity3d.com/530/Documentation/ScriptReference/SceneManagement.SceneManager.html
		SceneManager.LoadScene("FF Demo 08 Particle Test (960x600px)");
	}

	public void OnButtonLoad09()
	{
		// Unity 5.3 or higher uses SceneManager.LoadScene instead of Application.LoadLevel,
		// see http://docs.unity3d.com/Manual/UpgradeGuide53.html
		// and http://docs.unity3d.com/530/Documentation/ScriptReference/SceneManagement.SceneManager.html
		SceneManager.LoadScene("FF Demo 09 Arctic (960x600px)");
	}

	#endregion // Load scene functions

}
