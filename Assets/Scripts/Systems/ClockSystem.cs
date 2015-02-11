using Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Systems
{
    public class ClockSystem : Framework.Core.System
    {
        public Rect rect = new Rect(Screen.width / 3, 10, 395, 48);

        public Texture2D basicBar;
        public Texture2D fullBar;
        public Texture2D emptyBar;
        public float maxTime = 30.0f;
        public Vector2 offset = new Vector2(11, 9);

        private Event0 playerLose;
        private float currentTime;

        public override void Initialize()
        {
            playerLose = GetEvent(Event.PlayerLose);
            currentTime = maxTime;
        }

        public override void OnFixedUpdate()
        {
 	         currentTime -= FixedDeltaTime;

             if (currentTime <= 0.0f)
             {
                 playerLose.Report();
             }
        }

        public void OnGUI()
        {
            float ratio = currentTime / maxTime;
            int width = fullBar.width;

            int targetWidth = (int)((float) width * ratio);
            Debug.Log(targetWidth);
            Texture2D targetTexture = new Texture2D(basicBar.width, basicBar.height);
            targetTexture.SetPixels32(basicBar.GetPixels32());
            targetTexture.SetPixels((int)offset.x, (int)offset.y, targetWidth, fullBar.height, fullBar.GetPixels(0, 0, targetWidth, fullBar.height));
            targetTexture.SetPixels((int)offset.x + targetWidth, (int)offset.y, fullBar.width - targetWidth, emptyBar.height, emptyBar.GetPixels(targetWidth, 0, fullBar.width - targetWidth, fullBar.height));
            targetTexture.Apply(false);
            GUI.backgroundColor = Color.clear;
            GUI.Button(rect, targetTexture);
        }
    }
}
