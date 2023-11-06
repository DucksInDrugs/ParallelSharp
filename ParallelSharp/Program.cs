using ParallelSharp;

int taskNumber = int.Parse(Console.ReadLine());
switch (taskNumber)
{
    case 1:
        Task1 task1= new();
        task1.Run();
        break;
    case 2:
        Task2 task2= new();
        task2.Run();
        break;
    case 3:
        Task3 task3= new();
        task3.Run();
        break;
    case 4:
        Task4 task4= new();
        task4.Run();
        break;
    default:
        Console.WriteLine("Incorrect number");
        break;
}