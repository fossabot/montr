﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Montr.MasterData.Impl.Services;

namespace Montr.MasterData.Tests.Services
{
	[TestClass]
	public class NumeratorPatternParserTests
	{
		[TestMethod]
		[DataRow("P-{Number}", "{Number}")]
		[DataRow("P-{Number}-{Year2}", "{Number},{Year2}")]
		[DataRow("P-{Number}/{Year4}", "{Number},{Year4}")]
		[DataRow("{Company}-{Number}/{Year4}", "{Company},{Number},{Year4}")]
		public void Parse_ValidPatterns_ReturnTags(string pattern, string tags)
		{
			// arrange
			var parser = new NumeratorPatternParser();
			var expected = tags.Split(",");

			// act
			var result = parser.Parse(pattern);

			// assert
			Assert.IsTrue(result.Success);
			CollectionAssert.AreEqual(expected, result.Tags);
		}
	}
}
