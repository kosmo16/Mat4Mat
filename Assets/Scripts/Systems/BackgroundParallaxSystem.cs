using UnityEngine;

namespace Systems
{
	public class BackgroundParallaxSystem : Framework.Core.System
	{
		public Transform[] backgrounds;				// Array of all the backgrounds to be parallaxed.
		public float[] parallaxScale;					// The proportion of the camera's movement to move the backgrounds by.
        public float smoothing;
		
		
		private Transform cam;						// Shorter reference to the main camera's transform.
		private Vector3 previousCamPos;				// The postion of the camera in the previous frame.
		
		
		public override void Initialize ()
		{
			cam = Camera.main.transform;
			previousCamPos = cam.position;
		}
		
		public override void OnUpdate()
		{
            Vector3 offset = cam.position - previousCamPos;

			for (int i = 0; i < backgrounds.Length; i++)
			{
                Vector3 parallax = (cam.position - previousCamPos) * parallaxScale[i];
                Vector3 backgroundTargetPos = backgrounds[i].position + parallax;

                //Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * DeltaTime);
			}

            previousCamPos = cam.position;
		}
	}
}

