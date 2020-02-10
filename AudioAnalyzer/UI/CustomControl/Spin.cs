using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AudioAnalyzer.UI.CustomControl {
	/// <summary>
	/// Spinを実装します。
	/// 
	/// </summary>
	public class Spin : Control {

		/// <summary>
		/// デフォルトのスタイルの上書き
		/// </summary>
		static Spin() => DefaultStyleKeyProperty.OverrideMetadata(typeof(Spin), new FrameworkPropertyMetadata(typeof(Spin)));


		#region Property

		/// <summary>
		/// 値の依存プロパティ
		/// </summary>
		private static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int),
			typeof(Spin), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		/// <summary>
		/// 値
		/// </summary>
		public int Value {
			get => (int)this.GetValue(ValueProperty);
			set => this.SetValue(ValueProperty, value);
		}

		/// <summary>
		/// 値の最大値依存プロパティ
		/// </summary>
		private static readonly DependencyProperty MaximamProperty = DependencyProperty.Register("Maximam", typeof(int?),
			typeof(Spin), new FrameworkPropertyMetadata(null));

		/// <summary>
		/// 値の最大値
		/// </summary>
		public int? Maximam {
			get => (int?)this.GetValue(MaximamProperty);
			set => this.SetValue(MaximamProperty, value);
		}

		/// <summary>
		/// 値の最小値依存プロパティ
		/// </summary>
		private static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimam", typeof(int?),
			typeof(Spin), new FrameworkPropertyMetadata(null));

		/// <summary>
		/// 値の最小値
		/// </summary>
		public int? Minimum {
			get => (int?)this.GetValue(MinimumProperty);
			set => this.SetValue(MinimumProperty, value);
		}

		#endregion

		/// <summary>
		/// カウントアップボタン
		/// </summary>
		private ButtonBase upButton = null;

		/// <summary>
		/// カウントダウンボタン
		/// </summary>
		private ButtonBase downButton = null;

		/// <summary>
		/// テンプレートが適用されたときの処理の上書き
		/// </summary>
		public override void OnApplyTemplate() {
			base.OnApplyTemplate();

			if (this.upButton != null) {
				this.upButton.Click -= this.UpClick;
			}

			if (this.downButton != null) {
				this.downButton.Click -= this.DownClick;
			}

			this.upButton = this.GetTemplateChild("PART_UpButton") as ButtonBase;
			this.downButton = this.GetTemplateChild("PART_DownButton") as ButtonBase;

			if (this.upButton != null) {
				this.upButton.Click += this.UpClick;
			}

			if (this.downButton != null) {
				this.downButton.Click += this.DownClick;
			}

		}

		/// <summary>
		/// Upボタンが押された処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UpClick(object sender, RoutedEventArgs e) {
			if (this.Maximam.HasValue) {
				this.Value = Math.Min(this.Value + 1, this.Maximam.Value);
			} else {
				this.Value++; 
			}

			
		}

		/// <summary>
		/// Downボタンが押された処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DownClick(object sender, RoutedEventArgs e) {
			if (this.Minimum.HasValue) {
				this.Value = Math.Max(this.Value - 1, this.Minimum.Value);
			} else {
				this.Value--;
			}
		}
	}
}
