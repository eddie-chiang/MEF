using AuditEngine;
using System.ComponentModel.Composition;

namespace MyAuditEngine
{
    [Export(typeof(IAuditEngine))]
    [ExportMetadata("AuditSource", "My")]
    public class MyAuditEngine : IAuditEngine
    {
        public string Audit(string message)
        {
            return $"MyAuditEngine: {message}";
        }
    }
}
