using Components;
using Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Systems
{
	public class FixedXPositionSystem : Framework.Core.System
	{
		public float speed;

		public override void OnUpdate()
		{
			foreach (FixedXPosition fixedXPosition in GetListOf<FixedXPosition>())
			{
				fixedXPosition.transform.position = new Vector3(fixedXPosition.xPosition, fixedXPosition.transform.position.y, fixedXPosition.transform.position.z);

				for (int i = 0; i < fixedXPosition.objectsUnder.Count; i++)
				{
					if (!fixedXPosition.objectsUnder[i])
					{
						fixedXPosition.yPosition -= fixedXPosition.sizeOfObjectsUnder[i];
						fixedXPosition.sizeOfObjectsUnder.RemoveAt(i);
						fixedXPosition.objectsUnder.RemoveAt(i);
						break;
					}
				}

				Debug.Log(fixedXPosition.transform.position);
				fixedXPosition.transform.position = Vector3.MoveTowards(
					fixedXPosition.transform.position,
					new Vector3(fixedXPosition.transform.position.x, fixedXPosition.yPosition, fixedXPosition.transform.position.z),
					speed);
			}
		}
	}
}