using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Game_Engine {
	public class ShaderUtility {

		public static int _Shader;
		public static int _VertexArray;

		public static void Initialize() {
			_Shader = CreateProgram();
			GL.GenVertexArrays(1,out _VertexArray);
			GL.BindVertexArray(_VertexArray);
		}

		public static void Exit() {
			GL.DeleteVertexArrays(1,ref _VertexArray);
			GL.DeleteProgram(_Shader);
		}

		static int CompileShader(ShaderType type,string path) {
			var shader = GL.CreateShader(type);
			var src = File.ReadAllText(path);
			GL.ShaderSource(shader,src);
			GL.CompileShader(shader);
			return shader;
		}


		static int CreateProgram() {
			var program = GL.CreateProgram();
			var shaders = new List<int>();
			shaders.Add(CompileShader(ShaderType.VertexShader,@"Components\Shaders\vertexShader.vert"));
			shaders.Add(CompileShader(ShaderType.FragmentShader,@"Components\Shaders\fragmentShader.frag"));

			foreach (var shader in shaders) { GL.AttachShader(program,shader); }

			var info = GL.GetProgramInfoLog(program);
			if (!string.IsNullOrWhiteSpace(info)) { Console.WriteLine($"GL.LinkProgram had info log: {info}"); }

			GL.LinkProgram(program);

			foreach (var shader in shaders) {
				GL.DetachShader(program,shader);
				GL.DeleteShader(shader);
			}
			return program;
		}


	}
}
