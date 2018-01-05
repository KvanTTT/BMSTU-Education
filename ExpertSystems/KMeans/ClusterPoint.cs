using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace System.Data.Clustering
{
	public class ClusterPoint
	{
        /// <summary>
        /// Gets or sets X-coord of the point
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Gets or sets Y-coord of the point
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Gets or sets some additional data for point
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Gets or sets cluster index
        /// </summary>
        public float ClusterIndex { get; set; }

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="x">X-coord</param>
        /// <param name="y">Y-coord</param>
        public ClusterPoint(float x, float y)
		{
			this.X = x;
            this.Y = y;
            this.ClusterIndex = -1;
		}

        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="x">X-coord</param>
        /// <param name="y">Y-coord</param>
        public ClusterPoint(float x, float y, object tag)
        {
            this.X = x;
            this.Y = y;
            this.Tag = tag;
            this.ClusterIndex = -1;
        }
	}

}
