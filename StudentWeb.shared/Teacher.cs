using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebsite.shared
{
    public class Teacher
    {       
            public int TeacherId { get; set; }
            public string Name { get; set; }
            public List<Subject> SubjectsTaught { get; set; } = new List<Subject>();
        
    }
}
