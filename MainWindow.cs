using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Game_Engine {


	class MainWindow:GameWindow {

		public static MainWindow instance;

		public MainWindow() : base(1280,720,GraphicsMode.Default,"Super Awesome New Window") { 
			instance = this;
			this.Run(60);
		}

		protected override void OnLoad(EventArgs e) {

			ShaderUtility.Initialize();
			Rendering.RenderManager.Initialize();

			GL.PolygonMode(MaterialFace.FrontAndBack,PolygonMode.Fill);
			GL.PatchParameter(PatchParameterInt.PatchVertices,3);
		}

		protected override void OnResize(EventArgs e) {
			GL.Viewport(0,0,Width,Height);
			//GL.MatrixMode(MatrixMode.Projection);
			//GL.LoadIdentity();
			//GL.Ortho(0,50,0,50,-1,1);
			//GL.MatrixMode(MatrixMode.Modelview);

		}

		public override void Exit() {
			ShaderUtility.Exit();
			Rendering.RenderManager.Exit();
			base.Exit();
		}

		protected override void OnUpdateFrame(FrameEventArgs e) {
			//var ms = Mouse.GetState();
			//if (ms.IsButtonDown(MouseButton.Left)) { VSync = VSyncMode.On; Console.WriteLine("Button Down"); }
			//else if (ms.IsButtonDown(MouseButton.Right)) { VSync = VSyncMode.Off; Console.WriteLine("Right click"); }

			if (Keyboard.GetState().IsKeyDown(Key.Escape)) { Exit(); }

		}

		OpenTK.Graphics.Color4 color = Color4.AliceBlue;
		double time;

		protected override void OnRenderFrame(FrameEventArgs e) {
			time += e.Time;
			Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";
			GL.ClearColor(color);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			GL.UseProgram(ShaderUtility._Shader);
			Rendering.RenderManager.RenderAll();
			SwapBuffers();
		}
		
		//float currentZoom = 50;
		//float scrollWheelValue;
		void Old() {
			/*
			color.R = Math.Abs((float)Math.Sin(time));
			color.G = Math.Abs((float)Math.Cos(time));
			color.B = Math.Abs((float)(Math.Sin(time) + Math.Cos(time)) / 2);
			color.A = 1;
			Vector4 position;
			position.X = (float)Mouse.GetState().X / Width;
			position.Y = -(float)Mouse.GetState().Y / Height;
			position.Z = 0.0f;
			position.W = 1.0f;



			
			float scrollDif = Mouse.GetState().ScrollWheelValue - scrollWheelValue;
			currentZoom += Mouse.GetState().ScrollWheelValue;
			//currentZoom += scrollDif;
			scrollWheelValue = Mouse.GetState().ScrollWheelValue;
			*/
			//GL.VertexAttrib4(1,position);
			//GL.VertexAttrib1(0,time);

			//GL.DrawArrays(PrimitiveType.Points,0,1);
			//GL.PointSize(currentZoom);
		}

	}

}
