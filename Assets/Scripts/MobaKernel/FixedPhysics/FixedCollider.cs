using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Medusa
{
    public delegate void OnCollisionHandler(FixedCollider collider);

    [System.Serializable]
    public class FixedCollider 
    {
        public enum Shape
        {
            Box,
            Sphere,
            None
        }

        public Shape shape = Shape.Box;

        public OnCollisionHandler onCollisionHandler = null;

        #region pool
        private static Queue<FixedCollider> g_pool = new Queue<FixedCollider>();

        static FixedCollider()
        {
            //pool 5
            for (int i = 0; i < 5; i++)
            {
                g_pool.Enqueue (new FixedCollider ());
            }
        }

        public static FixedCollider Generate()
        {
            if (g_pool.Count > 0)
            {
                return g_pool.Dequeue ();
            }
            return new FixedCollider ();
        }
        #endregion

        #region box & sphere
        [SerializeField]
        private fixed3 _rightAxis = fixed3.right;
        public fixed3 rightAxis
        {
            get{ return _rightAxis; }
            set{ _rightAxis = value;}
        }

        [SerializeField]
        private fixed3 _upAxis = fixed3.up;
        public fixed3 upAxis
        {
            get{ return _upAxis; }
            set
            {
                if (value != _upAxis)
                {
                    _upAxis = value;
                    isDirty = true;
                }
            }
        }

        [SerializeField]
        private fixed3 _forwardAxis = fixed3.forward;
        public fixed3 forwardAxis
        {
            get{ return _forwardAxis; }
            set
            {
                if (value != _forwardAxis)
                {
                    _forwardAxis = value;
                    isDirty = true;
                }
            }
        }

        [SerializeField]
        private fixed3 _worldPosition = fixed3.zero;
        public fixed3 worldPosition
        {
            get{ return _worldPosition; }
            set
            {
                if (value != _worldPosition)
                {
                    _worldPosition = value;
                    isDirty = true;
                }
            }
        }

        [SerializeField]
        private long _worldRadius = 1000L;
        //base on size..only set in update shape function
        public long worldRadius 
        {
            get{ return _worldRadius;}
            set{ _worldRadius = value;}
        }
        #endregion

        #region box
        [SerializeField]
        private fixed3 _size = fixed3.one;
        public fixed3 size
        {
            get{ return _size;}
            set
            {
                if (value != _size)
                {
                    _size = value;
                    isDirty = true;
                }
            }
        }

        [SerializeField]
        private fixed3 _worldExtends = fixed3.half;
        //base on size..only set in update shape function
        public fixed3 worldExtends 
        {
            get{ return _worldExtends;}
            set{ _worldExtends = value;}
        }
        #endregion


        protected bool isDirty = false;

        public void UpdateShape ()
        {
            if(isDirty)
            {
                if (shape == Shape.Box)
                {
                    worldExtends = size.GetHalf ();
                    rightAxis = fixed3.Cross (upAxis, forwardAxis);
                    _worldRadius = worldExtends.GetMax ();
                }
                else if (shape == Shape.Sphere)
                {
                    rightAxis = fixed3.Cross (upAxis, forwardAxis);
                }
                isDirty = false;
            }
        }

        public void CallCollision (FixedCollider another)
        {
            if (onCollisionHandler != null)
            {
                onCollisionHandler (another);
            }
        }

        public void Recycle ()
        {
            onCollisionHandler = null;
            if (g_pool.Count < 30)
            {
                forwardAxis = fixed3.forward;
                upAxis = fixed3.up;
                rightAxis = fixed3.right;

                worldPosition = fixed3.zero;
                worldRadius = 500;
                size = fixed3.one;
                worldExtends = fixed3.half;

                g_pool.Enqueue (this);
            }
        }
    }
}

