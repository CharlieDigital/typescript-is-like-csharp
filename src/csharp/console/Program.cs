using System.Runtime.CompilerServices;
using System.Text.Json;

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
