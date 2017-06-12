using System.Collections;
using System.Collections.Generic;

namespace Medusa
{
    public sealed class FixedPhysicsWorld 
    {
        public static List<FixedCollider> colliders = new List<FixedCollider>();

        public static void Simulate()
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                for (int j = 0; j < colliders.Count; j++)
                {
                    FixedCollider c1 = colliders [i];
                    FixedCollider c2 = colliders [j];

                    if (c1 == c2)
                    {
                        continue;
                    }
                    c1.UpdateShape ();
                    c2.UpdateShape ();
                    if (IntersectTest (c1, c2))
                    {
                        c1.CallCollision (c2);
                        c2.CallCollision (c1);
                    }
                }
            }
        }


        #region IntersectTest Methods
        public static bool IntersectTest(FixedCollider collider1, FixedCollider collider2)
        {
            if (collider1.shape == FixedCollider.Shape.Box && collider2.shape == FixedCollider.Shape.Box)
            {
                return IntersectTest_BoxBox (collider1, collider2);
            }
            else if (collider1.shape == FixedCollider.Shape.Sphere && collider2.shape == FixedCollider.Shape.Box)
            {
                return IntersectTest_SphereBox (collider1, collider2);
            }
            else if (collider1.shape == FixedCollider.Shape.Sphere && collider2.shape == FixedCollider.Shape.Sphere)
            {
                return IntersectTest_SphereSphere (collider1, collider2);
            }
            else if (collider1.shape == FixedCollider.Shape.Box && collider2.shape == FixedCollider.Shape.Sphere)
            {
                return IntersectTest_BoxSphere (collider1, collider2);
            }
            return false;
        }

        private static bool IntersectTest_SphereSphere(FixedCollider sphere0, FixedCollider sphere1)
        {
            long sqrDistance = (sphere0.worldPosition - sphere1.worldPosition).sqrMagnitude;
            long delta = sphere0.worldRadius + sphere1.worldRadius;
            return sqrDistance < delta * delta;
        }

        private static bool IntersectTest_BoxBox(FixedCollider box0, FixedCollider box1)
        {
            fixed3 v = box1.worldPosition - box0.worldPosition;

            //too far away
            long radiusSum = box0.worldRadius + box1.worldRadius;
            long sqrDistance = v.sqrMagnitude;
            if (sqrDistance > (radiusSum * radiusSum))
            {
                return false;
            }

            //Compute A's basis
            fixed3 VAx = box0.rightAxis;
            fixed3 VAy = box0.upAxis;
            fixed3 VAz = box0.forwardAxis;

            fixed3[] VA = new fixed3[3];
            VA[0] = VAx;
            VA[1] = VAy;
            VA[2] = VAz;

            //Compute B's basis
            fixed3 VBx = box1.rightAxis;
            fixed3 VBy = box1.upAxis;
            fixed3 VBz = box1.forwardAxis;

            fixed3[] VB = new fixed3[3];
            VB[0] = VBx;
            VB[1] = VBy;
            VB[2] = VBz;

            fixed3 T = new fixed3(fixed3.Dot(v, VAx), fixed3.Dot(v, VAy), fixed3.Dot(v, VAz));

            long[,] R = new long[3, 3];
            long[,] FR = new long[3, 3];
            long ra, rb, t;

            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    R [i, k] = fixed3.Dot (VA [i], VB [k]);
                    FR[i, k] = 1L + FixedMath.Abs(R[i, k]);
                }
            }

            // A's basis vectors
            for (int i = 0; i < 3; i++)
            {
                ra = box0.worldExtends[i];
                rb = FixedMath.Divide (box1.worldExtends [0] * FR [i, 0] + box1.worldExtends [1] * FR [i, 1] + box1.worldExtends [2] * FR [i, 2], FixedMath.PRECISION);
                t = FixedMath.Abs(T[i]);
                if (t > ra + rb) return false;
            }

            // B's basis vectors
            for (int k = 0; k < 3; k++)
            {
                ra = FixedMath.Divide (box0.worldExtends[0] * FR[0, k] + box0.worldExtends[1] * FR[1, k] + box0.worldExtends[2] * FR[2, k], FixedMath.PRECISION);
                rb = box1.worldExtends[k];
                t = FixedMath.Abs (FixedMath.Divide (T [0] * R [0, k] + T [1] * R [1, k] + T [2] * R [2, k], FixedMath.PRECISION));
                if (t > ra + rb) return false;
            }

            //9 cross products

            //L = A0 x B0
            ra = box0.worldExtends[1] * FR[2, 0] + box0.worldExtends[2] * FR[1, 0];
            rb = box1.worldExtends[1] * FR[0, 2] + box1.worldExtends[2] * FR[0, 1];
            t = FixedMath.Abs(T[2] * R[1, 0] - T[1] * R[2, 0]);
            if (t > ra + rb) return false;

            //L = A0 x B1
            ra = box0.worldExtends[1] * FR[2, 1] + box0.worldExtends[2] * FR[1, 1];
            rb = box1.worldExtends[0] * FR[0, 2] + box1.worldExtends[2] * FR[0, 0];
            t = FixedMath.Abs(T[2] * R[1, 1] - T[1] * R[2, 1]);
            if (t > ra + rb) return false;

            //L = A0 x B2
            ra = box0.worldExtends[1] * FR[2, 2] + box0.worldExtends[2] * FR[1, 2];
            rb = box1.worldExtends[0] * FR[0, 1] + box1.worldExtends[1] * FR[0, 0];
            t = FixedMath.Abs(T[2] * R[1, 2] - T[1] * R[2, 2]);
            if (t > ra + rb) return false;

            //L = A1 x B0
            ra = box0.worldExtends[0] * FR[2, 0] + box0.worldExtends[2] * FR[0, 0];
            rb = box1.worldExtends[1] * FR[1, 2] + box1.worldExtends[2] * FR[1, 1];
            t = FixedMath.Abs(T[0] * R[2, 0] - T[2] * R[0, 0]);
            if (t > ra + rb) return false;

            //L = A1 x B1
            ra = box0.worldExtends[0] * FR[2, 1] + box0.worldExtends[2] * FR[0, 1];
            rb = box1.worldExtends[0] * FR[1, 2] + box1.worldExtends[2] * FR[1, 0];
            t = FixedMath.Abs(T[0] * R[2, 1] - T[2] * R[0, 1]);
            if (t > ra + rb) return false;

            //L = A1 x B2
            ra = box0.worldExtends[0] * FR[2, 2] + box0.worldExtends[2] * FR[0, 2];
            rb = box1.worldExtends[0] * FR[1, 1] + box1.worldExtends[1] * FR[1, 0];
            t = FixedMath.Abs(T[0] * R[2, 2] - T[2] * R[0, 2]);
            if (t > ra + rb) return false;

            //L = A2 x B0
            ra = box0.worldExtends[0] * FR[1, 0] + box0.worldExtends[1] * FR[0, 0];
            rb = box1.worldExtends[1] * FR[2, 2] + box1.worldExtends[2] * FR[2, 1];
            t = FixedMath.Abs(T[1] * R[0, 0] - T[0] * R[1, 0]);
            if (t > ra + rb) return false;

            //L = A2 x B1
            ra = box0.worldExtends[0] * FR[1, 1] + box0.worldExtends[1] * FR[0, 1];
            rb = box1.worldExtends[0] * FR[2, 2] + box1.worldExtends[2] * FR[2, 0];
            t = FixedMath.Abs(T[1] * R[0, 1] - T[0] * R[1, 1]);
            if (t > ra + rb) return false;

            //L = A2 x B2
            ra = box0.worldExtends[0] * FR[1, 2] + box0.worldExtends[1] * FR[0, 2];
            rb = box1.worldExtends[0] * FR[2, 1] + box1.worldExtends[1] * FR[2, 0];
            t = FixedMath.Abs(T[1] * R[0, 2] - T[0] * R[1, 2]);
            if (t > ra + rb) return false;

            return true;
        }

        private static bool IntersectTest_BoxSphere(FixedCollider box, FixedCollider sphere)
        {
            return IntersectTest (sphere, box);
        }

        private static bool IntersectTest_SphereBox(FixedCollider sphere, FixedCollider box)
        {

            fixed3 delta = sphere.worldPosition - box.worldPosition;

            //m1-trs inverse
            long x00 = box.rightAxis.x;
            long x01 = box.rightAxis.y;
            long x02 = box.rightAxis.z;

            long x10 = box.upAxis.x;
            long x11 = box.upAxis.y;
            long x12 = box.upAxis.z;

            long x20 = box.forwardAxis.x;
            long x21 = box.forwardAxis.y;
            long x22 = box.forwardAxis.z;

            //m2-trs 
            long y00 = box.rightAxis.x;
            long y01 = box.upAxis.x;
            long y02 = box.forwardAxis.x;

            long y10 = box.rightAxis.y;
            long y11 = box.upAxis.y;
            long y12 = box.forwardAxis.y;

            long y20 = box.rightAxis.z;
            long y21 = box.upAxis.z;
            long y22 = box.forwardAxis.z;


            long dotRotX = FixedMath.Divide (delta.x * y00 + delta.y * y10 + delta.z * y20, FixedMath.PRECISION);
            long dotRotY = FixedMath.Divide (delta.x * y01 + delta.y * y11 + delta.z * y21, FixedMath.PRECISION);
            long dotRotZ = FixedMath.Divide (delta.x * y02 + delta.y * y12 + delta.z * y22, FixedMath.PRECISION);

            fixed3 dRot = new fixed3 (dotRotX, dotRotY, dotRotZ);

            bool outside = false;
            if (dRot.x < -box.worldExtends.x)
            {
                outside = true;
                dRot.x = -box.worldExtends.x;
            }
            else if (dRot.x > box.worldExtends.x)
            {
                outside = true;
                dRot.x = box.worldExtends.x;
            }

            if (dRot.y < -box.worldExtends.y)
            {
                outside = true;
                dRot.y = -box.worldExtends.y;
            }
            else if (dRot.y > box.worldExtends.y)
            {
                outside = true;
                dRot.y = box.worldExtends.y;
            }

            if (dRot.z < -box.worldExtends.z)
            {
                outside = true;
                dRot.z = -box.worldExtends.z;
            }
            else if (dRot.z > box.worldExtends.z)
            {
                outside = true;
                dRot.z = box.worldExtends.z;
            }


            if (outside)
            {
                long f1 = FixedMath.Divide (dRot.x * x00 + dRot.y * x10 + dRot.z * x20,FixedMath.PRECISION);
                long f2 = FixedMath.Divide (dRot.x * x01 + dRot.y * x11 + dRot.z * x21,FixedMath.PRECISION);
                long f3 = FixedMath.Divide (dRot.x * x02 + dRot.y * x12 + dRot.z * x22,FixedMath.PRECISION);

                fixed3 clippedDelta = new fixed3 (f1, f2, f3); 
                fixed3 clippedVec = delta - clippedDelta;

                long lenSquared = clippedVec.sqrMagnitude;
                long radius = sphere.worldRadius;
                if (lenSquared > radius * radius)
                    return false; 
            }

            return true;
        }
        #endregion
    }   
}