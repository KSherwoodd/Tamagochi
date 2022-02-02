using System;
using System.Timers;

//? aTimer = new System.Timers.Timer(20000);

class Tamagochi {
  private string animal;
  private string name;
  private System.Timers.Timer tTimer;

  private int bored = 20;
  private int hunger = 20;
  private int intelligence = 20;

  public void SetupTimer() 
  {
    tTimer = new System.Timers.Timer(20000);
    tTimer.Elapsed += Deteriorate;
    tTimer.AutoReset = true;
    tTimer.Enabled = true;
  }

  private void Deteriorate(Object source, ElapsedEventArgs e)
  {
    Console.WriteLine($"{name} is getting bored, and hungry.");

    bored += 2;
    hunger += 2;
    intelligence -= 1;

    if ((bored >= 100) || (hunger >= 100) || (intelligence <= 0)) {
      Console.WriteLine($"{name} is dead. RIP");
      tTimer.Stop();
      tTimer.Dispose();
      Environment.Exit(0);
    }
  }

  public Tamagochi(string animalType, string animalName) 
  {
    animal = animalType;
    name = animalName;
  }

  public void stats()
  {
    Console.WriteLine($"------------\n{name}'s info\nHunger: {hunger}\nBoredom: {bored}\nIntelligence: {intelligence}\n------------\n");
  }

  public void feed()
  {
    hunger = 0;
    Console.WriteLine($"{name} ate and is now {hunger}% hungry\n");
  }

  public void play()
  {
    bored = 0;
    Console.WriteLine($"You played with {name}, they're {bored}% bored\n");
  }

  public void read()
  {
    int intelpre = (int) intelligence;
    int intelpost = (int) (intelligence * 1.06);

    if (intelpost == 0) {
      intelligence += 1;
      Console.WriteLine($"{name} started getting smart!\n");
    } 
    else if ((intelpost) < 150) {
      intelligence = intelpost;
      Console.WriteLine($"{name} gained {intelpost - intelpre} points of intelligence, total: {intelpost}\n");
    } 
    else {
      Console.WriteLine($"{name} read, but they're too smart and got bored!\n");
      bored += 2;
    }
  }
}

class Program {
  public static void Main (string[] args) {
    Console.WriteLine("What type of animal would you like your pet to be?");
    string type = Console.ReadLine();
    Console.WriteLine("What would you like to call them?");
    string name = Console.ReadLine();

    Tamagochi tm = new Tamagochi(type, name);
    tm.SetupTimer();

    while (true) {
      Console.WriteLine($"What would you like to do with {name}?\n- Feed\n- Play\n- Read\n- Stats");

      string action = Console.ReadLine().ToLower();

      if ((action == "f") || (action == "feed")) {
        tm.feed();
      }
      if ((action == "p") || (action == "play")) {
        tm.play();
      }
      if ((action == "r") || (action == "read")) {
        tm.read();
      }
      if ((action == "s") || (action == "stats")) {
        tm.stats();
      }
    }
    //? Possible implementation for cmd
    //! Should use Enum
    /*while (Console.ReadKey().Key != ConsoleKey.Escape) {
      if (Console.ReadKey().Key == ConsoleKey.F) 
      {
        tm.feed();
      }
      if (Console.ReadKey().Key == ConsoleKey.P) 
      {
        tm.play();
      }
      if (Console.ReadKey().Key == ConsoleKey.R) 
      {
        tm.read();
      }
      if (Console.ReadKey().Key == ConsoleKey.S) 
      {
        tm.stats();
      }*/
  }
}