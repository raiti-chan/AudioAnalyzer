using Microsoft.Win32;
using System;
using System.Windows.Input;

namespace AudioAnalyzer.UI.Command {
	/// <summary>
	/// 音声ファイルを選択します。
	/// </summary>
	internal class SelectAudioFileCommand : ICommand {

		/// <summary>
		/// コマンドの有効無効を通知するイベント。
		/// </summary>
		public event EventHandler CanExecuteChanged;

		/// <summary>
		/// コマンドの有効無効の変更をViewに通知します。
		/// </summary>
		public void OnCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		
		/// <summary>
		/// ファイルを開くボタンのコマンド
		/// </summary>
		/// <param name="model">モデル</param>
		public SelectAudioFileCommand() {

		}

		/// <summary>
		/// コマンドを実行できるかを返します
		/// </summary>
		/// <param name="parameter">コマンドで使用されたデータ</param>
		/// <returns></returns>
		public bool CanExecute(object parameter) => true;

		/// <summary>
		/// コマンドを実行します。
		/// </summary>
		/// <param name="parameter"></param>
		public void Execute(object parameter) {
			AudioAnalyzerModel model = (AudioAnalyzerModel)parameter;
			OpenFileDialog dialog = new OpenFileDialog {
				Filter = "音声ファイル(*.wave;*.mp3)|*wave;*.mp3"
			};

			if (dialog.ShowDialog() == true) {
				model.AudioFile = new AudioFile(dialog.FileName);
				model.OnPropertyChanded(null);
			}
		}
	}
}
