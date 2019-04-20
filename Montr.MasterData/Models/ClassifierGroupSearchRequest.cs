﻿using System;
using Montr.Core.Models;

namespace Montr.MasterData.Models
{
	public class ClassifierGroupSearchRequest : SearchRequest
	{
		public Guid CompanyUid { get; set; }

		public string TypeCode { get; set; }

		public string TreeCode { get; set; }

		public string ParentCode { get; set; }

		public Guid? FocusUid { get; set; }
	}
}
