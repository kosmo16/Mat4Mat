using Components;
using Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Systems
{
    public class CameraFollowerSystem : Framework.Core.System
    {
        public float farSize;
        public float nearSize;

        private bool isFar;

        public override void Initialize()
        {
            SubscribeEvent(Event.NeedClue, OnNeedClue);
        }

        public override void OnUpdate()
        {
            SetCamera();
            FollowPlayer(GetFirstOrNull<CameraFollower>());
        }

        private void SetCamera()
        {
            Camera mainCamera = Camera.main;

            if (isFar)
            {
                mainCamera.orthographicSize = farSize;
            }
            else
            {
                mainCamera.orthographicSize = nearSize;
            }
        }

        private void OnNeedClue()
        {
            isFar = !isFar;
        }

        private void FollowPlayer(CameraFollower follower)
        {

            float targetX = Mathf.Lerp(
                follower.transform.position.x,
                follower.player.position.x,
                follower.xSmooth * DeltaTime);

            float targetY = Mathf.Lerp(
                follower.transform.position.y,
                follower.player.position.y,
                follower.ySmooth * DeltaTime);

            float aspectRatio = (float) Screen.width / (float)Screen.height;

            targetX = Mathf.Clamp(targetX,
                follower.left.position.x + aspectRatio * Camera.main.orthographicSize,
                follower.right.position.x - aspectRatio * Camera.main.orthographicSize);

            targetY = Mathf.Clamp(targetY,
                follower.down.position.y + Camera.main.orthographicSize,
                follower.up.position.y - Camera.main.orthographicSize);
            
            follower.transform.position = new Vector3(targetX, targetY, follower.transform.position.z);
        }
    }
}
