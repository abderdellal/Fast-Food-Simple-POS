namespace Logic.Model
{
    public class User : ModelBase
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string MotDePass { get; set; }
        public TypeUser UserType { get; set; }
    }

    public enum TypeUser
    {
        Admin,
        Simple
    }
}