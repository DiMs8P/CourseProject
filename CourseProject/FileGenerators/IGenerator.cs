using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileGenerators
{
    public interface IGenerator
    {
        public void Generate(string rootPath);
    }
}
