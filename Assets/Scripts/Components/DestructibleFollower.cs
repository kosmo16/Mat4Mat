using Content;
using Framework.Core;
using UnityEngine;

namespace Components
{
	public class DestructibleFollower : SystemComponent
	{
		public Vector3 startOffset;
		public GameObject obj;

		public void Start()
		{
			startOffset = new Vector3 (
				transform.position.x - obj.transform.position.x,
				transform.position.y - obj.transform.position.y,
				0.0f);
		}
	}
}