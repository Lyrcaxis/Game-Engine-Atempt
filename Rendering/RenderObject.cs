using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Game_Engine.Rendering {

	public class RenderObject:IDisposable {

		readonly int _vertexArray;
		readonly int _buffer;
		readonly int _verticeCount;
		Vertex[] vertices;

		public RenderObject(Vertex[] vertices) {

			_verticeCount = vertices.Length;
			_vertexArray = GL.GenVertexArray();
			_buffer = GL.GenBuffer();
			this.vertices = vertices;
			
			//Buffer
			GL.BindVertexArray(_vertexArray);
			GL.BindBuffer(BufferTarget.ArrayBuffer,_buffer);
			GL.NamedBufferStorage(_buffer,Vertex.Size * _verticeCount,vertices,BufferStorageFlags.MapWriteBit);

			//Bind Position
			GL.VertexArrayAttribBinding(_vertexArray,0,0);
			GL.EnableVertexArrayAttrib(_vertexArray,0);
			GL.VertexArrayAttribFormat(_vertexArray,0,4,VertexAttribType.Float,false,0);
			
			//Bind Color 
			GL.VertexArrayAttribBinding(_vertexArray,1,0);
			GL.EnableVertexArrayAttrib(_vertexArray,1);
			GL.VertexArrayAttribFormat(_vertexArray,1,4,VertexAttribType.Float,false,16);
			
			//Bind mesh to Transform
			GL.VertexArrayVertexBuffer(_vertexArray,0,_buffer,IntPtr.Zero,Vertex.Size);
			Console.WriteLine("Initialized: VA:{0} VB:{1} VC:{2}",_vertexArray,_buffer,_verticeCount);
		}

		public void Render() {
			GL.BindVertexArray(_vertexArray);
			GL.DrawArrays(PrimitiveType.Triangles,0,_verticeCount);
			//Console.WriteLine("Rendering");
		}

		public virtual void Dispose() {
			GL.DeleteVertexArray(_vertexArray);
			GL.DeleteBuffer(_buffer);

			GC.SuppressFinalize(this);
		}


		Vector4 pos;

		public void Move(float x=0,float y=0,float z=0) {

			for (int i = 0; i < vertices.Length; i++) {
				Vertex v = vertices[i];
			
				v._position.X += x;
				v._position.Y += y;
				v._position.Z += z;
				Console.WriteLine("Moving to " + v._position);
			}

			pos += new Vector4(x,y,z,0);

		}

	}
}
