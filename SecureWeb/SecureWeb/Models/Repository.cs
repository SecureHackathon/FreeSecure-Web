using Raven.Client;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecureWeb.Models {
    
    public class Repository : IDisposable {

        private DocumentStore _documentStore;
        private IDocumentSession _session;

        public Repository() {
            InitializeDocumentStore();
        }

        public void InitializeDocumentStore() {
            if ( _documentStore == null || _documentStore.WasDisposed ) {
                _documentStore = new DocumentStore() { ConnectionStringName = "RavenDB" };
                _documentStore.Initialize();
                _session = _documentStore.OpenSession();
            }
        }

        public IEnumerable<T> GetAll<T>() where T: BaseModel {
            return _session.Query<T>().ToList();
        }

        public T Get<T>( string id ) where T :BaseModel {
            return _session.Load<T>(id);
        }

        public bool Save<T>( T document ) where T :BaseModel {
            try {
                _session.Store(document);
                _session.SaveChanges();
            } catch {
                return false;
            }
            return true;
        }

        public bool Delete<T>( string id ) where T :BaseModel {
            try {
                var document = Get<T>(id);
                _session.Delete<T>(document);
                _session.SaveChanges();
            } catch {
                return false;
            }
            return true;
        }

        public bool Update<T>( T document ) where T : BaseModel {
            
            try {
                _session.Store(document, document.Id);    
                _session.SaveChanges();
            } catch {
                return false;
            }
            return true;
        }

        public void Dispose() {
            if ( _documentStore != null ) {
                _documentStore.Dispose();
            }
        }
    }
}