using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.DataStructures
{
    public abstract class IReader<T>
    {
        public abstract List<T> Read();
    }
}
