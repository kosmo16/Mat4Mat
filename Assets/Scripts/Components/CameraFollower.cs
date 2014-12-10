using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Components
{
    public class CameraFollower : SystemComponent
    {
        public Transform player;
        public float xSmooth;
        public float ySmooth;
        public float minXnear;
        public float maxXnear;
        public float minYnear;
        public float minXfar;
        public float maxXfar;
        public float minYfar;
    }
}
