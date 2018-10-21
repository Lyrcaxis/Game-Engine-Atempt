using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Game_Engine.Rendering {

	public class RenderObject:IDisposable {


		private readonly int _vertexArray;
		private readonly int _buffer;
		private readonly int _verticeCount;

		public RenderObject(Vertex[] vertices) {

			_verticeCount = vertices.Length;
			_vertexArray = GL.GenVertexArray();
			_buffer = GL.GenBuffer();

			InitializeBuffer();
			InitializeColor();
			InitializeTransform();
   			
			//Bind mesh to Transform
			GL.VertexArrayVertexBuffer(_vertexArray,0,_buffer,IntPtr.Zero,Vertex.Size);

			#region SubMethods

			void InitializeBuffer() {
				GL.BindVertexArray(_vertexArray);
				GL.BindBuffer(BufferTarget.ArrayBuffer,_buffer);
				GL.NamedBufferStorage(_buffer,Vertex.Size * _verticeCount,vertices,BufferStorageFlags.MapWriteBit);
			}

			void InitializeTransform() {
				GL.VertexArrayAttribBinding(_vertexArray,0,0);
				GL.EnableVertexArrayAttrib(_vertexArray,0);
				GL.VertexArrayAttribFormat(_vertexArray,0,4,VertexAttribType.Float,false,0);
			}

			void InitializeColor() {
				GL.VertexArrayAttribBinding(_vertexArray,1,0);
				GL.EnableVertexArrayAttrib(_vertexArray,1);
				GL.VertexArrayAttribFormat(_vertexArray,1,4,VertexAttribType.Float,false,16);
			}
			#endregion

		}
		
		public void Render() {
			GL.BindVertexArray(_vertexArray);
			GL.DrawArrays(PrimitiveType.Triangles,0,_verticeCount);
		}

		public virtual void Dispose() {
			GL.DeleteVertexArray(_vertexArray);
			GL.DeleteBuffer(_buffer);

			GC.SuppressFinalize(this);
		}

	}
}
