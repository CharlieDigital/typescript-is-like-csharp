var contact = new Contact("Didi", "Cheng");
Console.WriteLine(contact.DisplayName);

file static class ContactExtensions
{
    extension(Contact contact)
    {
        public string DisplayName =>
          $"{contact.FirstName} {contact.LastName}";
    }
}

public record Contact(string FirstName, string LastName);
