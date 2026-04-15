namespace lab2CompMath;

public class Program
{
    public static float[,]? ConsoleIO()
    {
        int column, row;
        Console.WriteLine();
        
        do
        {
            Console.Write("Введите размерность матрицы (Квадратная): ");
            int.TryParse(Console.ReadLine(), out row);
            column = row;
         

            if (column != row) Console.WriteLine("Ошибка! Поиск решения для такой матрицы невозможен! Нужна квадратная");
            Console.WriteLine();
        } while (column != row);
        column++;
        float[,]? matrix = new float[row, column];
        
        for (int i = 0; i < row; i++)
        {
            bool flag = true;
            Console.Write($"Введите строку {i + 1} (через пробел): ");
            while(flag)
            {
                flag = false;
                string? str = Console.ReadLine(); // строка матрицы
                if (str == null)
                {
                    Console.WriteLine("Ввод строки отменен.");
                    Console.Write($"Введите строку {i + 1} (через пробел): ");
                    flag = true;
                }

                string[] values = str.Split(' ');

                if (values.Length != row)
                {
                    Console.WriteLine($"Ошибка: В строке {i + 1} должно быть {row} значений.");
                    Console.Write($"Введите строку {i + 1} (через пробел): ");
                    flag = true;

                }
                if (!flag)
                {
                    for (int j = 0; j < row; j++)
                    {
                        if (!float.TryParse(values[j], out float value))
                        {
                            Console.WriteLine($"Ошибка: Некорректное числовое значение '{values[j]}' в строке {i + 1}, столбец {j + 1}.");
                            Console.Write($"Введите строку {i + 1} (через пробел): ");
                            flag = true;
                            break;

                        }
                        matrix[i, j] = value;
                    }
                }
            }
            //тут уже введена матрица квадратная
            //добавим ввод столбца значений
        }
        for (int i = 0; i < row; i++)
        {
            bool flag = true;
            Console.Write($"Введите b {i + 1}: ");
            while (flag)
            {
                flag = false;
                string? str = Console.ReadLine(); // строка матрицы
                if (str == null)
                {
                    Console.WriteLine("Ввод отменен.");
                    //Console.Write($"Введите b {i + 1}: ");
                    //i -= 1;
                    flag = true;
                }

                if (!flag)
                {

                    if (!float.TryParse(str, out float value))
                    {
                        Console.WriteLine($"Ошибка: Некорректное числовое значение '{str}' в b {i + 1}.");
                        //Console.Write($"Введите b {i + 1}: ");
                        flag = true;
                        break;

                    }
                    matrix[i, column - 1] = value;
                    
                }
            }
        }

        //for(int i = 0; i < row; i++)
        //{
        //    for(int j = 0; j < column; j++)
        //    {
        //        Console.Write($"{matrix[i, j]} ");
        //    }
        //    Console.WriteLine();
        //}

        
        Console.WriteLine("Матрица успешно введена.");
        return matrix;
    }

    public static float[,]? Copy(float[,]? origin_matrix)
    {
        int rows = origin_matrix.GetLength(0);
        int cols = origin_matrix.GetLength(1);
        float[,] matrix = new float[rows, cols];

       
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = origin_matrix[i, j];  
            }
        }
        return matrix;
    }

    public static void Main()
    {
        float[,]? matrix;
        float[,]? origin_matrix;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Добро пожаловать в программу для решения СЛАУ!");
        Console.ResetColor();
        origin_matrix = ConsoleIO();
        do
        {
            matrix = Copy(origin_matrix);
            Console.ForegroundColor = ConsoleColor.Yellow; // устанавливаем цвет
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Поиск решения методом Гаусса без выбора главного элемента");
            Console.WriteLine("2. Поиск решения методом Гаусса с выбором главного элемента по столбцу");
            Console.WriteLine("3. Поиск решения методом Гаусса с выбором главного элемента по строке");
            Console.WriteLine("4. Поиск решения методом Гаусса с выбором главного элемента по всей матрице");
            Console.WriteLine("5. Поиск решения методом простых итераций");
            Console.WriteLine("6. Поиск решения методом Зейделя");
            Console.WriteLine("7. Поиск решения с трехдиагональной матрицей (Убедитесь что все не диагональные значения равны 0, иначе решение будет неверно!");
            Console.WriteLine("8. Ввести новую матрицу");
            Console.Write("Выберите действие: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();
            Console.ResetColor(); // сбрасываем в стандартный

            switch (choice) //что делаем
            {

                case "1":
                    Console.WriteLine("Поиск решения методом Гаусса без выбора главного элемента:");
                    Methods.GaussNoMainElement(matrix);
                    break;

                case "2":
                    Console.WriteLine("\nПоиск решения методом Гаусса с выбором главного элемента по столбцу:");
                    Methods.GaussWithMainElementByColumn(matrix);
                    break;

                case "3":
                    Console.WriteLine("\nПоиск решения методом Гаусса с выбором главного элемента по строке:");
                    Methods.GaussWithMainElementByRow(matrix);
                    break;

                case "4":
                    Console.WriteLine("\nПоиск решения методом Гаусса с выбором главного элемента по всей матрице:");
                    Methods.GaussWithMainElementByAllMatrix(matrix);
                    break;

                case "5":
                    Console.WriteLine("\nПоиск решения методом простых итераций:");
                    Methods.SimpleIteration(matrix);
                    break;

                case "6":
                    Console.WriteLine("\nПоиск решения методом Зейделя:");
                    Methods.Seidel(matrix);
                    break;

                case "7":
                    Console.WriteLine("\nПоиск решения с трехдиагональной матрицей:");
                    Methods.RunningAlgorithm(matrix);
                    break;

                case "8":
                    Console.WriteLine("\nВвод новой матрицы:");
                    origin_matrix = ConsoleIO();
                    break;

                default:
                    Console.WriteLine("Некорректный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Yellow; // устанавливаем цвет
            Console.WriteLine("\nНажмите enter, чтобы продолжить");
            Console.WriteLine("Для выхода нажмите esc");
            Console.ResetColor(); // сбрасываем в стандартный
        } while (Console.ReadKey().Key != ConsoleKey.Escape); //реализовал выход по esc
    }
}
