using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RimDev.Releases.Infrastructure
{
    public class NoMarkdownCache : IMarkdownCache 
    {
        public Task<string> Get(string markdown, string context, string mode) 
        {
            return Task.FromResult((string)null);
        }
        
        public Task Add(string markdown, string context, string mode, string contents) 
        {
            return Task.FromResult((object)null);
        }
    }
}