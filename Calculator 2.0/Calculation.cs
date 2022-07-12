namespace Calculator_2._0
{
    internal class Calculator    //Калькулятор получающий два массива (чисел и знаков мат. операций) 
    {                             //и выполняющий с ними матеатические операции в соответствии с математической логикой
        public double? SelectSign(double[] numbers_, string[] signs_)   //Метод определяющий необходимую последовательность операций
        {
            List<string> signs = new(signs_);   //Приведение массива знаков мат. операций к списку
            List<double> numbers = new(numbers_);  //Приведение массива чисел к списку 

            double x;    //Переменная для первого числа в каждом выражении
            double y;   //Переменная для второго числа в каждом выражении
            string z;  //Переменная для знака в каждом выражении 

            for (int i = 0; i < signs.Count; i++)  //Перебор знаков мат. операций
            {
                if (signs[i] == "/" || signs[i] == "*")  //Проверка на соответствие делению или умножению
                {
                    if (signs[i] == "/" & numbers[i + 1] != 0 || signs[i] == "*")  //Проверка деления на ноль
                    {
                        Decision(i);  //Вызов вычисляющего метода
                        i--;        //Возвращение на один знак назад, т.к. используемый удален
                    }
                    else return null; //Если попытка делить на ноль вернуть null
                }
                else if (signs[i] == "^")  //Проверка на знак степени
                {
                    Decision(i);  //Вызов вычисляющего метода
                    i--;        //Возвращение на один знак назад, т.к. используемый удален
                }
                else if (signs[i] == "sqrt")  //Проверка на знак квадратного корня
                {
                    z = signs[i];      //Присваивание переменной "sqrt"
                    x = numbers[i];   //Присваивание переменной числа
                    numbers[i] = Construct(x, z); //Вызов соответствующего выражения
                }
            }
            for (int i = 0; i < signs.Count; i++)   //Второй перебор для правильной очередности мат. операций
            {
                if (signs[i] == "+" || signs[i] == "-")  //Проверка на соответствие сложению или вычитанию
                {
                    Decision(i);  //Вызов вычисляющего метода
                    i--;        //Возвращение на один знак назад, т.к. используемый удален
                }
            }
            return numbers[0];  //Вернуть первое число в массиве

            void Decision(int i)  //Метод выполнения мат. операции 
            {
                z = signs[i];         //Присваивание переменной знака
                x = numbers[i];      //Присваивание переменной числа перед знаком
                y = numbers[i + 1]; //Присваивание переменной числа после знака
                numbers[i] = Construct(x, y, z); //Вызов соответствующего выражения
                numbers.RemoveAt(i + 1); //Удаление числа после знака
                signs.RemoveAt(i);      //Удаление знака
            }
        }
        double Construct(double x, double y, string z)  //Метод выбиращий нужное выражение
        {
            MathematicalExpressions mathematical = new();
            switch (z)
            {
                case "+": return mathematical.Addition(x, y);

                case "-": return mathematical.Subtraction(x, y);

                case "*": return mathematical.Multiplication(x, y);

                case "/": return mathematical.Division(x, y);

                case "^": return mathematical.Exponentiation(x, y);

                default: return 0;
            }
        }
        double Construct(double x, string z)  //Перегрузка 
        {
            MathematicalExpressions mathematical = new();
            switch (z)
            {
                case "sqrt": return mathematical.Sqrt(x);
                default: return 0;
            }
        }
    }
    class MathematicalExpressions  //реализация математические выражения
    {
        public double Addition(double x, double y) => x + y;
        public double Subtraction(double x, double y) => x - y;
        public double Multiplication(double x, double y) => x * y;
        public double Division(double x, double y) => x / y;
        public double Exponentiation(double x, double y) => Math.Pow(x, y);
        public double Sqrt(double x) => Math.Sqrt(x); 
    }
}
