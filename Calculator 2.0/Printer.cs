namespace Calculator_2._0
{
    internal class Printer
    {
        internal void Print()
        {
            InputParser Input = new(); 
            Calculator calculation = new();
            while (true)
            {
                Input.EnteringAnExample();     //ввод примера
                string inputString = Input.processedString;  //
                while (InputParser.CheckingBrackets(inputString))  // Проверка на скобки
                {
                    Input.Parser(Input.ExtractFromBrackets(inputString, out string outputString, out int index), //Вызов метода получения выражения в скобках
                        out double[] numbers_, out string[] signs_);                  //Получение двух массивов (числа и знаки)
                    double? decision = calculation.SelectSign(numbers_, signs_);            //Присвоение переменной выполненых вычислений
                    if (decision == null) break;                                           //Если ошибка то выйти из решения 
                    else inputString = outputString.Insert(index, decision.ToString());   //Вставиь результат на место выражения в скобках           
                }
                Input.Parser(inputString, out double[] numbers, out string[] signs);  //Получение двух массивов (числа и знаки)

                if (signs != null & numbers != null)  //Проверка наличия данных
                {
                    if (calculation.SelectSign(numbers, signs) == null) Console.WriteLine($"На ноль делить нельзя."); //При попытке деления на 0
                    else Console.WriteLine($"{Input.processedString} = {calculation.SelectSign(numbers, signs)}");   //Выдача вводной строки и решения
                }
                else Console.WriteLine("Введено неверное выражение, попробуйте еще раз.");
                Console.WriteLine("Для завершения наберите \"Выход\"");
            }
        }
    }
}
