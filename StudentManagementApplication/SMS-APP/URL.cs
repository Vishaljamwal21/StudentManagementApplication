namespace SMS_APP
{
    public class URL
    {
        public static string APIBaseURl = "https://localhost:7077/";
        public static string StudentAPIPath = APIBaseURl + "api/Student";
        public static string CourseAPIPath = APIBaseURl + "api/Course";
        public static string EnrollmentAPIPath = APIBaseURl + "api/Enrollment";
        public static string GradeAPIPath = APIBaseURl + "api/Grade";
        // UserController URLs
        public static string AuthenticateAPIPath = APIBaseURl + "api/User/Authenticate";
        public static string RegisterAPIPath = APIBaseURl + "api/User/Register";
    }
}
