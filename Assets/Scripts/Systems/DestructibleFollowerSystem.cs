using Components;
using Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Systems
{
	public class DestructibleFollowerSystem : Framework.Core.System
	{
		public override void OnUpdate()
		{
			foreach (DestructibleFollower follower in GetListOf<DestructibleFollower>())
			{
				follower.transform.position = follower.obj.transform.position + follower.startOffset;
			}
		}
	}
}