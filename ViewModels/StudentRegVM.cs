using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CollegeManagement_VNR.ViewModels
{
    public class StudentRegVM
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //default
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 30 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; }

        public string Roll_Number { get; set; }
        [Required(ErrorMessage = "Section is required.")]
        public string Section { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[\w\.-]+@[\w\.-]+\.\w+$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Invalid phone number.")]
        public long Phone_Number { get; set; }
        [Required(ErrorMessage = "Fees is required.")]
        [Range(0, 200000, ErrorMessage = "Invalid fees.")]
        public double Student_Fee { get; set; }
        public Nullable<int> User_Id { get; set; }

        //Address.cs
        //public int Id { get; set; }
        [Required(ErrorMessage = "Door number is required.")]
        [StringLength(50, ErrorMessage = "Door number should not exceed 50 characters.")]
        public string Door_Number { get; set; }


        [Required(ErrorMessage = "Area is required.")]
        [StringLength(100, ErrorMessage = "Area should not exceed 100 characters.")]
        public string Area { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, ErrorMessage = "City should not exceed 100 characters.")]

        public string City { get; set; }

        [Required(ErrorMessage = "Pincode is required.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode should be a 6-digit number.")]
        public int Pincode { get; set; }

        public Nullable<int> Teacher_Id { get; set; }//default
        public Nullable<int> Student_Id { get; set; }//default

        //User.cs
        //public int Id { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password must be between 8 and 15 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Status { get; set; }//default
        public int User_Type { get; set; }//default
    }
}
