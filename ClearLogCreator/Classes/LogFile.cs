using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearLogCreator.Classes
{
    public class LogFile
    {
        private string _name;
        private string _createDate;
        private string _fileSource;

        public string Name => _name;
        public string CreateDate => _createDate;
        public string FileSource => _fileSource;

        public LogFile(string name, string createDate, string fileSource)
        {
            _name = name;
            _createDate = createDate;
            _fileSource = fileSource;
        }
    }
}
