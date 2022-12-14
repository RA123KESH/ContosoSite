using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoSite.Models
{
    public class PartialClasses
    {
        [MetadataType(typeof(StudentMetadata))]
        public partial class Student
        {
        }

        [MetadataType(typeof(EnrollmentMetadata))]
        public partial class Enrollment
        {
        }

        [MetadataType(typeof(RegisterMetadata))]
        public partial class ContosoUser
        {

        }

        [MetadataType(typeof (LoginMetadata))]
        public partial class LoginViewModel
        {

        }
    }
}