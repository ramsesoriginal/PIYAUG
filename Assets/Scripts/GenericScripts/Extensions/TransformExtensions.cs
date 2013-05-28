using UnityEngine;
using System.Collections;

/// <summary>
/// Extensions to the Transform class, to simplify Syntax and common tasks.
/// </summary>
public static class TransformExtensions {
	
	/// <summary>
	/// Sets the x part of the position.
	/// </summary>
	/// <param name='x'>
	/// The new x value
	/// </param>
	public static void SetX(this Transform transform, float x)
	{
	  transform.SetXYZ(x, transform.position.y, transform.position.z);
	}
	
	/// <summary>
	/// Sets the y part of the position
	/// </summary>
	/// <param name='y'>
	/// The new y value
	/// </param>
	public static void SetY(this Transform transform, float y)
	{
	  transform.SetXYZ(transform.position.x, y, transform.position.z);
	}
	
	/// <summary>
	/// Sets the z part of the position
	/// </summary>
	/// <param name='z'>
	/// The new z Value
	/// </param>
	public static void SetZ(this Transform transform, float z)
	{
	  transform.SetXYZ(transform.position.x, transform.position.y, z);
	}
	
	/// <summary>
	/// Sets the x, y and z values of the positions
	/// </summary>
	/// <param name='x'>
	/// The new x value
	/// </param>
	/// <param name='y'>
	/// The new y value
	/// </param>
	/// <param name='z'>
	/// The new z Value
	/// </param>
	public static void SetXYZ(this Transform transform, float x, float y, float z)
	{
	  Vector3 newPosition = 
	     new Vector3(x, y, z);
	
	  transform.position = newPosition;
	}
}
