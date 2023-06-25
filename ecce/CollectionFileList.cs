using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecce
{
    internal class CollectionFileList
    {
        private List<string> FilePath { get; set; }
        public int CountFiles { get; set; }
        public CollectionFileList(IEnumerable<string> files) 
        {
            FilePath = new List<string>();
            FilePath = files.ToList();
            CountFiles = FilePath.Count;
        }
        public CollectionFileList(string file)
        {
            FilePath = new List<string>();
            FilePath.Add(file);
            CountFiles = FilePath.Count;
        }

        public string SwitchPicture(int i)
        {
            return FilePath[i];
        }
    }
}
