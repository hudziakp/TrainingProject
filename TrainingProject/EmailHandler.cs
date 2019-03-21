using System.Text.RegularExpressions;

namespace TrainingProject
{
    public class EmailHandler
    {
        public const string SampleEmail = "sample@email.com";

        private const string EmailVerificationRegex = @"^((?!\.)[\w-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$";

        public string Email { get; set; }

        public bool IsValid => new Regex(EmailVerificationRegex).Match(Email).Success;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailAddress">Email address</param>
        public EmailHandler(string emailAddress)
        {
            Email = emailAddress;
        }
    }
}
