using AuditEngine;
using System.ComponentModel.Composition;

namespace SomeAuditEngine
{
    [Export(typeof(IAuditEngine))]
    [ExportMetadata("AuditSource", "Some")]
    public class SomeAuditEngine : IAuditEngine
    {
        public string Audit(string message)
        {
            return $"SomeAuditEngine: {message}";
        }
    }
}
