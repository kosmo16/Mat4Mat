using Content;
using Framework.Core;
using UnityEngine;
using System.Collections.Generic;

namespace Components
{
	public class FixedXPosition : SystemComponent
	{
		public float xPosition;
		public float yPosition;
		public List<GameObject> objectsUnder;
		public List<float> sizeOfObjectsUnder;

		
		public void Start()
		{
			xPosition = transform.position.x;
			yPosition = transform.position.y;
		}
	}
}