using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CircularBuffer
{
	[TestFixture]
	class CircularBufferTest
	{
		[Test]
		public void TestBufferContent()
		{
			var buffer = new CircularBuffer<long>(3);
			buffer.Enqueue(2);
			buffer.Enqueue(3);
			buffer.Enqueue(4);
			Assert.AreEqual(2, buffer.Dequeue());
			Assert.AreEqual(3, buffer.Dequeue());
			Assert.AreEqual(4, buffer.Dequeue());
		}

		[Test]
		public void TestBufferWhenFull()
		{
			var buffer = new CircularBuffer<long>(5);
			buffer.Enqueue(1);
			buffer.Enqueue(2);
			buffer.Enqueue(3);
			buffer.Enqueue(4);
			buffer.Enqueue(5);
			Assert.AreEqual(true, buffer.IsFull);
		}

		[Test]
		public void TestBufferWhenEmpty()
		{
			var buffer = new CircularBuffer<long>(5);
			buffer.Enqueue(1);
			buffer.Enqueue(2);
			buffer.Enqueue(3);
			buffer.Enqueue(4);
			buffer.Enqueue(5);

			buffer.Dequeue();
			buffer.Dequeue();
			buffer.Dequeue();
			buffer.Dequeue();
			buffer.Dequeue();
			Assert.AreEqual(true, buffer.IsEmpty);
		}

		[Test]
		public void TestIncreaseCapacityWhenFull()
		{
			var buffer = new CircularBuffer<long>(5);
			buffer.Enqueue(1);
			buffer.Enqueue(2);
			buffer.Enqueue(3);
			buffer.Enqueue(4);
			buffer.Enqueue(5);

			buffer.Enqueue(6);
			Assert.AreEqual(1, buffer.Dequeue());
		}

		[Test]
		public void TestDecreaseCapacityWhenFull()
		{
			var buffer = new CircularBuffer<long>(5);
			buffer.Enqueue(1);
			buffer.Enqueue(2);
			buffer.Enqueue(3);
			buffer.Enqueue(4);
			buffer.Enqueue(5);

			buffer.Dequeue();
			buffer.Dequeue();
			Assert.AreEqual(3, buffer.Dequeue());
		}


		[Test]
		public void TestOrderOfElementsWhenAddToFull()
		{
			var buffer = new CircularBuffer<long>(6);
			buffer.Enqueue(1);
			buffer.Enqueue(2);
			buffer.Enqueue(3);
			buffer.Enqueue(4);
			buffer.Enqueue(5);
			buffer.Enqueue(6);

			buffer.Dequeue();
			buffer.Dequeue();

			buffer.Enqueue(7);
			buffer.Enqueue(8);
			buffer.Dequeue();

			buffer.Enqueue(9);
			buffer.Enqueue(10);

			Assert.AreEqual(4, buffer.Dequeue());
			Assert.AreEqual(5, buffer.Dequeue());
			Assert.AreEqual(6, buffer.Dequeue());
		}

		[Test]
		public void TestBufferOverloading()
		{
			var buffer = new CircularBuffer<long>(4);
			buffer.Enqueue(1);
			buffer.Enqueue(2);
			buffer.Enqueue(3);
			buffer.Enqueue(4);

			//add two elements to full buffer
			buffer.Enqueue(5);
			buffer.Enqueue(6);

			Assert.AreEqual(1, buffer.Dequeue());
			Assert.AreEqual(2, buffer.Dequeue());
			Assert.AreEqual(3, buffer.Dequeue());
			Assert.AreEqual(4, buffer.Dequeue());
		}


		[Test]
		public void TestElementsOrderAfterReadingFullBuffer()
		{
			var buffer = new CircularBuffer<long>(4);
			buffer.Enqueue(1);
			buffer.Enqueue(2);
			buffer.Enqueue(3);
			buffer.Enqueue(4);

			buffer.Dequeue();
			buffer.Dequeue();
			buffer.Dequeue();

			//add two elements to full buffer
			buffer.Enqueue(5);
			buffer.Enqueue(6);

			Assert.AreEqual(4, buffer.Dequeue());
			Assert.AreEqual(5, buffer.Dequeue());
		}


		[Test]
		public void TestBufferWithVariousCombinations()
		{
			var buffer = new CircularBuffer<long>(2);
			buffer.Enqueue(1);
			buffer.Enqueue(2);
		    Assert.AreEqual(1,buffer.Dequeue());

			buffer.Enqueue(3);
			buffer.Enqueue(4);
			buffer.Enqueue(5);

			Assert.AreEqual(2, buffer.Dequeue());
			Assert.AreEqual(3, buffer.Dequeue());
			Assert.AreEqual(4, buffer.Dequeue());
			Assert.AreEqual(5, buffer.Dequeue());

		}

	}
}
