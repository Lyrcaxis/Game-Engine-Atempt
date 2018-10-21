using OpenTK;
using System;

namespace Game_Engine {
	class Program {
		[STAThread]
		static void Main() {
			new ConsoleReader();
			using (MainWindow game = new MainWindow()) {
				game.Run(60);
			}
		}
	}
}
