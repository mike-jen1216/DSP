using System;

using System.Collections.Generic;

#region Payment Strategy

public interface IPaymentMethod

{

    void Pay(decimal amount);

}

public class LinePay : IPaymentMethod

{

    public void Pay(decimal amount)

    {

        // Logic for LinePay payment

        Console.WriteLine($"Paid {amount} using LinePay.");

    }

}

public class CreditCardPay : IPaymentMethod

{

    public void Pay(decimal amount)

    {

        // Logic for CreditCard payment

        Console.WriteLine($"Paid {amount} using CreditCard.");

    }

}

public class EasyCardPay : IPaymentMethod

{

    public void Pay(decimal amount)

    {

        // Logic for EasyCard payment

        Console.WriteLine($"Paid {amount} using EasyCard.");

    }

}

public class PaymentContext

{

    private IPaymentMethod _paymentMethod;

    public PaymentContext(IPaymentMethod paymentMethod)

    {

        _paymentMethod = paymentMethod;

    }

    public SetPaymentMethod(IPaymentMethod paymentMethod)

    {

        _paymentMethod = paymentMethod;

    }

    public void ExecutePay(decimal amount)

    {

        if (_paymentMethod == null)

        {

            throw new InvalidOperationException("Payment method not set.");

        }

        _paymentMethod.Pay(amount);

    }

}

#endregion



#region Discount Strategy

public interface IDiscount

{

    decimal GetDiscountPrice(decimal originalPrice);

}



public class NoDiscount : IDiscount

{

    public decimal GetDiscountPrice(decimal originalPrice)

    {

        return originalPrice; // No discount applied

    }

}

public class VipDiscount : IDiscount

{

    public decimal GetDiscountPrice(decimal originalPrice)

    {

        return originalPrice*0.9m; // No discount applied

    }

}

public class ThresholdDiscount : IDiscount

{

    public decimal GetDiscountPrice(decimal originalPrice)

    {

        if (originalPrice > 1000)

        {

            return originalPrice - 100; // Apply -100 discount

        }

        return originalPrice; // No discount applied

    }

}

public class DiscountContext

{

    private IDiscount _discount;



    public DiscountContext(IDiscount discount)

    {

        _discount = discount;

    }

    public void SetDiscount(IDiscount discount)

    {

        _discount = discount;

    }

    public decimal ExecuteGetDiscountPrice(decimal originalPrice)

    {

        if (_discount == null)

        {

            throw new InvalidOperationException("Discount method not set.");

        }

        return _discount.GetDiscountPrice(originalPrice);

    }

}

#endregion



#region Notification Factory

public interface INotificationSender

{

    void Send(string message);

}

public class EmailNotificationSender : INotificationSender

{

    public void Send(string message)

    {

        // Logic for sending email notification

        Console.WriteLine($"Email sent: {message}");

    }

}

public class LineNotificationSender : INotificationSender

{

    public void Send(string message)

    {

        // Logic for sending Line notification

        Console.WriteLine($"Line message sent: {message}");

    }

}

public class SMSNotificationSender : INotificationSender

{

    public void Send(string message)

    {

        // Logic for sending SMS notification

        Console.WriteLine($"SMS sent: {message}");

    }

}

public class NotificationCenter

{

    private List<INotificationSender> _observers = new List<INotificationSender>();

    public void AddObserver(INotificationSender notificationSender)

    {

        _observers.Add(notificationSender);



    }

    public void RemoveObserver(INotificationSender notificationSender)

    {

        _observers.Remove(notificationSender);

    }

    public void NotifyAll(string message)

    {

        if (_observers.Count > 0)

        {

            foreach (var item in _observers)

            {

                _observers.Send(message);

            }

        }

    }

}

#endregion

#region Observer Event Args





public class NotiArgs : EventArgs

{

    public string Message { get; }

    public NotiArgs(string message)

    {

        Message = message;

    }

}

public class NotiEvent

{

    public event EventHandler<NotiArgs> notiEvent;

    public void SendMessage(string message)

    {

        notiEvent?.Invoke(this, new NotiArgs(message));

    }

}

public class EmailSubscriber

{

    public void OnMessage(object sender, NotiArgs e)

    {

        Console.WriteLine("Get message from Email" + e.Message);

    }

}

public class LineSubscriber

{

    public void OnMessage(object sender, NotiArgs e) {

        Console.WriteLine("Get message from Line" + e.Message);

    }

}

#endregion

#region Decorator Pattern

public interface ICoffeeGet {

    public void GetCoffee(int times);

}

public class AmericanoButton : ICoffeeGet

{

    public void GetCoffee(int times)

    {

        Console.WriteLine("Get Americano" + times + "cup.");

    }

}public class LatteButton : ICoffeeGet

{

    public void GetCoffee(int times)

    {

        Console.WriteLine("Get Latte" + times + "cup.");

    }

}

public class CoffeeContext : ICoffeeGet

{

    private ICoffeeGet _inner;

    private bool IsVIP;

    public CoffeeContext(ICoffeeGet coffee, bool isVIP)

    {

        _inner = coffee;

        IsVIP = isVIP;

    }

    public void SetCoffee(ICoffeeGet coffee, bool isVIP)

    {

        _inner = coffee;

        IsVIP = isVIP;

    }

    public void GetCoffee(int times)

    {

        if (!IsVIP)

            throw new Exception("Access Denied");

        Console.WriteLine("Coffee Prep");

        _inner.GetCoffee(times);

        Console.WriteLine("Coffee Done");

    }

}

#endregion

#region Proxy Pattern

public class NotiProxy : INotificationSender

{

    private readonly INotificationSender _sender;

    private bool IsVIP;

    public NotiProxy(INotificationSender sender, bool isVIP)

    {

        _sender = sender;

        IsVIP=isVIP;

    }

    public void Send(string message)

    {

        if (!IsVIP)

        {

            Console.WriteLine("Is Not VIP");

            return;

        }

        _sender.Send(message);

    }

}

#endregion

#region Template Pattern

public abstract class Beer

{

    public void Make()

    {

        PrepWater();

        AddGrains();

        Brew();

        Bottled();

    }

    protected void PrepWater()

    {

        Console.WriteLine("Prep water to brew");

    }

    protected void AddGrains()

    {



        Console.WriteLine("Add default grains");

    }

    protected abstract void Brew();

    protected abstract void Bottled();

}

public class Taiwan18Days : Beer

{

    protected override void Bottled()

    {

        Console.WriteLine("Bottled Taiwan18Days");

    }



    protected override void Brew()

    {

        Console.WriteLine("Brew Taiwan18Days");

    }

}

public class Heniken : Beer

{



    protected override void Bottled()

    {

        Console.WriteLine("Bottled Heniken");

    }



    protected override void Brew()

    {

        Console.WriteLine("Brew Heniken");

    }

}

#endregion

#region Abstract Factory



public interface IBeer

{

    void Brew();

}

public interface IBottle

{

    void Package();

}public interface ILabel

{

    void Print();

}

public interface IBeerFactory

{

    IBeer CreateBeer();

    IBottle CreateBottle();

    ILabel CreateLabel();

}

public class TaiwanFactory : IBeerFactory

{

    IBeer CreateBeer() => new TaiwanBeer();

    IBottle CreateBottle() => new TaiwanBottle();

    ILabel CreateLabel() => new TaiwanLabel();

}

public class TaiwanBeer : IBeer

{

    public void Brew()

    {

       

    }

}public class TaiwanBottle : IBottle

{

    public void Brew()

    {

       

    }

}public class TaiwanLabel : ILabel

{

    public void Brew()

    {



    }

}

public class BottleFactory

{

    private IBeer _beer;

    private IBottle _bottle;

    private ILabel _label;

    public BottleFactory(IBeerFactory beerFactory)

    {

        _beer = beerFactory.CreateBeer(); _bottle = beerFactory.CreateBottle(); _label = beerFactory.CreateLabel();

    }

    public void StartProduction()

    {

        _beer.Brew();

        _bottle.Package();

        _label.Print();

    }

}

#endregion

#region Command Pattern

//Revceiver

//ICommand

//Concrete Command

//Invoker



public class CoffeeMachine {

    public void Brew(int times) {

        Console.WriteLine($"🟤 Brewing {times} cup(s) of coffee...");

    }

    public void UnBrew()

    {

        Console.WriteLine("🛑 Brewing cancelled.");

    }

    public void Clean(int times)

    {

        Console.WriteLine($"🧼 Cleaning coffee machine {times} time(s)...");

    }

    public void UnClean()

    {

        Console.WriteLine("↩️ Cancel cleaning process.");

    }

}

public interface ICoffeeMachineCommand

{

    void Execute(int times);

    void Undo();

}

public class CoffeeMachineBrewCommand : ICoffeeMachineCommand

{

    private CoffeeMachine _coffeeMachine;

    public CoffeeMachineBrewCommand(CoffeeMachine coffeeMachine)

    {

        _coffeeMachine = coffeeMachine;

    }

    public void Execute(int times)

    {

        if (times < 0 || times > 2)

            throw new Exception("Times is not right number");

        Console.WriteLine("Brew coffee");

        _coffeeMachine.Brew(times);

    }

    public void Undo()

    {

        Console.WriteLine("Stop brewing coffee");

        _coffeeMachine.Undo();

    }

}

public class CoffeeMachineCleanCommand : ICoffeeMachineCommand



{

    private CoffeeMachine _coffeeMachine;

    public CoffeeMachineCleanCommand(CoffeeMachine coffeeMachine)

    {

        _coffeeMachine = coffeeMachine;

    }

    public void Execute(int times)

    {

        if (times < 0 || times > 1)

            throw new Exception("Times is not right number");

        _coffeeMachine.Clean(times);

    }

    public void Undo()

    {

        _coffeeMachine.UnClean();

    }

}

public class RemoteControl

{

    private ICoffeeMachineCommand _coffeeMachineCommand;

    public RemoteControl(ICoffeeMachineCommand coffeeMachineCommand)

    {

        _coffeeMachineCommand = coffeeMachineCommand;

    }

    public void SetCommand(ICoffeeMachineCommand coffeeMachineCommand)

    {

        _coffeeMachineCommand = coffeeMachineCommand;

    }

    public void PressDo(int times)

    {

        _coffeeMachineCommand.Execute(times);

    }

    public void PressUndo()

    {

        _coffeeMachineCommand.Undo();

    }

}

#endregion

#region State Pattern

public interface IOrderState

{

    void Next(Order context);

    void Cancel(Order context);

}

public class DraftState : IOrderState

{

    public void Next(Order context)

    {

        Console.WriteLine("Order Ongoing");

        context.SetState(new SubmittedState());

    }

    public void Cancel(Order context)

    {



        Console.WriteLine("Order Cancelled");

        context.SetState(new CancelledState());

    }

}

public class SubmittedState : IOrderState

{

    public void Next(Order context)

    {

        Console.WriteLine("Order Submitted");

    }

    public void Cancel(Order context)

    {



        Console.WriteLine("unable to cancel");

    }

}

public class CancelledState : IOrderState

{

    public void Next(Order context)

    {

        Console.WriteLine("Order Cancelled");

    }

    public void Cancel(Order context)

    {

       

        Console.WriteLine("Order already Cancelled");

    }

}

public class Order

{

    private IOrderState _state;

    public Order()

    {

        _state = new DraftState();

    }

    public void SetState(IOrderState state)

    {

        _state = state;

    }

    public void Next()

    {

        _state.Next();

    }

    public void Cancel()

    {

        _state.Cancel();

    }

}

#endregion



#region Coffee State



public enum CoffeeEnum

{

    Start,Brew,Cleanning,Cancelled

}

public interface ICoffeeMachineState

{

    CoffeeEnum coffeeEnum{ get; }

    void Next(CoffeeMachineOperation context);

    void Cancel(CoffeeMachineOperation context);

}

public class CoffeeMachineStartState : ICoffeeMachineState

{

    public CoffeeEnum coffeeEnum => CoffeeEnum.Start;

    public void Next(CoffeeMachineOperation context)

    {

        Console.WriteLine("Coffee Going to brew");

        context.SetState(new CoffeeMachineBrewState());

    }

    public void Cancel(CoffeeMachineOperation context)

    {

        Console.WriteLine("Coffee Cancelled");

       

        context.SetState(new CoffeeMachineCancelState());

    }

}

public class CoffeeMachineBrewState : ICoffeeMachineState

{

    public CoffeeEnum coffeeEnum => CoffeeEnum.Brew;

    public void Next(CoffeeMachineOperation context)

    {

        Console.WriteLine("Coffee Brewing");

        context.SetState(new CoffeeMachineCleaningState());

    }

    public void Cancel(CoffeeMachineOperation context)

    {

        Console.WriteLine("Coffee Cancelled");

        context.SetState(new CoffeeMachineCancelState());

    }

}

public class CoffeeMachineCleaningState : ICoffeeMachineState

{

    public CoffeeEnum coffeeEnum => CoffeeEnum.Cleanning;

    public void Next(CoffeeMachineOperation context)

    {

        Console.WriteLine("Coffee Cleaning");

        context.SetState(new CoffeeMachineStartState());

    }

    public void Cancel(CoffeeMachineOperation context)

    {



        Console.WriteLine("Coffee Cancelled");

        context.SetState(new CoffeeMachineCancelState());

    }

}

public class CoffeeMachineCancelState : ICoffeeMachineState

{

    public CoffeeEnum coffeeEnum => CoffeeEnum.Cancelled;

    public void Next(CoffeeMachineOperation context)

    {

        Console.WriteLine("Coffee State Cancelled");

        context.SetState(new CoffeeMachineStartState());

    }

    public void Cancel(CoffeeMachineOperation context)

    {

        Console.WriteLine("Coffee State Cancelled");

        context.SetState(new CoffeeMachineCancelState());

    }

}

public class CoffeeMachineOperation

{

    private ICoffeeMachineState _state;

    public CoffeeMachineOperation()

    {

        _state = new CoffeeMachineStartState();

    }

    public void SetState(ICoffeeMachineState state)

    {

        _state = state;

        Console.WriteLine($"State{_state.coffeeEnum}");

    }

    public void Next()

    {

        Console.WriteLine($"Current State{_state.coffeeEnum}");

        _state.Next(this);

    }

    public void Cancel()

    {

        Console.WriteLine($"Current State{_state.coffeeEnum}");

        _state.Cancel(this);

    }

}

#endregion

#region Chain of Responsibility

public abstract class Approver

{

    protected Approver _next;

    public void SetNext(Approver next) => _next = next;

    public abstract void Handle(int days);

}

public class TeamLead : Approver

{

    public override void Handle(int days)

    {

        if (days <= 2)

            Console.WriteLine("TeamLead Approved");

        else

            _next?.Handle(days);

    }

}

public class Manager : Approver

{

    public override void Handle(int days)

    {

        if (days <= 5)

            Console.WriteLine("Manager Approved");

        else

            _next?.Handle(days);

    }

}

public class Director:Approver{

    public override void Handle(int days)

    {

        if (days <= 10)

            Console.WriteLine("Director Approved");

        else

            Console.WriteLine("Director Not Approved");

    }

}

#endregion

#region Chain of CoffeeMachine

//Handler

//Concrete

//Client

public abstract class CoffeeMachineHandler

{

    protected CoffeeMachineHandler _next;

    public void SetNext(CoffeeMachineHandler next) => _next = next;

    public abstract bool Handle();

}

public class WaterHandler : CoffeeMachineHandler

{

    private int _waterAmount;

    public WaterHandler(int waterAmount)

    {

        _waterAmount = waterAmount;

    }

    public override bool Handle()

    {

        if (waterAmount < 500)

        {

            Console.WriteLine("Please fill the water");

            return false;

        }

        else

            return _next?.Handle()??true;

    }

}

public class BeanHandler : CoffeeMachineHandler

{

    private int _beansAmount;

    public BeanHandler(int beansAmount)

    {

        _beansAmount = beansAmount;

    }

    public override bool Handle()

    {

        if (beansAmount < 300)

        {

            Console.WriteLine("Please fill the bean");

            return false;

        }

        else

            return _next?.Handle()??true;

    }

}

public class CupHandler:CoffeeMachineHandler

{

    private int _cupSet;

    public CupHandler(bool cupSet) {

        _cupSet = cupSet;

    }

    public override bool Handle()

    {

        if (cupSet == false)

        {

            Console.WriteLine("Please fill the water");

            return false;

        }  

        else

            return _next?.Handle() ?? true;

    }

}

#endregion

#region Mediator Pattern

public interface ICoffeeMediator

{

    void Notify(object sender, string ev);

}

public class CoffeeMediator : ICoffeeMediator

{

    public CoffeeButton Button { get; set; }

    public CoffeeDisplay Display { get; set; }

    public CoffeeCleaner Cleaner { get; set; }

    public void Notify(object sender, string ev)

    {

        if (ev == "ButtonPressed")

        {

            Console.WriteLine("Mediator Starting");

            Display.Show("Brewing");

            Cleaner.Clean();

        }

        else if (ev = "CleanerCompleted")

        {



            Console.WriteLine("Mediator End Cleaning");

        }

    }

}

public class CoffeeButton

{

    private ICoffeeMediator _coffeeMediator;

    public CoffeeButton(ICoffeeMediator coffeeMediator) => _coffeeMediator = coffeeMediator;

    public void Pressed()

    {

        Console.WriteLine("Brewing...");

        _coffeeMediator.Notify(this, "ButtonPressed");

    }

}

public class CoffeeDisplay

{

    public void Show(string message) {

       

        Console.WriteLine($"Display {message}");

    }

}

public class CoffeeCleaner

{



    private ICoffeeMediator _coffeeMediator;

    public CoffeeCleaner(ICoffeeMediator coffeeMediator) => _coffeeMediator = coffeeMediator;

    public void Clean()

    {

        Console.WriteLine("Cleaning...");

        _coffeeMediator.Notify(this,"CleanerCompleted");

    }

}

#endregion

#region Mediator Pattern Coffee Logger

public interface ICoffeeMediatorLogger

{

    void Log(object sender, string ev);

}

//With colleagues

public class CoffeeLoggerMediator : ICoffeeMediatorLogger

{

    public CBotton cBotton;



    public CClean cClean;

    public CDisplay cDisplay;

    public void Log(object sender, string ev)

    {

        if (ev == "brewing button press")

        {

            Console.WriteLine("Mediator Start Brew");

            cDisplay.Display(ev);

            cClean.Clean();

        }

        else if (ev == "cleanning start")

        {

            Console.WriteLine("Mediator Start Cleaning");

            cDisplay.Display(ev);

        }

    }

}

public class CButton

{

    public readonly CoffeeLoggerMediator _coffeeLoggerMediator;

    public CButton(CoffeeLoggerMediator coffeeLoggerMediator) => _coffeeLoggerMediator = coffeeLoggerMediator;

    public void Press()

    {

        _coffeeLoggerMediator.Log(this, "brewing button press");

    }

}

public class CClean

{

    public readonly CoffeeLoggerMediator _coffeeLoggerMediator;

    public CClean(CoffeeLoggerMediator coffeeLoggerMediator) => _coffeeLoggerMediator = coffeeLoggerMediator;

    public void Clean()

    {

        _coffeeLoggerMediator.Log(this, "cleanning start");

    }

}

public class CDisplay

{

    public void Display(string message)

    {

        Console.WriteLine(message);

    }

}

#endregion

#region Memento



public class CM

{

    public int Strength { get; private set; }

    public CM(int str = 1)

    {

        Strength = str;

    }

    public void SetStrength(int value)

    {

        Strength = value;

    }

    public CMMemento Save(int strength)

    {

        return new CMMemento(strength);

    }

    public void Restore(CMMemento cMMemento)

    {

        Strength = cMMemento.Strength;

    }

}

//Memo Only Record when init then get

public class CMMemento

{

    public int Strength { get;}

    public CMMemento(int strength)

    {

        Strength = strength;

    }

}

public class CareTaker

{

    public Stack<CMMemento> stack = new Stack<CMMemento>();



    public void BackUp(CM  cm) {

        stack.push(cm.Save());

    }

    public void Undo(CM  cm)

    {

        if (stack.Count > 0)

        {

            var last = stack.pop();

            cm.Restore(last);

        }

        else

            Console.WriteLine("No Data");

    }

}

#endregion

public class Program

{

    public static void Main(string[] args)

    {

        #region Strategy Payment Context

        PaymentContext paymentContext = new PaymentContext(new LinePay());

        paymentContext.ExecutePay(100.00m);

        paymentContext.SetPaymentMethod(new EasyCardPay());

        paymentContext.ExecutePay(200.00m);

        paymentContext.SetPaymentMethod(new CreditCardPay());

        paymentContext.ExecutePay(200.00m);

        #endregion



        #region Strategy Discount Context

        DiscountContext discountContext = new DiscountContext(new NoDiscount());

        decimal originalPrice = 1200.00m;



        decimal NoDiscountPrice = discountContext.ExecuteGetDiscountPrice(originalPrice);

        Console.WriteLine($"Original Price: {originalPrice}, No Discount Price: {NoDiscountPrice}");



        discountContext.SetDiscount(new VipDiscount());

        decimal VipDiscountPrice = discountContext.ExecuteGetDiscountPrice(originalPrice);

        Console.WriteLine($"Original Price: {originalPrice}, Vip Discount Price: {VipDiscountPrice}");



        discountContext.SetDiscount(new ThresholdDiscount());

        decimal ThresholdDiscountPrice = discountContext.ExecuteGetDiscountPrice(originalPrice);

        Console.WriteLine($"Original Price: {originalPrice}, Threshold Discount Price: {ThresholdDiscountPrice}");

        #endregion



        #region Observer

        var center = new NotificationCenter();

        center.AddObserver(new LineNotificationSender());

        center.AddObserver(new EmailNotificationSender());

        center.AddObserver(new SMSNotificationSender());

        center.NotifyAll("you have a new message");

        #endregion

        #region Observer Event

        var centerEvent = new NotiEvent();

        var email = new EmailSubscriber();

        var line = new LineSubscriber();

        centerEvent.notiEvent += email.OnMessage;

        centerEvent.notiEvent += line.OnMessage;

        centerEvent.SendMessage("new message");

        centerEvent.notiEvent -= line.OnMessage;

        #endregion

        #region Proxy

        var proxy = new NotiProxy(new EmailNotificationSender(), true);

        proxy.Send("VIP Message Send");

        #endregion

        #region  Beer

        var eightendays = new Taiwan18Days();

        eightendays.Make();



        var heniken = new Heniken();

        heniken.Make();

        #endregion

        #region Abstract Factory

        IBeerFactory taiwanFac = new TaiwanFactory();

        BottleFactory bottleFac = new BottleFactory(new taiwanFac());

        bottleFac.StartProduction();

        #endregion

        #region Command

        var coffeeMachine = new CoffeeMachine();

        var brewCommand = new CoffeeMachineBrewCommand(coffeeMachine);

        var cleanCommand = new CoffeeMachineCleanCommand(coffeeMachine);

        var remote = new RemoteControl(brewCommand);

        remote.PressDo(2);

        remote.PressUndo();

        remote.SetCommand(cleanCommand);

        remote.PressDo(2);

        remote.PressUndo();

        #endregion

        #region Order

        var order = new Order();

        order.Next();

        order.Cancel();

        #endregion

        #region Coffee State

        var coffeeOperation = new CoffeeMachineOperation();

        coffeeOperation.Next();

        coffeeOperation.Next();

        coffeeOperation.Cancel();

        #endregion

        #region Leave Chain

        var lead = new TeamLead();

        var manager = new Manager();

        var director = new Director();

        lead.SetNext(manager);

        manager.SetNext(director);

        lead.Handle(1);

        lead.Handle(5);

        lead.Handle(10);

        lead.Handle(13);

        #endregion

        #region Coffee Machine Chain

        var waterHandler = new WaterHandler(600);

        var beanHandler = new BeanHandler(400);

        var cupHandler = new CupHandler(true);

        waterHandler.SetNext(beanHandler);

        beanHandler.SetNext(cupHandler);

        bool allPass = waterHandler.Handle();

        #endregion

        #region Coffee Mediator

        var mediator = new CoffeeMediator();

        var coffeeButton = new CoffeeButton(mediator);

        var coffCleaner = new CoffeeCleaner(mediator);

        var coffeeDisplay = new CoffeeDisplay();



        mediator.Button = coffeeButton;

        mediator.Cleaner = coffCleaner;

        mediator.Display = coffeeDisplay;



        coffeeButton.Pressed();

        #endregion

        #region Coffee Mediator Log

        var mediatorLog = new CoffeeLoggerMediator();

        var coffeelog = new CBotton(mediatorLog);

        var coffeeCleanLog = new CClean(mediatorLog);

        var coffeeDisplayLog = new CDisplay();

        mediatorLog.cBotton = coffeelog;

        mediatorLog.cClean = coffeeCleanLog;

        mediatorLog.cDisplay = coffeeDisplayLog;

        coffeelog.Press();

        #endregion

        #region Memento

        var cm = new CM();

        var cmTaker = new CareTaker();

        cm.SetStrength(3);

        cmTaker.BackUp(cm);

        cm.SetStrength(5);

        cmTaker.BackUp(cm);

        cm.SetStrength(7);

        cmTaker.BackUp(cm);

        cmTaker.Undo(cm);

        #endregion

    }

}