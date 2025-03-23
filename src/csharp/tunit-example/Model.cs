public record User(
  string FirstName,
  string LastName,
  string Email
)
{
  public string DisplayName => $"{FirstName} {LastName}";

  public string Handle => $"@{Email.Split('@')[0]}";
}

public class UserRepository
{
  public virtual string SaveToDb(User user)
  {
    // TODO: Actual database save.
    return "Saved user (from real repository)";
  }
}

public class UserService(UserRepository userRepository)
{
  public string SaveUser(User user)
  {
    // TODO: Do validation, prepare model, etc.
    return userRepository.SaveToDb(user);
  }
}
