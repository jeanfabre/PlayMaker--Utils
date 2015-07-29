// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using UnityEngine;
using System.Collections;

using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Ecosystem.Utils
{
	public class PlayMakerEventProxy : MonoBehaviour {

		public PlayMakerEventTarget eventTarget = new PlayMakerEventTarget(false);
		
		[EventTargetVariable("eventTarget")]
		//[ShowOptions]
		public PlayMakerEvent fsmEvent;

		public bool debug;

		public void SendPlayMakerEvent()
		{

			if (!Application.isPlaying)
			{
				UnityEngine.Debug.Log("<color=RED>Application must run to send a PlayMaker Event, but the proxy at least works:</color>",this);
				return;
			}

			if (debug)
			{
				UnityEngine.Debug.Log("SendPlayMakerEvent "+fsmEvent+" on "+eventTarget,this);
			}

			fsmEvent.SendEvent(null,eventTarget);
		}
	}
}
