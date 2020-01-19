using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioAnalyzer.UI.Command;

namespace AudioAnalyzer.UI {
	internal class AudioAnalyzerModel : INotifyPropertyChanged {

		/// <summary>
		/// プロパティが変更されたとき、ViewModelに通知するイベント
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#region Property

		/// <summary>
		/// 開いてる音声ファイル。開いてない場合はnullです。
		/// </summary>
		public AudioFile AudioFile { get; set; } = null;

		/// <summary>
		/// UIが操作可能か否か
		/// </summary>
		public bool IsEnableUI { get; set; } = true;

		#endregion

		#region Command

		/// <summary>
		/// 開くボタンのコマンド
		/// </summary>
		public SelectAudioFile SelectAudioFile { get; private set; } = new SelectAudioFile();

		/// <summary>
		/// テクスチャを書き出すコマンド
		/// </summary>
		public ExportTexture ExportTexture { get; private set; } = new ExportTexture();

		/// <summary>
		/// テクスチャ書き出しの進行度
		/// </summary>
		public int Progress { get; set; } = 0;

		#endregion

		#region Function

		/// <summary>
		/// Modelを初期化します。
		/// </summary>
		public AudioAnalyzerModel() {

		}

		/// <summary>
		/// プロパティの変更をViewに通知
		/// </summary>
		/// <param name="propertyName">変更されたプロパティ名</param>
		public void OnPropertyChanded(string propertyName) {
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

			this.SelectAudioFile.OnCanExecuteChanged();
			this.ExportTexture.OnCanExecuteChanged();
			
		}

		#endregion
	}
}
