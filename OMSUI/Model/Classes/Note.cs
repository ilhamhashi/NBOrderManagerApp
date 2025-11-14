using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMSDesktopUI.Model.Classes
{
    public class Note
    {
        public int NoteId { get; set; }
        public string NoteText { get; set; }
        public List<string> ImagePaths { get; set; }
    }
}
