using System.Text.Json;
using OneOf;

var log = (object payload) => Console.WriteLine(JsonSerializer.Serialize(payload));

// Arrays
string[] pets = ["Tomi", "Rascal", "Puck"];

var pets2 = new[] { "asdf" };

string[] pets3 = [.. pets];

log(pets3[0..2]);

// Lists
var friends = new List<string> { "Christi" };
friends.Add("Ram");
friends.Add("Minli");

log(friends[1]);

// Stacks
var tasks = new Stack<string>();
tasks.Push("task1");
tasks.Push("task2");
var task2 = tasks.Pop();

log(task2);

// Queues
var tasks2 = new Queue<string>();
tasks2.Enqueue("task1");
tasks2.Enqueue("task2");
var task1 = tasks2.Dequeue();

log(task1);

/// <summary>
///
/// </summary>
/// <param name="a"></param>
/// <param name="b"></param>
/// <returns></returns>
int Sum(int a, int b)
{
    return a + b;
}

Transit.TransitOption ChooseTransit(int numPeople)
{
    if (numPeople == 1)
        return new Scooter(true);
    if (numPeople < 5)
        return new Car(5);
    if (numPeople < 7)
        return new Car(8);
    return new Train(TrainType.Bullet);
}

ChooseTransit(5)
    .Switch(
        car => log($"Car with {car.numSeats} seats"),
        scooter => log($"Scooter is electric: {scooter.electric}"),
        train => log($"Train type: {train.type}")
    );

enum TrainType
{
    Bullet,
    Normal
}

record Car(int numSeats);

record Scooter(bool electric);

record Train(TrainType type);

namespace Transit
{
    [GenerateOneOf]
    partial class TransitOption : OneOfBase<Car, Scooter, Train> { };
}
