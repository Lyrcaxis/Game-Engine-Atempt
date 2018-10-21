using Game_Engine.Rendering;
using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine {
	public class ConsoleReader {

		public ConsoleReader() {
			Task.Run(ConsoleChecking);
		}


		async Task ConsoleChecking() {
			while (true) {
				var line = Console.ReadLine();
				Console.Clear();
				Console.WriteLine(RecognizeCommand(line));

				await Task.Delay(100);
			}
		}


		Color4[] colors = {
			Color4.Red,
			Color4.Beige,
			Color4.Gray,
			Color4.Gold,
			Color4.Fuchsia,
			Color4.LightCoral,
			Color4.MediumPurple,
			Color4.MediumSeaGreen,
			Color4.MediumSlateBlue,
			Color4.MediumOrchid,
			Color4.PapayaWhip,
			Color4.Peru,
			Color4.PowderBlue

		};

		List<string> GetWordsFromString(string str,bool PrintWordsInConsole=false) {

			List<string> words = new List<string>();
			string currentStr = str;

			bool shouldRemove() => !string.IsNullOrEmpty(currentStr) && currentStr[0].Equals(' ');
			void RemoveFirst() => currentStr = currentStr.Remove(0,1); 
			
			while (true) {

				while (shouldRemove()) { RemoveFirst(); }

				if (string.IsNullOrEmpty(currentStr)) { break; }
				else if (currentStr.Contains(' ')) {
					int index = currentStr.IndexOf(' ');
					string curWord = currentStr.Substring(0,index);
					words.Add(curWord);
					currentStr = currentStr.Remove(0,index);
				}
				else { words.Add(currentStr); break; }


			}

			if (PrintWordsInConsole)
				for (int i = 0; i < words.Count; i++) { Console.WriteLine("Word {0}: {1}",i,words[i]); }

			return words;
		}

		float mm => 1f / colors.Length;

		string RecognizeCommand(string command) {
			string f = command;
			var words = GetWordsFromString(command,true); 

			if (words[0].Equals("new")) {
				var id = new Random().Next(colors.Length);
				var newCube = ObjectFactory.CreateSolidCube(id * mm,colors[id]);
				float move = id * mm;
				Console.WriteLine("ID:" + id + ". Color: " + colors[id] + ". Move: " + move);
				Action ac = () => { RenderManager.AddNew(newCube,move); Console.WriteLine(move); };
				MainWindow.instance.AddAction(ac);
				f = "Creating new Cube";
			}
			else if (words[0].Equals("clear")) {
				MainWindow.instance.AddAction(() => RenderManager.ClearAll());
				f = "Clearing board";
			}
			else if (words[0].Equals("move")) {
				try {
					words[1] = words[1].Replace(".",",");
					words[2] = words[2].Replace(".",",");

					float x = float.Parse(words[1]);
					float y = float.Parse(words[2]);
					Action ac = () => RenderManager.renderObjects[0].Move(x,y);
					MainWindow.instance.AddAction(ac);

					f = BakeAnswer("Moving by: x: {0}, y: {1}", x, y);
				}
				catch { f = "Unrecognized Command"; }
			}

			else {
				f = "Unrecognized Command";
				
			}

			return f;
		}

		string BakeAnswer(string str,params object[] args) {
			for (int i = 0; i < args.Length; i++) {
				str = str.Replace("{" + i + "}",args[i].ToString());
			}
			return str;
		}

	}



}
