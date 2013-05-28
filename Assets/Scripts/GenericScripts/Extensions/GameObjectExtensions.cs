using UnityEngine;
using System.Collections;

/// <summary>
/// Extensions to the GameObject class, to simplify Syntax and common tasks.
/// </summary>
public static class GameObjectExtensions {
	
	/// <summary>
	/// Gets a Component safely, without requiring "RequiredComponent"
	/// </summary>
	/// <returns>
	/// The component or null
	/// </returns>
	/// <typeparam name='T'>
	/// The component Type.
	/// </typeparam>
	public static T GetSafeComponent<T>(this GameObject obj) where T : MonoBehaviour
	{
		T component = obj.GetComponent<T>();
		
		if(component == null)
		{
		  Debug.LogError("Expected to find component of type " 
		     + typeof(T) + " but found none", obj);
		}
		
		return component;
	}
}
