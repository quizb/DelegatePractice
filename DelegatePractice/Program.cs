using System;

// 1.声明委托类型
public delegate int MathOperation(int a, int b);

class Calculator
{
    // 2.定义符合签名的方法
    public int Add(int x, int y) => x + y;
    public int Multiply(int x, int y) => x * y;
}

// 定义委托，表示事件的签名
public delegate void AccountBalanceEventHandler(object sender, decimal balance);
//声明事件
public class BankAccount
{
    private decimal _balance;

    // 定义事件
    public event AccountBalanceEventHandler BalanceChanged;

    // 构造函数
    public BankAccount(decimal initialBalance)
    {
        _balance = initialBalance;
    }

    // 存款方法
    public void Deposit(decimal amount)
    {
        _balance += amount;
        // 触发事件
        BalanceChanged?.Invoke(this, _balance);
    }

    // 获取余额
    public decimal GetBalance()
    {
        return _balance;
    }
}
class Program
{
    static void Main()
    {


        #region 单播委托
        // 创建 Calculator 类的实例
        var calc = new Calculator();

        // 3.实例化委托
        MathOperation op = calc.Add;

        // 使用委托调用方法
        Console.WriteLine(op(3, 5)); // 输出 8

        // 修改委托指向 Multiply 方法
        op = calc.Multiply;
        Console.WriteLine(op(3, 5)); // 输出 15
        #endregion

        #region 多播委托
        MathOperation multiOp = calc.Add;
        multiOp += calc.Multiply;//将 Multiply 方法添加到 multiOp 的方法列表中，与 Add 方法一起绑定。

        Console.WriteLine(multiOp(3, 5)); // 输出 15
        #endregion

        #region 使用内置泛型委托
        var calcu = new Calculator();
        // 委托用于表示有返回值的方法。它可以有 0 到 16 个输入参数，最后一个参数是返回值类型。
        Func<int, int, int> operation = calcu.Add;

        int result = operation(2, 3); // 5
        Console.WriteLine(result);
        // 委托用于表示无返回值的方法。它可以有 0 到 16 个输入参数。
        Action<string> logger = msg => Console.WriteLine($"LOG: {msg}");
        logger("Calculation completed");
        #endregion

        #region Event
        // 创建银行账户实例
        BankAccount account = new BankAccount(100);

        // 订阅事件
        account.BalanceChanged += OnBalanceChanged;

        // 存款
        account.Deposit(50);
    }

    // 事件处理方法
    private static void OnBalanceChanged(object sender, decimal balance)
    {
        Console.WriteLine($"账户余额已更改：{balance:C}");
    }
    #endregion
}
}