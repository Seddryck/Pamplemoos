using System;
using System.Collections.Generic;
using System.Linq;
using Pamplemoos.Core;

namespace Pamplemoos.Repository.FileSystem
{
    public sealed class CacheManager
    {
        private static readonly Lazy<CacheManager> lazy =
            new Lazy<CacheManager>(() => new CacheManager());

        public static CacheManager Instance 
        { 
            get 
            {
                return lazy.Value; 
            }
        }

        private CacheManager()
        {
            sessions = new SortedList<DateTime, string>();
        }

        private readonly SortedList<DateTime, string> sessions;
        
        public void RegisterSession(DateTime execution, string filename)
        {
            if (sessions.ContainsKey(execution))
                sessions.Add(execution, filename);
        }

        public IEnumerable<string> RetrieveLastSessions(int count)
        {
            return sessions.Reverse().Take(count).Select(kv => kv.Value);
        }
    }
}
