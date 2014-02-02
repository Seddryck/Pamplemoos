using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pamplemoos.Core;
using Pamplemoos.Parser;

namespace Pamplemoos.Repository.FileSystem
{
    class SessionRepository : ISessionRepository
    {
        public string RootDirectory { get; protected set; }
        protected ParserFactory Factory { get; set; }
        
        public SessionRepository()
        {
            Factory = new ParserFactory();
        }

        public Session GetSession(string id)
        {
            foreach (var file in Directory.EnumerateFiles(RootDirectory, "*.xml"))
            {
                var fileParser = Factory.GetInstance(file);
                var fileId = fileParser.GetId();
                if (fileId == id)
                    return fileParser.GetSession();
            }
            return null;
        }

        public Session GetLastSession()
        {
            Session session=null;

            foreach (var file in Directory.EnumerateFiles(RootDirectory, "*.xml"))
            {
                var fileParser = Factory.GetInstance(file);
                var fileExecution = fileParser.GetExecution();
                if (session == null || fileExecution > session.Execution)
                    session = fileParser.GetSession();
            }

            return session;
        }

        public IEnumerable<Session> GetLastSessions(int count)
        {
            var sessions = new List<Session>();

            foreach (var file in Directory.EnumerateFiles(RootDirectory, "*.xml"))
            {
                var fileParser = Factory.GetInstance(file);
                var fileExecution = fileParser.GetExecution();
                if (sessions.Count < count || fileExecution > sessions.Select(s => s.Execution).Min())
                    sessions.Add(fileParser.GetSession());
                if (sessions.Count > count)
                    sessions = sessions.OrderBy(s => s.Execution).Take(count).ToList();
            }

            return sessions.OrderBy(s => s.Execution);
        }
    }
}
