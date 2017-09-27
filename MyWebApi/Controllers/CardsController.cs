using AuditEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Web.Http;
using System.Linq;
using System.Reflection;
using System.IO;

namespace MyWebApi.Controllers
{
    public class CardsController : ApiController
    {
        [ImportMany(typeof(IAuditEngine))]
        private IEnumerable<Lazy<IAuditEngine, IAuditEngineMetaData>> auditEngines;

        public CardsController()
        {
            var catalog = new AggregateCatalog();
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            var directoryCatalog = new DirectoryCatalog(Path.GetDirectoryName(path), "*.dll"); // All all DLLs in the run time folder.
            catalog.Catalogs.Add(directoryCatalog);
            var container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(this); // Fill the [Import] and [ImportMany] of this class.
            }
            catch (CompositionException e)
            {
                Console.WriteLine(e);
            }
        }

        [Route("api/Cards/{auditSource}")]
        public IEnumerable<string> Get(string auditSource)
        {
            IAuditEngine auditEngine = auditEngines.First(x => x.Metadata.AuditSource == auditSource).Value;

            return new string[] {
                "value1",
                "value2",
                auditEngine.Audit("CardsController.Get")
            };
        }
    }
}
