export class CustomerRepository {
  constructor() {}
}

class Person {
  constructor(
    public firstName: string,
    public lastName: string,
  ) {}

  get displayName(): string {
    return `${this.firstName} ${this.displayName}`;
  }

  contact() {
    console.log("Contacting...");
  }
}
