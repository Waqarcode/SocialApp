
namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob){
            var Today = DateTime.Now;
            var Age = Today.Year - dob.Year;
            if (dob.Date > Today.AddYears(-Age))
            {
               Age--;
            }
            return Age;
        }
    }
}