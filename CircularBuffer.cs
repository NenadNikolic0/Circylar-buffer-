using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularBuffer
{
	class CircularBuffer<T>
	{
		T[] _buffer;
		int _head;
		int _tail;
		int _length;
		public int _bufferSize;


		object _lock = new object();

		public CircularBuffer(int bufferSize)
		{

			_buffer = new T[bufferSize];
			_bufferSize = bufferSize;
			_head = bufferSize - 1;
		}

		//check if buffer is empty
		public bool IsEmpty
		{
			get { return _length == 0; }
		}

		//check if buffer is full
		public bool IsFull
		{
			get { return _length == _bufferSize; }
		}

		//remove element from tail
		public T Dequeue()
		{
			lock (_lock)
			{
				if (IsEmpty)
				{
					throw new InvalidOperationException("Queue exhausted");
				}

				T dequeued = _buffer[_tail];
				_tail = NextPosition(_tail);
				_length--;
				return dequeued;
			}
		}



		//remove element from tail
		public T ReturnFromIndex(int index)
		{
			T element = _buffer[index];
			return element;
		}




		//calculate head position
		private int NextPosition(int position)
		{
			return (position + 1) % _bufferSize;
		}

		//add element to buffer
		public void Enqueue(T toAdd)
		{
			lock (_lock)
			{
				if (IsFull)
				{
					

					//create temporary array to hold elements
					T[] copyArray = new T[_bufferSize];

					//Create helper array 
					int j = 0;
					for (int i = _tail; i < _bufferSize; i++)
					{
						copyArray[j] = _buffer[i];
						j++;

					}

					for (int i = 0; i < _tail; i++)
					{
						copyArray[j] = _buffer[i];
						j++;
					}


					//if buffer is full, increase capacity to double
					_bufferSize = _bufferSize * 2;


					//instance double size buffer, add previous elements and new
					_buffer = new T[_bufferSize];

					for (int i = 0; i < copyArray.Length; i++)
					{
						_buffer[i] = copyArray[i];
					}

					_head = _length;
					_tail = 0;
					_length++;
				}

				else
				{
					_length++;
					_head = NextPosition(_head);
				}

				_buffer[_head] = toAdd;

			}
		}


	}

}
