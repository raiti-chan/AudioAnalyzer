﻿using System;
using System.Windows;
using System.Windows.Input;

namespace AudioAnalyzer.UI.Command {
	class ExportTextureCommand : ICommand{

		/// <summary>
		/// コマンドの有効無効を通知するイベント。
		/// </summary>
		public event EventHandler CanExecuteChanged;

		/// <summary>
		/// コマンドの有効無効の変更をViewに通知します。
		/// </summary>
		public void OnCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);

		/// <summary>
		/// テクスチャ書き出しコマンド
		/// </summary>
		public ExportTextureCommand() {

		}

		/// <summary>
		/// コマンドを実行できるかを返します。
		/// </summary>
		/// <param name="parameter">実行可能チェックで渡される引数</param>
		/// <returns>実行可能な場合true</returns>
		public bool CanExecute(object parameter) {
			if (parameter == null) {
				return false;
			}
			AudioAnalyzerModel model = (AudioAnalyzerModel)parameter;
			return model.AudioFile != null;
		}

		/// <summary>
		/// コマンドを実行します。
		/// </summary>
		/// <param name="paramerter">コマンドに渡されるパラメータ</param>
		public async void Execute(object paramerter) {
			AudioAnalyzerModel model = (AudioAnalyzerModel)paramerter;
			model.IsEnableUI = false; // UIを操作不能に
			model.OnPropertyChanded(nameof(model.IsEnableUI));

			Progress<int> progress = new Progress<int>(percent => {
				model.Progress = percent;
				model.OnPropertyChanded(nameof(model.Progress));
			});
			AudioTexture texture = new AudioTexture(8192, 128);
			
			await model.AudioFile.ExportTextureAsync(progress, texture);

			
			texture.SaveTexture("./test.png");
			MessageBox.Show("テクスチャの書き出しが完了しました。", "", MessageBoxButton.OK, MessageBoxImage.Information);
			
			model.AudioTexture = texture;
			model.AudioTexSource = texture.CreateBitmapSource();
			model.IsEnableUI = true;
			model.IsEnablePreview = true;
			model.Progress = 0;
			model.OnPropertyChanded(null);
		}


 }
}
