using OpenTK;
using System;

namespace Game_Engine {
	class Program {
		[STAThread]
		static void Main() {
			// The 'using' idiom guarantees proper resource cleanup.
			// We request 30 UpdateFrame events per second, and unlimited
			// RenderFrame events (as fast as the computer can handle).
			using (MainWindow game = new MainWindow()) {
				game.Run(30.0);
			}
		}

	}
}
