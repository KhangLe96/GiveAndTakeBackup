﻿using System;

namespace Giveaway.API.Shared.Exceptions
{
	public class ConflictException : Exception
	{
		public ConflictException(string message) : base(message)
		{
		}
	}
}
