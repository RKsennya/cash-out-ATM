using System.Text;

var amounts = new Dictionary<int, int>()
{
    [10] = 90,
    [50] = 90,
    [100] = 90,
    [200] = 90
};

string? input = string.Empty;
var suma = 0;
bool continueLoop = true;
bool vendAllNotesType = false;
var toIssue = new Dictionary<int, int>();


do
{
    Console.WriteLine("Введите сумму");
    input = Console.ReadLine();

    checkInputsuma();

    if (input.ToLower() == "y")
    {
        vendAllNotesType = true;
    }

    if (suma > 0)
    {
        findNotes();
    }
    else

    {
        Console.WriteLine("Невозможно выдать больше 30 купюр!!!");

    }

    StringBuilder sb = new StringBuilder();
    sb.Append(Environment.NewLine);
    printNotes(sb);

    Console.WriteLine(sb.ToString());

    Console.WriteLine("Хотите совершить еще одну операцию выдачи денег? y/n");
    input = Console.ReadLine();


    if (input.ToLower() == "y")
    {
        suma = 0;
        vendAllNotesType = false;
        continueLoop = true;
    }

    else
    {
        continueLoop = false;
    }

} while (continueLoop);


void checkInputsuma()
{
    int.TryParse(input, out int amt);

    if (amt % 10 != 0)
    {
        Console.WriteLine("Введите сумму кратную 10");
        input = Console.ReadLine();
        checkInputsuma();
    }

    else
    {
        suma = amt;

    }
    

}

void findNotes()
{

    foreach (var nominal in amounts.Keys.OrderByDescending(x => x))
    {


        var count = Math.Min(suma / nominal, amounts[nominal]);
        toIssue[nominal] = count;
        suma -= count * nominal;



    }


    

}


void printNotes(StringBuilder sb)
{
    foreach (var nominal in toIssue.Keys.OrderByDescending(x => x))
    {
        var k = toIssue.Sum(v => v.Value);
        if (k <= 30)
        {
            sb.Append(Environment.NewLine);
            sb.Append($"Купюр номиналом {nominal} — {toIssue[nominal]} штук" + Environment.NewLine);
            sb.Append("___________________________" + Environment.NewLine);

            // кол-во оставшихся купюр 
            amounts[nominal] -= toIssue[nominal];

            //Console.WriteLine($"Кол-во {amounts[nominal]} штук");

            if (amounts[nominal] < 0)
            {

                var count = Math.Min(suma / nominal, amounts[nominal]);
                toIssue[nominal] = count;
                suma -= count * nominal;

                sb.Append(Environment.NewLine);
                sb.Append($"Купюр номиналом {nominal} — {toIssue[nominal]} штук" + Environment.NewLine);
                sb.Append("___________________________" + Environment.NewLine);


            }

        }

        else

        {
            Console.WriteLine("Невозможно выдать больше 30 купюр!!!");
        }

    }

}
