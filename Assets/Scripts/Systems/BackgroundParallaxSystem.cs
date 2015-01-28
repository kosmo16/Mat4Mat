using UnityEngine;

namespace Systems
{
	public class BackgroundParallaxSystem : Framework.Core.System
	{
		public Transform[] backgrounds;				// Array of all the backgrounds to be parallaxed.
		public float parallaxScale;					// The proportion of the camera's movement to move the backgrounds by.
		public float parallaxReductionFactor;		// How much less each successive layer should parallax.
		public float smoothing;						// How smooth the parallax effect should be.
		
		
		private Transform cam;						// Shorter reference to the main camera's transform.
		private Vector3 previousCamPos;				// The postion of the camera in the previous frame.
		
		
		public override void Initialize ()
		{
			cam = Camera.main.transform;
			previousCamPos = cam.position;
		}
		
		public override void OnUpdate()
		{
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScale;

			for(int i = 0; i < backgrounds.Length; i++)
			{
				float backgroundTargetPosX = backgrounds[i].position.x + parallax * (i * parallaxReductionFactor + 1);

				Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

				backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
			}

			previousCamPos = cam.position;
		}
	}
}

