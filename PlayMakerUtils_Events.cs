//	(c) Jean Fabre, 2013 All rights reserved.
//	http://www.fabrejean.net
//  contact: http://www.fabrejean.net/contact.htm
//
// Version Alpha 0.1

// INSTRUCTIONS
// This set of utils is here to help custom action development, and scripts in general that wants to connect and work with PlayMaker API.


using UnityEngine;

using HutongGames.PlayMaker;

public partial class PlayMakerUtils {

	/// <summary>
	/// Initial work to create some menus for editors
	/// </summary>
	/// <param name="fromFsm">From fsm.</param>
	public static void GetFsmEvents(PlayMakerFSM fromFsm)
	{
		if (fromFsm==null)
		{
			return;
		}

		Debug.Log("fsm events ( found in the events tag, not necessarly used, warning");
		foreach(var _event in fromFsm.FsmEvents)
		{
			Debug.Log(_event.Name +", is global: "+_event.IsGlobal);
		}

		Debug.Log("global transitions events, actually implemented in that fsm");
		foreach(var _globaltransition in fromFsm.FsmGlobalTransitions)
		{
			var _event = _globaltransition.FsmEvent;
			Debug.Log(_event.Name +", is global: "+_event.IsGlobal);
		}

		Debug.Log("global events, within this project");
		foreach(var name in PlayMakerGlobals.Instance.Events)
		{
			Debug.Log(name);
		}

	}

	public static void SendEventToGameObject(PlayMakerFSM fromFsm,GameObject target,string fsmEvent)
	{
		SendEventToGameObject(fromFsm,target,fsmEvent,null);
	}

	public static void SendEventToGameObject(PlayMakerFSM fromFsm,GameObject target,string fsmEvent,FsmEventData eventData)
	{
		if (eventData!=null)
		{
			HutongGames.PlayMaker.Fsm.EventData = eventData;
		}
		
		FsmEventTarget _eventTarget = new FsmEventTarget();
		_eventTarget.excludeSelf = false;
		FsmOwnerDefault owner = new FsmOwnerDefault();
		owner.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
		owner.GameObject = new FsmGameObject();
		owner.GameObject.Value = target;
		_eventTarget.gameObject = owner;
		_eventTarget.target = FsmEventTarget.EventTarget.GameObject;	
			
		_eventTarget.sendToChildren = false;
		
		fromFsm.Fsm.Event(_eventTarget,fsmEvent);
	}
}
