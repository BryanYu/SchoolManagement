using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public MajorEnum? Major { get; set; }
        
        public string Email { get; set; }

        public string PhotoPath { get; set; }

        [NotMapped]
        public string EncryptedId { get; set; }

    }
}