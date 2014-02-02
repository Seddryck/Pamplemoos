using System;
using System.Collections.Generic;
using System.Linq;
using Pamplemoos.Core;

namespace Pamplemoos.Repository
{
    public interface ISessionRepository
    {
        Session GetSession(string Id);
        Session GetLastSession();
        IEnumerable<Session> GetLastSessions(int count);
    }
}
