using System;
using System.Collections;
using System.Collections.Generic;

namespace Logging.Shared
{
	/// <summary>Represents a CircularBuffer to store items.</summary>
	/// <typeparam name="T">Type of data stored in the buffer.</typeparam>
	public class CircularBuffer<T> : IEnumerable<T>
	{
		private T[] _buffer;
		private int _nextFree;
		private int _count;

		/// <summary>Creates a new CircularBuffer with the specified size.</summary>
		/// <param name="size">The Max size of the buffer. Must be greater than 0.</param>
		public CircularBuffer(int size)
		{
			if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));
			Size = size;
			_buffer = new T[size];
		}

		/// <summary>The max size of the buffer.</summary>
		public int Size { get; }

		/// <summary>The current number of items in the buffer.</summary>
		public int Count
		{
			get { return _count; }
			private set { _count = value <= Size ? value : Size; }
		}

		/// <summary>Adds a new item to the buffer.</summary>
		/// <param name="item">The item to add to the buffer.</param>
		public void Add(T item)
		{
			_buffer[_nextFree] = item;
			_nextFree = (_nextFree + 1) % Size;
			Count++;
		}

		/// <summary>Clears and resets the buffer.</summary>
		public void Clear()
		{
			_buffer = new T[Size];
			_nextFree = 0;
			Count = 0;
		}

		/// <summary>Enumerates the items in the buffer.</summary>
		public IEnumerator<T> GetEnumerator()
		{
			var start = Count < Size ? 0 : _nextFree;
			for (int i = 0; i < Count; i++)
			{
				var index = (start + i) % Size;
				yield return _buffer[index];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}