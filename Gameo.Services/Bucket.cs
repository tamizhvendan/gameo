using System.Collections.Generic;

namespace Gameo.Services
{
    public class Bucket<T> where T :class
    {
        public string Label { get; set; }
        public IEnumerable<T> Values { get; set; } 
    }
}