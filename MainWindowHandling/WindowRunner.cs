using OpenTK;
using OpenTK.Graphics.OpenGL4;


namespace Game_Engine.MainWindowHandling {
	public class WindowRunner {

		public static GameWindow Window;

		public WindowRunner(GameWindow win) {
			Window = win;
			Window.Load += WindowSetup;
			Window.Run(1 / 60f,60f);
		}

		void WindowSetup(object sender,System.EventArgs e) {
			Window.VSync = VSyncMode.On;

			Window.RenderFrame += WindowRenderFrame;
			Window.Resize += WindowResize;


			System.Console.WriteLine("Loaded");
		}

		OpenTK.Graphics.Color4 color;
		void WindowResize(object sender,System.EventArgs e) {
			//GL.Viewport(0,0,Window.Width,Window.Height);
			//GL.MatrixMode(MatrixMode.Projection);
			//GL.LoadIdentity();
			//GL.Ortho(0,50,0,50,-1,1);
			//GL.MatrixMode(MatrixMode.Modelview);
		}


		float time;
		bool running;

		async void WindowRenderFrame(object sender,FrameEventArgs e) {
			if (running) { return; }
			running = true;

			time += 1 / 60f;
			if (time>1) { time -= 1; }

			color.R = 1 - time;
			color.G = time;
			color.B = System.Math.Max(time,1 - time);

			color.A = 1;
			//GL.Begin(PrimitiveType.Points);
			GL.ClearColor(color);
			//GL.Color4(color);
			//GL.End();
			var ms = OpenTK.Input.Mouse.GetState();
			Vector2 mouse = new Vector2(ms.X,ms.Y);
			await System.Threading.Tasks.Task.Delay(1000);
			System.Console.WriteLine("Framing " + mouse);
			running = false;
		}
	}
}
