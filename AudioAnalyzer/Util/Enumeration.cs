using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AudioAnalyzer.Util {

	/// <summary>
	/// オブジェクトタイプのEnum列挙型を提供するための抽象クラス
	/// </summary>
	internal abstract class Enumeration : IComparable {

		/// <summary>
		/// EnumID
		/// </summary>
		public int Id { get; private set; }

		/// <summary>
		/// IDを指定してEnumインスタンスを初期化します。
		/// </summary>
		/// <param name="id"></param>
		protected Enumeration(int id) => this.Id = id;

		/// <summary>
		/// Idの相対値を計算します。
		/// </summary>
		/// <param name="other">相手のオブジェクト</param>
		/// <returns>相対値</returns>
		public int CompareTo(object other) => this.Id.CompareTo(((Enumeration)other).Id);

		/// <summary>
		/// Enumオブジェクトが等しいか検証します。
		/// </summary>
		/// <param name="obj">相手オブジェクト</param>
		/// <returns>等しい場合true</returns>
		public override bool Equals(object obj) {
			if (!(obj is Enumeration otherValue)) {
				return false;
			}

			bool typeMatches = this.GetType().Equals(obj.GetType());
			bool valueMatches = this.Id.Equals(otherValue.Id);

			return typeMatches && valueMatches;
		}

		/// <summary>
		/// インスタンスのハッシュ値を取得します
		/// </summary>
		/// <returns>ハッシュ値</returns>
		public override int GetHashCode() => 2108858624 + this.Id.GetHashCode();

		/// <summary>
		/// 定義されたすべてのEnumインスタンスを列挙します。
		/// </summary>
		/// <typeparam name="T">列挙するEnum型</typeparam>
		/// <returns>列挙されたEnumインスタンス</returns>
		public static IEnumerable<T> GetAll<T>() where T : Enumeration {
			FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
			return fields.Select(f => f.GetValue(null)).Cast<T>();
		}

	}

}
