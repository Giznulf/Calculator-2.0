using System.Text.RegularExpressions;
namespace Calculator_2._0;

public class InputParser
{
    internal string processedString;       //Переменная получающее обработанную строку
    public void EnteringAnExample()      //Ввод примера
    {
        Console.Write("Введите математический пример: "); 
        string rawString = Console.ReadLine();            //Введение примера processedString
        if (rawString == "Выход") Environment.Exit(0);   //Проверка на ключевое слово "Выход" завершающее программу
        rawString = rawString.Replace(".", ",");        //Приведение разделителя десятичных цифр к понятному программе значению
        processedString = rawString.Replace(" ", "");  //Обработанная строка 
    }
    public void Parser(string stringForParsing, out double[] numbers, out string[] signs)  //Метод получающий введеную строку и отдающий массив чисел и массив действий
    {
        if (Regex.IsMatch(stringForParsing, @"^((\d+([,]\d+)?[*\/+-])+(\d+([,]\d+)?))$"))  //Проверка на соответствие стандарному мат. выражению (+-*/)
        {
            signs = Regex.Matches(stringForParsing, @"([\*\/\+\-]){1,}").Select(m => m.Value).ToArray();  //получение массива знаков (+-*/)
            ParserNumbers(stringForParsing, out numbers);                                                //Получение массива чисел
        }
        else if (Regex.IsMatch(stringForParsing, @"^((sqrt){1}\(?[0-9]+\,?[0-9]*\)?)$"))   //Проверка на квадратный корень
        {
            signs = Regex.Matches(stringForParsing, @"(sqrt){1}").Select(m => m.Value).ToArray(); //Получение выражения "sqrt"           
            ParserNumbers(stringForParsing, out numbers);                                        //Получение числа из которого надо извлечь корень
        }
        else if (Regex.IsMatch(stringForParsing, @"^([0-9]+\^{1}[0-9]+)$"))  //Проверка на возведение в степень
        {
            signs = Regex.Matches(stringForParsing, @"\^{1}").Select(m => m.Value).ToArray(); //Получение знака степени
            ParserNumbers(stringForParsing, out numbers);                                    //Получение числа возводимого в степень и числа степени 
        }
        else                //Во всех остальных случаях массивы пусты
        {
            signs = null;  
            numbers = null; 
        }
    }
    internal void ParserNumbers(string input, out double[] numbers) //Метод получения массива чисел 
    {
        var array = Regex.Matches(input, @"([0-9]{1,}\,?[0-9]*){1,}").Select(m => m.Value).ToArray();  //получение массива чисел в string
        numbers = array.Select(x => double.Parse(x)).ToArray();    // перевод string в double 
    }
    internal static bool CheckingBrackets(string input) => 
        Regex.IsMatch(input, @"((?<=\({1})(\d+([,]\d+)?[*\/+-])+(\d+([,]\d+)?)\)){1}"); //Проверка валидности скобочного выражения 
    internal string ExtractFromBrackets(string inputString,out string outputString, out int index) //Получение выражения в скобках для работы с ним
    {
        index = inputString.IndexOf('(');                                  //Получение индекса открывающей скобки
        int index1 = inputString.IndexOf(')');                            //Получение индекса закрывающей скобки
        string c = inputString.Substring(index + 1, index1 - index - 1); //Извлечение выражения из скобок
        outputString = inputString.Remove(index, index1 - index + 1);   //Удаление выражения в скобках в изначальной строке
        return c;       
    }
}
