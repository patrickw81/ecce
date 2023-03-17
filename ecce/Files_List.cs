using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecce
{
    internal class Files_List
    {
        int index = 0;
        List<string> file_path;

        public Files_List(IEnumerable<string> files) 
        {
            file_path=new List<string>();
            file_path =files.ToList();
        } 

        public string switchemelemnt(int i)
        {
            int _index = index;
            index = index + (i);
            if (index < 0)
            {
                index= _index;    
            }
            if (index >= file_path.Count)
            {
                index= _index;
            }

            return file_path[index];
        }

    }
}
