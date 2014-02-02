using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pamplemoos.Core
{
    public class Suite
    {
        public string Name { get; set; }
        
        public Suite()
        {
            Suites = new List<Suite>();
            Tests = new List<ExecutedTest>();
        }

        public IList<Suite> Suites { get; set; }
        public IList<ExecutedTest> Tests { get; set; }


        public int TotalTestsExecuted
        {
            get
            {
                var total = Tests.Count;
                foreach (var suite in Suites)
                {
                    total += suite.TotalTestsExecuted;
                }
                return total;
            }
        }

        public int TotalTestsSucceed
        {
            get
            {
                var total = Tests.Count(t => t.Result.IsSuccess);
                foreach (var suite in Suites)
                {
                    total += suite.TotalTestsSucceed;
                }
                return total;
            }
        }

        public float PercentageSucceed
        {
            get
            {
                return TotalTestsSucceed/TotalTestsExecuted*100;
            }
        }

        public Dictionary<string, int> TestsExecutedByCategories(IEnumerable<string> categories)
        {
            var total = new Dictionary<string, int>();
            foreach (var category in categories)
                total.Add(category, 0);

            Predicate<ExecutedTest> condition = test => true;
            Func<ExecutedTest, string, bool> filter = (test, f) => test.Categories.Contains(f);

            GoThrough(condition, filter, ref total);
            return total;
        }

        public Dictionary<string, int> TestsSucceedByCategories(IEnumerable<string> categories)
        {
            var total = new Dictionary<string, int>();
            foreach (var category in categories)
                total.Add(category, 0);

            Predicate<ExecutedTest> condition = test => test.Result.IsSuccess;
            Func<ExecutedTest, string, bool> filter = (test, f) => test.Categories.Contains(f);

            GoThrough(condition, filter, ref total);
            return total;
        }

        protected void GoThrough(Predicate<ExecutedTest> condition, Func<ExecutedTest, string, bool> filter, ref Dictionary<string, int> dico)
        {
            foreach (var filterInstance in dico.Keys)
                dico[filterInstance] += Tests.Count(t => condition(t) && filter(t, filterInstance));

            foreach (var suite in Suites)
                suite.GoThrough(condition, filter, ref dico);
        }
    }
}
