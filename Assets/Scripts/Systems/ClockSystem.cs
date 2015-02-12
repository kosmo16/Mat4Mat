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
        public RectTransform rectTransform;
        public float maxTime = 30.0f;
        public float addTime = 15.0f;

        private float maxWidth;
        private Event0 playerLose;
        private float currentTime;

        public override void Initialize()
        {
            SubscribeEvent(Event.GatherCoin, OnGatherCoin);

            maxWidth = rectTransform.rect.width;
            playerLose = GetEvent(Event.PlayerLose);
            currentTime = maxTime;
        }

        private void OnGatherCoin()
        {
            currentTime = Mathf.Clamp(currentTime + addTime, 0.0f, maxTime);
        }

        public override void OnFixedUpdate()
        {
            float ratio = currentTime / maxTime;
            float width = ratio * maxWidth;
            rectTransform.offsetMax = new Vector2(-(maxWidth - width), 0f);
 	         currentTime -= FixedDeltaTime;

             if (currentTime <= 0.0f)
             {
                 playerLose.Report();
             }
        }
    }
}
