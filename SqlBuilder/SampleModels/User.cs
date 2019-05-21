using System;
using System.Collections.Generic;
using System.Text;

namespace SqlBuilder.SampleModels
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int AccessCount { get; set; }
        public DateTime Birth { get; set; }
    }
}
