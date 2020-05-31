﻿using System;
using Montr.Core.Models;

namespace Montr.Automate.Models
{
	public class AutomationSearchRequest : SearchRequest
	{
		public string EntityTypeCode { get; set; }

		public Guid EntityTypeUid { get; set; }

		public bool? IsActive { get; set; }
	}
}
