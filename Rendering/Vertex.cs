using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine.Rendering {
	public struct Vertex {
		public const int Size = (4 + 4) * 4; // size of struct in bytes

		public Vector4 _position;
		public Color4 _color;

		public Vertex(Vector4 position,Color4 color) {
			_position = position;
			_color = color;
		}

		public Vertex(float x,float y,float width,float height) {
			_position = new Vector4(x,y,width,height);
			_color = Color4.PaleGreen;
		}

	}
}
