namespace ConsaltiongApp.Models
{
    public class User
    {
        public User(string firstName, string lastName, string middletName, string groupName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddletName = middletName;
            GroupName = groupName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddletName { get; set; }
        public string GroupName { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1} {2}", LastName, FirstName, MiddletName); }
        }
    }
}