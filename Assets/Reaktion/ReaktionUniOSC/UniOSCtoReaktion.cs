/*
* UniOSC
* Copyright © 2014 Stefan Schlupek
* All rights reserved
* info@monoflow.org
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Reaktion;


namespace UniOSC{

	/// <summary>
	/// Rotates (localRotation) the hosting game object.
	/// For every axis you have a separate OSC address to specify
	/// </summary>
	[AddComponentMenu("UniOSC/UniOSC to Reaktion")]
	public class UniOSCtoReaktion :  UniOSCEventTarget {

	
		OSCInjector injector;
		OSCInjector[] injectorArray;
		List<OSCInjector> injectorList;


		#region private

		private float tempFloat;
		private int tempInt;

		#endregion

		void Awake(){

			injectorArray = FindObjectsOfType (typeof(OSCInjector)) as OSCInjector[];
			injectorList = injectorArray.ToList();

			foreach (OSCInjector inject in injectorList) 
			{
				Debug.Log("Address: " + inject.Address + " Value: " + inject.Value + "Enabled" + inject.On);
			}
		}

		public override void OnEnable(){
			_Init();
			base.OnEnable();
		}

		private void _Init(){
			
			//receiveAllAddresses = false;
			_oscAddresses.Clear();
			if(!receiveAllAddresses){
				foreach (OSCInjector inject in injectorList) 
				{
					_oscAddresses.Add(inject.Address);
				}

			}

		}
	

		public override void OnOSCMessageReceived(UniOSCEventArgs args){
		
			if(args.Message.Data.Count <1)return;
			if(!( args.Message.Data[0]  is  System.Single))return;

			float value = (float)args.Message.Data[0] ;

			foreach (OSCInjector inject in injectorList) 
			{
				if (String.Equals (args.Address, inject.Address)) {
					if(inject.On)
					{
						inject.Value = value;
					}
					//Debug to check working
					//Debug.Log("Address: " + inject.Address + " Value: " + inject.Value);
					}
			}


		}


	}

}