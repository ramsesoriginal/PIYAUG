using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// PIYAUG behaviour base. Every script should inherit from this class instead of MonoBehaviour,
/// so we have full control, and can inject additional methos (like type safe invoke, random
/// invoke, ...)
/// </summary>
public class PIYAUGBehaviourBase : MonoBehaviour {
	
	#region ItemLookupByInterface
	/// <summary>
	/// Gets a component by its interface.
	/// </summary>
	/// <returns>
	/// The component with the Interface I
	/// </returns>
	/// <typeparam name='I'>
	/// The Interface we're looking for.
	/// </typeparam>
	public I GetInterfaceComponent<I>() where I : class
	{
	   return GetComponent(typeof(I)) as I;
	}
	
	/// <summary>
	/// Finds all active loaded objects with a given interface.
	/// No assets (meshes, textures, prefabs, ...) or inactive objects will be returned
	/// </summary>
	/// <returns>
	/// The objects with the interface I
	/// </returns>
	/// <typeparam name='I'>
	/// The Interface we're looking for.
	/// </typeparam>
	public static List<I> FindObjectsOfInterface<I>() where I : class
	{
	   MonoBehaviour[] monoBehaviours = FindObjectsOfType(typeof(MonoBehaviour)) as MonoBehaviour[];
	   List<I> list = new List<I>();
	 
	   foreach(MonoBehaviour behaviour in monoBehaviours)
	   {
	      I component = behaviour.GetComponent(typeof(I)) as I;
	 
	      if(component != null)
	      {
	         list.Add(component);
	      }
	   }
	 
	   return list;
	}
	
	#endregion

}
