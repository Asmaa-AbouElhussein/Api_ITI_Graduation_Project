using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace courses_site_api.SignalR
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connection = new Dictionary<T, HashSet<string>>();
    
       
       public int count  {get{ return _connection.Count; } }
        
        public void Add(T key,string connectionId)
        {
            
            lock (_connection)
            {
                HashSet<string> connectionids;
                if (!_connection.TryGetValue(key,out connectionids)) {

                     connectionids = new HashSet<string>();
                    _connection.Add(key, connectionids);
                }
                lock (connectionids)
                {
                    connectionids.Add(connectionId);
                }

            }
        }

        public IEnumerable<string> GetKeys()
        {
           return _connection.Keys.Cast<string>();
        }


        public IEnumerable<string> Getconnections(T key)
        {
            HashSet<string> connections;
            if (_connection.TryGetValue(key,out connections))
            {
                return connections;
            }
            return Enumerable.Empty<string>();
        }
        public void Remove(T key,string connectionid)
        {
            lock (_connection)
            {
                HashSet<string> connectionids;
                if (!_connection.TryGetValue(key,out connectionids))
                {
                    return;
                }
                lock (connectionids)
                {
                    connectionids.Remove(connectionid);
                    if (connectionids.Count == 0)
                    {
                        _connection.Remove(key);
                    }
                }
            }
        }
    
    }
}
