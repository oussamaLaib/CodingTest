using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        List<Shirt> ShirtsFound = new List<Shirt> { };
        List<SizeCount> SzCounts = new List<SizeCount> { };
        List<ColorCount> ClCounts = new List<ColorCount> { };

        Dictionary<string, int> ColorsIndex = new Dictionary<string, int>();
        Dictionary<string, int> SizesIndex = new Dictionary<string, int>();

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.


            foreach (var color in Color.All)
            {
                ClCounts.Add(new ColorCount() { Color = color, Count = 0 });
            }

            foreach (var size in Size.All)
            {
                SzCounts.Add(new SizeCount() { Size = size, Count = 0 });
            }



            // This initialization code for when using the second search method
            //
            //int indx = 0;
            //foreach (var color in Color.All)
            //{
            //    ClCounts.Add(new ColorCount() { Color = color, Count = 0 });
            //    ColorsIndex.Add(color.Name, indx);
            //    indx++;
            //}

            //indx = 0;
            //foreach (var size in Size.All)
            //{
            //    SzCounts.Add(new SizeCount() { Size = size, Count = 0 });
            //    SizesIndex.Add(size.Name, indx);
            //    indx++;
            //}

        }



            public SearchResults Search(SearchOptions options)
        {
            // TODO: search logic goes here.
            

            if (options.Sizes.Count != 0)
            {
                foreach (var size in options.Sizes)
                {
                    if (options.Colors.Count != 0)
                    {
                        foreach (var color in options.Colors)
                        {
                            foreach (var shirt in _shirts.FindAll(s => s.Size.Name.Equals(size.Name) && s.Color.Name.Equals(color.Name)))
                            {
                                ShirtsFound.Add(shirt);

                                SzCounts.Find(s => s.Size.Name.Equals(size.Name)).Count++;
                                ClCounts.Find(c => c.Color.Name.Equals(color.Name)).Count++;

                            }
                        }
                    }
                    else
                    {
                        foreach (var shirt in _shirts.FindAll(s => s.Size.Name.Equals(size.Name)))
                        {
                            ShirtsFound.Add(shirt);
                            SzCounts.Find(s => s.Size.Name.Equals(shirt.Size.Name)).Count++;
                            ClCounts.Find(c => c.Color.Name.Equals(shirt.Color.Name)).Count++;
                        }
                    }
                }
            }
            else
            {
                foreach (var color in options.Colors)
                {
                    foreach (var shirt in _shirts.FindAll(s => s.Color.Name == color.Name))
                    {
                        ShirtsFound.Add(shirt);
                        SzCounts.Find(s => s.Size.Name.Equals(shirt.Size.Name)).Count++;
                        ClCounts.Find(c => c.Color.Name.Equals(color.Name)).Count++;
                    }

                }
            }

            SearchResults sr = new SearchResults();
            sr.Shirts = ShirtsFound;
            sr.SizeCounts = SzCounts;
            sr.ColorCounts = ClCounts;
            return sr;
        }

        /// <summary>
        /// Another searching method based on 'for statement' and dealing with the lists as simple arrays without using any predicates 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SearchResults Search_2(SearchOptions options)
        {
            //TODO: search logic goes here.



            if (options.Colors.Count == 0 && options.Sizes.Count != 0)
            {
                for (int shirtIndex = 0; shirtIndex < options.Sizes.Count; shirtIndex++)
                {
                    foreach (var shirt in _shirts)
                    {
                        if (options.Sizes[shirtIndex].Name.Equals(shirt.Size.Name))
                        {
                            ShirtsFound.Add(shirt);

                            ClCounts[ColorsIndex[shirt.Color.Name]].Count++;
                            SzCounts[SizesIndex[shirt.Size.Name]].Count++;
                        }
                    }

                }

            }

            else if (options.Sizes.Count == 0 && options.Colors.Count != 0)
            {
                for (int shirtIndex = 0; shirtIndex < options.Colors.Count; shirtIndex++)
                {
                    foreach (var shirt in _shirts)
                    {
                        if (options.Colors[shirtIndex].Name.Equals(shirt.Color.Name))
                        {
                            ShirtsFound.Add(shirt);
                            ClCounts[ColorsIndex[shirt.Color.Name]].Count++;
                            SzCounts[SizesIndex[shirt.Size.Name]].Count++;
                        }
                    }
                }
            }
            else
            {
                for (int shirtIndex = 0; shirtIndex < options.Sizes.Count; shirtIndex++)
                {
                    foreach (var shirt in _shirts)
                    {
                        if (options.Sizes[shirtIndex].Name.Equals(shirt.Size.Name))
                        {
                            if (options.Colors[shirtIndex].Name.Equals(shirt.Color.Name))
                            {
                                ShirtsFound.Add(shirt);
                                ClCounts[ColorsIndex[shirt.Color.Name]].Count++;
                                SzCounts[SizesIndex[shirt.Size.Name]].Count++;
                            }
                        }
                    }
                }
            }



            SearchResults sr = new SearchResults();
            sr.Shirts = ShirtsFound;
            sr.SizeCounts = SzCounts;
            sr.ColorCounts = ClCounts;
            return sr;
        }
    }
}