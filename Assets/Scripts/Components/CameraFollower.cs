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
    }
}
