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
				new Vertex(new Vector4(-0.25f, 0.25f, 0.5f, 1-0f), Color4.HotPink),
				new Vertex(new Vector4( 0.0f, -0.25f, 0.5f, 1-0f), Color4.HotPink),
				new Vertex(new Vector4( 0.25f, 0.25f, 0.5f, 1-0f), Color4.HotPink),
			};
			AddNew(vertices);
			Console.WriteLine("Manager Initialized");
		}

		public static void AddNew(Vertex[] vertices) {
			renderObjects.Add(new RenderObject(vertices));
		}
		
		public static void Exit() {
			foreach (var obj in renderObjects) { obj.Dispose();	}
		}
		public static void RenderAll() {
			foreach (var obj in renderObjects) { obj.Render(); } //Confirmed this runs
		}

	}
}
