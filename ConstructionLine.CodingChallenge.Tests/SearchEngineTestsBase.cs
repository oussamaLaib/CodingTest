﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class SearchEngineTestsBase
    {
        protected static void AssertResults(List<Shirt> shirts, SearchOptions options)
        {
            Assert.That(shirts, Is.Not.Null);

            var resultingShirtIds = shirts.Select(s => s.Id).ToList();
            var sizeIds = options.Sizes.Select(s => s.Id).ToList();
            var colorIds = options.Colors.Select(c => c.Id).ToList();

            foreach (var shirt in shirts)
            {
                if (sizeIds.Contains(shirt.Size.Id)
                    && colorIds.Contains(shirt.Color.Id)
                    && !resultingShirtIds.Contains(shirt.Id))
                {
                    Assert.Fail($"'{shirt.Name}' with Size '{shirt.Size.Name}' and Color '{shirt.Color.Name}' not found in results, " +
                                $"when selected sizes where '{string.Join(",", options.Sizes.Select(s => s.Name))}' " +
                                $"and colors '{string.Join(",", options.Colors.Select(c => c.Name))}'");
                }
            }
        }


        protected static void AssertSizeCounts(List<Shirt> shirts, SearchOptions searchOptions, List<SizeCount> sizeCounts)
        {
            Assert.That(sizeCounts, Is.Not.Null);

            foreach (var size in Size.All)
            {
                var sizeCount = sizeCounts.SingleOrDefault(s => s.Size.Id == size.Id);
                Assert.That(sizeCount, Is.Not.Null, $"Size count for '{size.Name}' not found in results");

                int expectedSizeCount = 0;

                if (searchOptions.Colors.Count == 0)
                {
                    expectedSizeCount = shirts.Count(sh => (sh.Size.Id == size.Id) && searchOptions.Sizes.Exists(optc => optc.Id == size.Id));
                }
                else
                {
                    if (searchOptions.Sizes.Count != 0)
                    {

                        foreach (var color in searchOptions.Colors)
                        {
                            expectedSizeCount += shirts.Count(sh => (sh.Size.Name.Equals(size.Name)) && (sh.Color.Id == color.Id)
                                                 && searchOptions.Sizes.Exists(opts => opts.Name.Equals(size.Name)));
                        }
                    }
                    else
                    {
                        foreach (var color in searchOptions.Colors)
                        {
                            expectedSizeCount += shirts.Count(sh => (sh.Size.Name.Equals(size.Name)) && (sh.Color.Id == color.Id));
                        }
                    }
                }

                Assert.That(sizeCount.Count, Is.EqualTo(expectedSizeCount), 
                    $"Size count for '{sizeCount.Size.Name}' showing '{sizeCount.Count}' should be '{expectedSizeCount}'");
            }
        }


        protected static void AssertColorCounts(List<Shirt> shirts, SearchOptions searchOptions, List<ColorCount> colorCounts)
        {
            Assert.That(colorCounts, Is.Not.Null);
            
            foreach (var color in Color.All)
            {
                var colorCount = colorCounts.SingleOrDefault(s => s.Color.Id == color.Id);
                Assert.That(colorCount, Is.Not.Null, $"Color count for '{color.Name}' not found in results");
                var expectedColorCount = 0;
                if (searchOptions.Sizes.Count == 0)
                {
                    expectedColorCount += shirts.Count(c => (c.Color.Id == color.Id) && searchOptions.Colors.Exists(optc => optc.Id == color.Id));
                }
                else
                {
                    if (searchOptions.Colors.Count != 0)
                    {
                        foreach (var size in searchOptions.Sizes)
                        {
                            expectedColorCount += shirts.Count(sh => (sh.Color.Name.Equals(color.Name)) && (sh.Size.Id == size.Id) && searchOptions.Colors.Exists(optc => optc.Id == color.Id));
                        }
                    }
                    else
                    {
                        foreach (var size in searchOptions.Sizes)
                        {
                            expectedColorCount += shirts.Count(sh => (sh.Color.Name.Equals(color.Name)) && (sh.Size.Id == size.Id));
                        }
                    }
                }
                Assert.That(colorCount.Count, Is.EqualTo(expectedColorCount),
                    $"Color count for '{colorCount.Color.Name}' showing '{colorCount.Count}' should be '{expectedColorCount}'");
            }
        }
    }
}