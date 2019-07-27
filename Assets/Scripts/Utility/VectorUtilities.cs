using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoUnity
{
    public static class VectorUtilities
    {

        public static float Vector2ToAngle(Vector2 direction)
        {
            float angle = 0;
            if (direction.x >= 0)
            {
                angle = -1 * Vector2.Angle(new Vector2(0, 1), direction);
            }
            else
            {
                angle = Vector2.Angle(new Vector2(0, 1), direction);
            }
            return angle;
        }

        public static Vector3 PositionToTangentVector(Vector3 position, Vector3 levelCenter)
        {
            Vector3 flatPosition = GetFlatVector3(position);
            Vector3 flatLevelCenter = GetFlatVector3(levelCenter);
            Vector3 radiusVector = flatPosition - flatLevelCenter;

            return Vector3.Normalize(Vector3.Cross(radiusVector, Vector3.up));
            
        }

        public static Vector3 GetFlatVector3(Vector3 vector)
        {
            return new Vector3(vector.x, 0, vector.z);
        }

        public static Vector3 GetFlatVector3AtHeight(Vector3 vector, float height)
        {
            return new Vector3(vector.x, height, vector.z);
        }

    }
}
