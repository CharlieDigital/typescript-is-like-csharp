import { describe, test, expect, vi } from "vitest";
import { User, UserService } from "./model";

describe("User creation", () => {
  test("user display name is formatted correctly", () => {
    const user = new User("Charles", "Chen", "chrlschn@example.org");

    expect(user.displayName).toBe("Charles Chen");
  });

  test("user handle is email with @", () => {
    const user = new User("Ada", "Lovelace", "alove@example.org");

    expect(user.displayName).toBe("Ada Lovelace");
    expect(user.handle).toBe("@alove");
  });

  test("user saved to database", () => {
    const UserRepository = vi.fn();
    UserRepository.prototype.saveToDb = vi.fn(
      () => "Saved user (from mock repository)"
    );

    const repo = new UserRepository();
    const userService = new UserService(repo);
    const user = new User("Ada", "Lovelace", "alove@example.org");
    const msg = userService.saveUser(user);

    expect(msg).toBe("Saved user (from mock repository)");
  });
});
