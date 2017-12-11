using System;
using System.Linq;
using Logging.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Logging.Tests.Shared
{
	[TestClass]
	public class CircularBufferTest
	{
		[TestMethod]
		public void NewCircularBufferIsEmpty()
		{
			var circularBuffer = new CircularBuffer<int>(5);

			Assert.AreEqual(5, circularBuffer.Size);
			Assert.AreEqual(0, circularBuffer.Count);
			Assert.IsFalse(circularBuffer.Any());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void InvalidSizeThrowsOutOfRange()
		{
			var circularBuffer = new CircularBuffer<string>(-5);
		}

		[TestMethod]
		public void AddAddsAnItem()
		{
			var circularBuffer = new CircularBuffer<int>(5);
			circularBuffer.Add(42);

			Assert.AreEqual(1, circularBuffer.Count);
			// Testing Enumerator
			Assert.AreEqual(1, circularBuffer.Count());
			Assert.AreEqual(42, circularBuffer.First());
		}

		[TestMethod]
		public void OverFlowCircularBuffer()
		{
			var circularBuffer = new CircularBuffer<int>(5);
			for (int i = 0; i < 14; i++)
				circularBuffer.Add(i);

			Assert.AreEqual(5, circularBuffer.Size);
			Assert.AreEqual(5, circularBuffer.Count);
			Assert.AreEqual(5, circularBuffer.Count());
			var value = 9;
			foreach (var item in circularBuffer)
			{
				Assert.AreEqual(value, item);
				value++;
			}
		}

		[TestMethod]
		public void ClearResetsBuffer()
		{
			var circularBuffer = new CircularBuffer<int>(5);
			for (int i = 0; i < 14; i++)
				circularBuffer.Add(i);

			circularBuffer.Clear();

			var privateCircularBuffer = new PrivateObject(circularBuffer);
			Assert.AreEqual(0, privateCircularBuffer.GetField("_nextFree"));
			

			Assert.AreEqual(5, circularBuffer.Size);
			Assert.AreEqual(0, circularBuffer.Count);
			Assert.IsFalse(circularBuffer.Any());
		}
	}
}