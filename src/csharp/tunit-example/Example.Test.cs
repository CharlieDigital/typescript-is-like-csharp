using NSubstitute;

public class User_Creation
{
  [Test]
  public async Task User_Display_Name_Is_Formatted_Correctly()
  {
    var user = new User("Charles", "Chen", "chrlschn@example.org");

    await Assert.That(user.DisplayName).IsEqualTo("Charles Chen");
  }

  [Test]
  public async Task User_Handle_Is_Email_With_At()
  {
    var user = new User("Ada", "Lovelace", "alove@example.org");

    await Assert.That(user.DisplayName).IsEqualTo("Ada Lovelace");
    await Assert.That(user.Handle).IsEqualTo("@alove");
  }

  [Test]
  public async Task User_Save_To_Database()
  {
    var user = new User("Ada", "Lovelace", "alove@example.org");

    var mockRepo = Substitute.For<UserRepository>();
    mockRepo.SaveToDb(user).Returns("Saved user (from mock repository)");

    var userService = new UserService(mockRepo);
    var msg = userService.SaveUser(user);

    await Assert.That(msg).IsEqualTo("Saved user (from mock repository)");
  }
}
