using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CircularBuffer
{
	class Program
	{
	    static CircularBuffer<int> queue;

		static void Main(string[] args)
		{
			Console.Write("Enter buffer size:");
			int bufferSize = int.Parse(Console.ReadLine());


			queue = new CircularBuffer<int>(bufferSize);

			queue.Enqueue(1);
			queue
				.Enqueue(2);
			queue.Enqueue(3);
			queue.Enqueue(4);


			queue.Dequeue();


			queue.Enqueue(7);
			queue.Enqueue(8);


			queue.Enqueue(9);
			queue.Enqueue(10);

			ReadElementsFromQueue();
		
			Console.ReadKey();
		}

		public static void AddElementsToQueue()
		{

			int number;
			Random rand = new Random();
			//add five numbers less then 100 to buffer
			while (!queue.IsFull)
			{
				number = rand.Next(10, 30);
				queue.Enqueue(number);
				Console.WriteLine("Enqueued element {0}", number);
			}

			//add empty row to console
			Console.WriteLine();

		}

		public static void ReadElementsFromQueue()
		{

			for(int i=0; i < queue._bufferSize; i++)
			{
				Console.Write("{0} ", queue.ReturnFromIndex(i));
				if(i== queue._bufferSize-1)
				{
					Console.WriteLine();
				}
			}


			////retrieve buffer data
			//while (!queue.IsEmpty)
			//{
			//	Console.Write("{0}", queue.Dequeue());
			//}
		}

	
	
	}
}
