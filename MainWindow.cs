using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Game_Engine {


	class MainWindow:GameWindow {

		public static MainWindow instance;

		public MainWindow() : base(1280,720,GraphicsMode.Default,"Super Awesome New Window"
		,GameWindowFlags.Default,DisplayDevice.Default,4,5,GraphicsContextFlags.ForwardCompatible) { 
			instance = this;
		}

		protected override void OnLoad(EventArgs e) {

			ShaderUtility.Initialize();
			Rendering.RenderManager.Initialize();

			GL.PolygonMode(MaterialFace.FrontAndBack,PolygonMode.Line);
			GL.PatchParameter(PatchParameterInt.PatchVertices,3);
			GL.Enable(EnableCap.DepthTest);
			Console.WriteLine(GL.GetString(StringName.Version));
		}

		protected override void OnResize(EventArgs e) {
			GL.Viewport(0,0,Width,Height);


			return;

		}

		List<Action> ActionQueue = new List<Action>();
		public void AddAction(Action ac) => ActionQueue.Add(ac);

		public override void Exit() {
			ShaderUtility.Exit();
			Rendering.RenderManager.Exit();
			base.Exit();
		}

		Matrix4 _modelView;
		protected override void OnUpdateFrame(FrameEventArgs e) {
			//var ms = Mouse.GetState();
			//if (ms.IsButtonDown(MouseButton.Left)) { VSync = VSyncMode.On; Console.WriteLine("Button Down"); }
			//else if (ms.IsButtonDown(MouseButton.Right)) { VSync = VSyncMode.Off; Console.WriteLine("Right click"); }

			time += e.Time;
			var k = (float)time * 0.05f;
			var r1 = Matrix4.CreateRotationX(k * 13.0f);
			var r2 = Matrix4.CreateRotationY(k * 13.0f);
			var r3 = Matrix4.CreateRotationZ(k * 3.0f);
			_modelView = r1 * r2 * r3;

			float x1 = (float)(Math.Sin(k * 5f) * 0.5f);
			float x2 = (float)(Math.Cos(k * 5f) * 0.5f);
			var t1 = Matrix4.CreateTranslation(x1,x2,0);
			_modelView = r1 * r2 * r3 * t1;

			if (Keyboard.GetState().IsKeyDown(Key.Escape)) { Exit(); }
			for (int i = ActionQueue.Count - 1; i >= 0; i--) {
				ActionQueue[i].Invoke();
				ActionQueue.RemoveAt(i);
			}
		}

		OpenTK.Graphics.Color4 color = Color4.AliceBlue;
		double time;

		protected override void OnRenderFrame(FrameEventArgs e) {
			Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";
			GL.ClearColor(Color4.BlueViolet);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			GL.UseProgram(ShaderUtility._Shader);
			GL.UniformMatrix4(20,false,ref _modelView);
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
