using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicSet
{
	public class Set<T> : IEnumerable, IEnumerable<T>, IEquatable<Set<T>>, ISet<T> where T : class, IEquatable<T>
	{
		/// <summary>
		/// The array.
		/// </summary>
		private T[] array;
		/// <summary>
		/// The capacity.
		/// </summary>
		private int capacity = 8;
		/// <summary>
		/// The count.
		/// </summary>
		private int count;
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LogicSet.Set`1"/> class.
		/// </summary>
		public Set()
		{
			array = new T[capacity];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LogicSet.Set`1"/> class.
		/// </summary>
		/// <param name="capacity">Capacity.</param>
		public Set(int capacity)
		{
			if (capacity <= 0) throw new ArgumentException($"{nameof(capacity)} is invalid!");

			this.capacity = capacity;
			array = new T[capacity];
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LogicSet.Set`1"/> class.
		/// </summary>
		/// <param name="collection">Collection.</param>
		public Set(IEnumerable<T> collection) : this()
		{
			if (ReferenceEquals(collection, null)) throw new ArgumentNullException($"{nameof(collection)} is invalid!");

			foreach (var item in collection)
			{
				Add(item);
			}
		}
		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count => count;
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:LogicSet.Set`1"/> is empty.
		/// </summary>
		/// <value><c>true</c> if is empty; otherwise, <c>false</c>.</value>
		public bool IsEmpty => count == 0;
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:LogicSet.Set`1"/> is full.
		/// </summary>
		/// <value><c>true</c> if is full; otherwise, <c>false</c>.</value>
		private bool IsFull => count == capacity;
		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < count; i++)
			{
				yield return array[i];
			}
		}
		/// <summary>
		/// System.s the collections. IE numerable. get enumerator.
		/// </summary>
		/// <returns>The collections. IE numerable. get enumerator.</returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		/// <summary>
		/// Add the specified item.
		/// </summary>
		/// <returns>The add.</returns>
		/// <param name="item">Item.</param>
		public bool Add(T item)
		{
			if (ReferenceEquals(item, null)) throw new ArgumentNullException($"{nameof(item)} is null.");

			if (this.Contains(item))
				return false;

			if (IsFull) Expansion();

			array[count++] = item;
			return true;
		}
		/// <summary>
		/// System.s the collections. generic. IC ollection< t>. add.
		/// </summary>
		/// <param name="item">Item.</param>
		void ICollection<T>.Add(T item) => Add(item);
		/// <summary>
		/// Remove the specified item.
		/// </summary>
		/// <returns>The remove.</returns>
		/// <param name="item">Item.</param>
		public bool Remove(T item)
		{
			if (!Contains(item)) return false;

			int index = 0;
			for (int i = 0; i < Count; i++)
			{
				if (array[i].Equals(item)) index = i;
			}
			array[index] = array[Count - 1];
			array[Count - 1] = default(T);
			count--;
			return true;
		}
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:LogicSet.Set`1"/> is read only.
		/// </summary>
		/// <value><c>true</c> if is read only; otherwise, <c>false</c>.</value>
		public bool IsReadOnly => false;
		/// <summary>
		/// Contains the specified item.
		/// </summary>
		/// <returns>The contains.</returns>
		/// <param name="item">Item.</param>
		public bool Contains(T item)
		{
			for (int i = 0; i < count; i++)
			{
				if (array[i].Equals(item))
					return true;
			}
			return false;
		}
		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear()
		{
			this.count = 0;
			this.capacity = 8;
			this.array = new T[this.capacity];
		}
		/// <summary>
		/// Unions the with.
		/// </summary>
		/// <param name="other">Other.</param>
		public void UnionWith(IEnumerable<T> other)
		{
			if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

			foreach (var item in other)
			{
				Add(item);
			}
		}
		/// <summary>
		/// Intersects the with.
		/// </summary>
		/// <param name="other">Other.</param>
		public void IntersectWith(IEnumerable<T> other)
		{
			if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

			foreach (var item in other)
			{
				if (!Contains(item))
					Remove(item);
			}
		}
		/// <summary>
		/// Excepts the with.
		/// </summary>
		/// <param name="other">Other.</param>
		public void ExceptWith(IEnumerable<T> other)
		{
			if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

			foreach (var item in other)
			{
				Remove(item);
			}
		}
		/// <summary>
		/// Symmetrics the except with.
		/// </summary>
		/// <param name="other">Other.</param>
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

			foreach (var item in other)
			{
				if (Contains(item))
					Remove(item);
				else
					Add(item);
			}
		}
		/// <summary>
		/// Ises the subset of.
		/// </summary>
		/// <returns><c>true</c>, if subset of was ised, <c>false</c> otherwise.</returns>
		/// <param name="other">Other.</param>
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");
			if (this.Count > other.Count()) return false;

			for (int i = 0; i < Count; i++)
			{
				if (!other.Contains(array[i])) return false;
			}
			return true;
		}
		/// <summary>
		/// Ises the superset of.
		/// </summary>
		/// <returns><c>true</c>, if superset of was ised, <c>false</c> otherwise.</returns>
		/// <param name="other">Other.</param>
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");
			if (this.Count < other.Count()) return false;

			foreach (var item in other)
			{
				if (!Contains(item)) return false;
			}
			return true;
		}
		/// <summary>
		/// Ises the proper superset of.
		/// </summary>
		/// <returns><c>true</c>, if proper superset of was ised, <c>false</c> otherwise.</returns>
		/// <param name="other">Other.</param>
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

			return IsSupersetOf(other) && this.count != other.Count();
		}
		/// <summary>
		/// Ises the proper subset of.
		/// </summary>
		/// <returns><c>true</c>, if proper subset of was ised, <c>false</c> otherwise.</returns>
		/// <param name="other">Other.</param>
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

			return IsSubsetOf(other) && this.count != other.Count();
		}
		/// <summary>
		/// Overlaps the specified other.
		/// </summary>
		/// <returns>The overlaps.</returns>
		/// <param name="other">Other.</param>
		public bool Overlaps(IEnumerable<T> other)
		{
			if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

			foreach (var item in this)
			{
				if (other.Contains(item)) return true;
			}
			return false;
		}
		/// <summary>
		/// Sets the equals.
		/// </summary>
		/// <returns><c>true</c>, if equals was set, <c>false</c> otherwise.</returns>
		/// <param name="other">Other.</param>
		public bool SetEquals(IEnumerable<T> other)
		{
			if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null");

			if (ReferenceEquals(this, other)) return true;
			if (Count != other.Count()) return false;

			return IsSupersetOf(other) && IsSubsetOf(other);
		}
		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="array">Array.</param>
		/// <param name="arrayIndex">Array index.</param>
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (ReferenceEquals(array, null)) throw new ArgumentNullException($"{nameof(array)} is null.");
			if (arrayIndex < 0) throw new ArgumentException($"{nameof(arrayIndex)} is invalid!");
			for (int i = arrayIndex; i < array.Length; i++)
			{
				if (i - arrayIndex > Count) return;
				array[i] = this.array[i - arrayIndex];
			}
		}
		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:LogicSet.Set`1"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:LogicSet.Set`1"/>.</returns>
		public override string ToString()
		{
			if (Count == 0) return "Set is empty!";
			StringBuilder str = new StringBuilder();
			for (int i = 0; i < count; i++)
			{
				str.Append(array[i].ToString() + " ");
			}
			return str.ToString();
		}
		/// <summary>
		/// Determines whether the specified <see cref="LogicSet.Set<T>"/> is equal to the current <see cref="T:LogicSet.Set`1"/>.
		/// </summary>
		/// <param name="other">The <see cref="LogicSet.Set<T>"/> to compare with the current <see cref="T:LogicSet.Set`1"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="LogicSet.Set<T>"/> is equal to the current <see cref="T:LogicSet.Set`1"/>;
		/// otherwise, <c>false</c>.</returns>
		public bool Equals(Set<T> other)
		{
			if (ReferenceEquals(other, null)) throw new ArgumentNullException();

			if (Count != other.Count) return false;
			foreach (var item in other)
			{
				if (!Contains(item)) return false;
			}
			return true;
		}
		/// <summary>
		/// Expansion this instance.
		/// </summary>
		private void Expansion()
		{
			capacity *= 2;
			Array.Resize(ref array, capacity);
		}
	}
}