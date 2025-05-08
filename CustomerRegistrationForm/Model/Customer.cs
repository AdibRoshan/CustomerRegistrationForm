using System.ComponentModel.DataAnnotations;

namespace CustomerRegistrationForm.Model
    {
    public class Customer
        {
        public int Id { get; set; }
        [Required ( ErrorMessage = "First name is required" )]
        [MaxLength ( 50 )]
        public string FirstName { get; set; }

        [Required ( ErrorMessage = "Last name is required" )]
        [MaxLength ( 50 )]
        public string LastName { get; set; }

        [Required ( ErrorMessage = "Email is required" )]
        [EmailAddress ( ErrorMessage = "Invalid email address" )]
        [MaxLength ( 100 )]
        public string Email { get; set; }

        [Required ( ErrorMessage = "Phone is required" )]
        [Phone ( ErrorMessage = "Invalid phone number" )]
        [MaxLength ( 15 )]
        public string Phone { get; set; }

        [Required ( ErrorMessage = "Date of birth is required" )]
        [DataType ( DataType.Date )]
        public DateTime DateOfBirth { get; set; }

        [MaxLength ( 200 )]
        public string Address { get; set; }

        [MaxLength ( 100 )]
        public string EmployerName { get; set; }

        [MaxLength ( 100 )]
        public string JobTitle { get; set; }

        [Range ( 0 , 10000000 , ErrorMessage = "Salary must be non-negative" )]
        public decimal Salary { get; set; }

        [Required ( ErrorMessage = "Employment start date is required" )]
        [DataType ( DataType.Date )]
        public DateTime EmploymentStartDate { get; set; }
        }
    }
