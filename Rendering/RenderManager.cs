using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine.Rendering {
	public static class RenderManager {
		public static List<RenderObject> renderObjects = new List<RenderObject>();

		public static void Initialize() {
			Vertex[] vertices = {
				new Vertex(new Vector4(-0.5f, 0.5f, 0, 1), Color4.HotPink),
				new Vertex(new Vector4( 0.5f,-0.5f, 0, 1), Color4.HotPink),
				new Vertex(new Vector4(	0, 0.5f,0, 1), Color4.HotPink),
			};

			AddNew(vertices,0);
			Console.WriteLine("Manager Initialized");
		}

		public static void AddNew(Vertex[] vertices,float moveBy) {
			renderObjects.Add(new RenderObject(vertices));
			if (moveBy == 0) { return; }
			//foreach (var v in vertices) { v.MoveBy(moveBy,-moveBy); }
			//Console.WriteLine("moved " + vertices.Length + " vertices");
		}

	public static void Exit() {
			foreach (var obj in renderObjects) { obj.Dispose();	}
		}
		public static void RenderAll() {
			foreach (var obj in renderObjects) { obj.Render(); } //Confirmed this runs
		}

		public static void ClearAll() {
			renderObjects.Clear();
		}





	}
}
