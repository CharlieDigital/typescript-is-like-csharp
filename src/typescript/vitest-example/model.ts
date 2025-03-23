export class User {
  constructor(
    public readonly firstName: string,
    public readonly lastName: string,
    public readonly email: string
  ) {}

  get displayName() {
    return `${this.firstName} ${this.lastName}`;
  }

  get handle() {
    return `@${this.email.split("@")[0]}`;
  }
}

export class UserRepository {
  saveToDb(user: User): string {
    // TODO: Actual database save.
    return "Saved user (from real repository)";
  }
}

export class UserService {
  constructor(private readonly userRepository: UserRepository) {}

  saveUser(user: User): string {
    // TODO: Do validation, prepare model, etc.
    return this.userRepository.saveToDb(user);
  }
}
