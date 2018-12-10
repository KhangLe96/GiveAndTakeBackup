﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GiveAndTake.Core.Models
{
	[DataContract]
	public class PostResponse
	{
		[DataMember(Name = "id")]
		public Guid Id { get; set; }

		[DataMember(Name = "image")]
		public string Image { get; set; }
	}
}