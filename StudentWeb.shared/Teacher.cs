using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebsite.shared
{
    public class Teacher
    {       
            public string Id { get; set; }
            public string Name { get; set; }
            public List<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
