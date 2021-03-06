﻿using System;
using Montr.Core.Models;

namespace Montr.MasterData.Models
{
	public class ClassifierTreeSearchRequest : SearchRequest
	{
		public Guid CompanyUid { get; set; }

		public Guid UserUid { get; set; }

		public string TypeCode { get; set; }

		public string Code { get; set; }

		public Guid? Uid { get; set; }
	}
}
